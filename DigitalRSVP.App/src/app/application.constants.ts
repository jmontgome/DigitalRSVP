export enum EnvironmentType {
  DEV = 0,
  TEST = 1,
  STAGING = 2,
  PRODUCTION = 3
}

export class ApplicationConstants {
  public static AppConstants = class {
    public static INVITE_ID_STORAGE: string = "inviteId";
    public static INVITE_OBJ_STORAGE: string = "invite";
    public static RSVP_OBJ_STORAGE: string = "rsvp";

    public static HASOPENED_FLAG_STORAGE: string = "hasOpenedInv";
  }

  public static ApiConstants = class {
    public static GetApiUrl(): string {
      if (this.Environment == EnvironmentType.DEV) {
        return this.DEV_API_URL;
      }
      else if (this.Environment == EnvironmentType.TEST) {
        return this.TEST_API_URL;
      }
      else if (this.Environment == EnvironmentType.STAGING) {
        return this.PROD_API_URL;
      }
      else if (this.Environment == EnvironmentType.PRODUCTION) {
        return this.PROD_API_URL;
      }
      else {
        return this.TEST_API_URL;
      }
    }

    public static Environment: EnvironmentType = EnvironmentType.DEV;

    public static DEV_API_URL: string = "https://localhost:44317/";
    public static TEST_API_URL: string = "https://digitalrsvp-service-dev.azurewebsites.net/";
    public static PROD_API_URL: string = "https://digitalrsvp-service.azurewebsites.net/";

    public static Error_Post: string = "Error";

    public static Utilities_GetNewGuid: string = "Utility/NewGuid";

    public static Invitation_GetInvite: string = "Invitation/id";

    public static Rsvp_GetRsvp: string = "RSVP/id";
    public static Rsvp_GetRsvp_ByInvite: string = "RSVP/invitation=";
  }
}
