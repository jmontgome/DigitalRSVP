using System.Collections;

namespace DigitalRSVP.WAPI
{
    public class ApplicationEnvironmentVariables
    {
        private string _showSwaggerKey = "APP_ShowSwagger";
        public bool ShowSwagger { get; private set; } = false;

        private string _bkgServices = "APP_BackgroundServices";
        public bool RunBackgroundServices { get; private set; } = false;

        public ApplicationEnvironmentVariables() { }
        public ApplicationEnvironmentVariables(IDictionary environmentVariables)
        {
            if (environmentVariables != null)
            {
                if (environmentVariables.Contains(_showSwaggerKey))
                {
                    ShowSwagger = bool.Parse((string)environmentVariables[_showSwaggerKey]);
                }
                if (environmentVariables.Contains(_bkgServices))
                {
                    RunBackgroundServices = bool.Parse((string)environmentVariables[_bkgServices]);
                }
            }
        }
    }
}