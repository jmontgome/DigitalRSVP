using DigitalRSVP.Core.Options;
using DigitalRSVP.Core.Services;
using DigitalRSVP.WAPI.Extensions;
using DigitalRSVP.WAPI.Repositories;
using DigitalRSVP.WAPI.Repositories.Interfaces;
using DigitalRSVP.WAPI.Services;
using DigitalRSVP.WAPI.Services.Interfaces;
using Microsoft.OpenApi.Models;

namespace DigitalRSVP.WAPI
{
    public class Startup
    {
        public ILogger<Startup> Logger;

        public IConfiguration Configuration { get; }

        public ConnectionOptions ConnectionOptions { get; private set; } = new ConnectionOptions();
        public ApplicationEnvironmentVariables AppEnvVariables { get; private set; }

        public static class ConfigurationKeys
        {
            public const string ConnectionStringsKey = "ConnectionStrings";
        }

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddLog4Net();
            });
            Logger = loggerFactory.CreateLogger<Startup>();

            Logger.LogInformation($"Starting {ApplicationConstants.APPLICATION_TITLE}...");
            Configuration = configuration;
            Configuration.Bind(ConfigurationKeys.ConnectionStringsKey, this.ConnectionOptions);

            try
            {
                AppEnvVariables = new ApplicationEnvironmentVariables(configuration);
            }
            catch (Exception exc)
            {
                Logger.LogError($"Issue establishing Environment Variables - {exc}!");
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Logger.LogInformation($"Environment: {env.EnvironmentName}");
            if (this.ConnectionOptions.DR_ConnString.Contains("UserID=;Password=;"))
            {
                throw new Exception("Connection String Credentials not configured!");
            }

            app.UseHttpsRedirection();
            app.UseRouting();

#if DEBUG
            if (env.IsDevelopment())
                app.UseCors(ApplicationConstants.CORS_DEV);
            else if (env.IsStaging())
                app.UseCors(ApplicationConstants.CORS_TEST);
            else if (env.IsProduction())
                app.UseCors(ApplicationConstants.CORS_PROD);
            else
                app.UseCors(ApplicationConstants.CORS_PROD);
#elif RELEASE
            app.UseCors(ApplicationConstants.CORS_PROD);
#endif

            app.UseAuthentication();
            app.UseAuthorization();

            if (AppEnvVariables != null)
            {
                if (AppEnvVariables.ShowSwagger)
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint($"/swagger/{ApplicationConstants.APPLICATION_VERSION}/swagger.json", $"{ApplicationConstants.APPLICATION_TITLE} {ApplicationConstants.APPLICATION_VERSION} [{env.EnvironmentName}]");
                    });
                }
            }

            app.UseRateLimiter();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(cors =>
            {
#if DEBUG
                cors.AddPolicy(ApplicationConstants.CORS_DEV, builder =>
                {
                    builder.WithOrigins(ApplicationConstants.AllowedDomains_DEV)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
                cors.AddPolicy(ApplicationConstants.CORS_TEST, builder =>
                {
                    builder.WithOrigins(ApplicationConstants.AllowedDomains_TEST)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
#endif
                cors.AddPolicy(ApplicationConstants.CORS_PROD, builder =>
                {
                    builder.WithOrigins(ApplicationConstants.AllowedDomains_PROD)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            services.AddGlobalRateLimiter();
            services.AddStrictRateLimiter(ApplicationConstants.STRICT_RATE_LIMITER_POLICY_NAME);

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddLog4Net();
                builder.SetMinimumLevel(LogLevel.Debug);
            });

            Logger.LogInformation($"Adding Configuration Options for Services to consume.");
            services.AddSingleton<ConnectionOptions>(this.ConnectionOptions);
            services.AddSingleton<ApplicationEnvironmentVariables>(this.AppEnvVariables);

            Logger.LogInformation($"Adding Services");
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IEventService, EventService>();
            services.AddSingleton<IInvitationService, InvitationService>();
            services.AddSingleton<IRSVPService, RSVPService>();


            Logger.LogInformation($"Adding Controllers");
            services.AddControllers();

            Logger.LogInformation($"Adding Repositories");
            services.AddSingleton<IEventRepository, EventRepository>();
            services.AddSingleton<IInvitationRepository, InvitationRepository>();
            services.AddSingleton<IRSVPRepository, RSVPRepository>();
            services.AddSingleton<IGuestRepository, GuestRepository>();

#if RELEASE
            if (AppEnvVariables != null)
            {
                if (AppEnvVariables.RunBackgroundServices)
                {
                    Logger.LogInformation($"Adding Background Services");
                }

                if (AppEnvVariables.ShowSwagger)
                {
                    Logger.LogInformation($"Adding Swagger");
                    services.AddSwaggerGen(options =>
                    {
                        options.SwaggerDoc(ApplicationConstants.APPLICATION_VERSION, new OpenApiInfo
                        {
                            Version = ApplicationConstants.APPLICATION_VERSION,
                            Title = ApplicationConstants.APPLICATION_TITLE,
                            Description = ApplicationConstants.APPLICATION_DESC
                        });
                    });
                }
            }
#elif DEBUG
            Logger.LogInformation($"Adding Background Services");

            Logger.LogInformation($"Adding Swagger");
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApplicationConstants.APPLICATION_VERSION, new OpenApiInfo
                {
                    Version = ApplicationConstants.APPLICATION_VERSION,
                    Title = ApplicationConstants.APPLICATION_TITLE,
                    Description = ApplicationConstants.APPLICATION_DESC
                });
            });
#endif
        }
    }
}
