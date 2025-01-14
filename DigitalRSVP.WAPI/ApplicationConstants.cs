namespace DigitalRSVP.WAPI
{
    public class ApplicationConstants
    {
        public const string APPLICATION_VERSION = "v0.1";
        public const string APPLICATION_TITLE = "DigitalRSVP API";
        public const string APPLICATION_DESC = "A Web Service for the Digital RSVP Front End.";

        public const string CLIENT_ID = "";

        public const string CORS_TEST = "DR_CORS_TEST";
        public const string CORS_DEV = "DR_CORS_DEV";
        public const string CORS_PROD = "DR_CORS_PROD";

        public static string[] AllowedDomains_DEV =
        {

        };
        public static string[] AllowedDomains_TEST =
        {

        };
#if RELEASE
        public static string[] AllowedDomains_PROD = 
        {
                
        }
#else
        public static string[] AllowedDomains_PROD =
        {

        };
#endif
    }

    public class KnownSQL
    {
        public class KnownTables
        {
            public static string Invitation = "dbo.Invitation";
            public static string RSVP = "dbo.RSVP";
        }

        public class KnownStoredProcedures
        {
            public static string Invitation_Get_ById = "dbo.up_Invitation_Get_ById";
        }
    }
}
