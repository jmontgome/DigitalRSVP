import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ErrorService } from "./error.service";
import { RSVP } from "../data/rsvp";
import { ApplicationConstants } from "../application.constants";
import { take } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class RsvpService {
    private _httpClient: HttpClient;

    private _errorService: ErrorService;

    constructor(httpClient: HttpClient, errorService: ErrorService) {
        this._httpClient = httpClient;
        this._errorService = errorService;
    }

    public async GetRsvpAsync(rsvpId: string): Promise<RSVP> {
        return new Promise(resolve => {
            try {
                this._httpClient.get<RSVP>(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Rsvp_GetRsvp}=${rsvpId}`)
                    .pipe(take(1))
                    .subscribe((data: RSVP) => {
                        resolve(data);
                    });
            }
            catch (exc) {
                this._errorService.SubmitErrorAsync(exc);
            }
        });
    }

    public async GetRsvpByInvitee(inviteId: string): Promise<RSVP> {
        return new Promise(resolve => {
            try {
                this._httpClient.get<RSVP>(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Rsvp_GetRsvp_ByInvite}=${inviteId}`)
                    .pipe(take(1))
                    .subscribe((data: RSVP) => {
                        resolve(data);
                    });
            }
            catch (exc) {
                this._errorService.SubmitErrorAsync(exc);
            }
        })
    }

    public async SubmitRSVP(rsvp: RSVP) {
        try {
            await this._httpClient.post(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Rsvp_Submit}`, rsvp.SerializeToJson()).subscribe();
        }
        catch (exc) {
            this._errorService.SubmitErrorAsync(exc);
        }
    }

    public async SubmitRSVPEdit(rsvp: RSVP) {
        try {
            await this._httpClient.put(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Rsvp_Edit}`, rsvp.SerializeToJson()).subscribe();
        }
        catch (exc) {
            this._errorService.SubmitErrorAsync(exc);
        }
    }
}