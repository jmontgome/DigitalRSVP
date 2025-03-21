namespace DigitalRSVP.WAPI.Reporting
{
    public abstract class ReportingFactory<CallingType>
    {
        private readonly ILogger<CallingType> _logger;
        
        public ReportingFactory(ILogger<CallingType> logger)
        {
            this._logger = logger;
        }

        public abstract string GenerateEmailReportBody();
        public abstract MemoryStream GenerateCSVFile();
    }
}
