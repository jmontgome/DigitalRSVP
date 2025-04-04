using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace DigitalRSVP.WAPI.Extensions
{
    public static class RateLimiting
    {
        public static void AddGlobalRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext => RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 50,
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(1)
                    }));
            });
        }

        public static void AddStrictRateLimiter(this IServiceCollection services, string policyName)
        {
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(policyName, opt =>
                {
                    opt.PermitLimit = 5;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueLimit = 0;
                });
            });
        }
    }
}
