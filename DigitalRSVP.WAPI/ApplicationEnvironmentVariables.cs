namespace DigitalRSVP.WAPI
{
    public class EmailCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Host { get; set; }
        public string Port { get; set; }

        public bool Ready
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Username) &&
                    !string.IsNullOrWhiteSpace(this.Password) &&
                    !string.IsNullOrWhiteSpace(this.Host) &&
                    !string.IsNullOrWhiteSpace(this.Port))
                {
                    return true;
                }
                return false;
            }
        }

        public EmailCredentials() { }
    }

    public class ApplicationEnvironmentVariables
    {
        private string _showSwaggerKey = "APP_ShowSwagger";
        public bool ShowSwagger { get; private set; } = false;

        private string _bkgServicesKey = "APP_BackgroundServices";
        public bool RunBackgroundServices { get; private set; } = false;

        private string _emailCredentialsKey = "ServiceEmailCredentials";
        public EmailCredentials EmailCredentials { get; private set; } = new EmailCredentials();

        public ApplicationEnvironmentVariables() { }
        public ApplicationEnvironmentVariables(IConfiguration config)
        {
            if (config != null)
            {
                if (config[_showSwaggerKey] != null)
                {
                    ShowSwagger = bool.Parse(config[_showSwaggerKey]!);
                }
                if (config[_bkgServicesKey] != null)
                {
                    RunBackgroundServices = bool.Parse(config[_bkgServicesKey]!);
                }
                config.Bind(_emailCredentialsKey, EmailCredentials);
            }
        }
    }
}