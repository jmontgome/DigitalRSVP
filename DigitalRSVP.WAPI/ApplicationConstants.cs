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
            "http://localhost:63432"
        };
        public static string[] AllowedDomains_TEST =
        {
            "http://localhost:63432"
        };
#if RELEASE
        public static string[] AllowedDomains_PROD = 
        {
                
        }
#else
        public static string[] AllowedDomains_PROD =
        {
            "http://localhost:63432"
        };
#endif
    }

    public class KnownSQL
    {
        public class KnownTables
        {
            public static string Event = "dbo.Event";
            public static string Guest = "dbo.Guest";
            public static string Invitation = "dbo.Invitation";
            public static string RSVP = "dbo.RSVP";
        }

        public class KnownStoredProcedures
        {
            public static string Event_Get_ByEmail = "dbo.up_Event_Get_ByEmail";
            public static string Event_Get_ById = "dbo.up_Event_Get_ById";
            public static string Guest_Get_ByRsvpId = "dbo.up_Guest_Get_ByRsvpId";
            public static string Guest_Upsert = "dbo.up_Guest_Upsert";
            public static string Invitation_Get_ByEventId = "dbo.up_Invitation_Get_ByEventId";
            public static string Invitation_Get_ById = "dbo.up_Invitation_Get_ById";
            public static string RSVP_Get_ByEventId = "dbo.up_RSVP_Get_ByEventId";
            public static string RSVP_Get_ById = "dbo.up_RSVP_Get_ById";
            public static string RSVP_Get_ByInvitationID = "dbo.up_RSVP_Get_ByInvitationId";
            public static string RSVP_Submit = "dbo.up_RSVP_Submit";
            public static string RSVP_Update = "dbo.up_RSVP_Update";
        }
    }
}
