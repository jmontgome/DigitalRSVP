
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { ApplicationConstants } from "../application.constants";
import { Invitation } from "../data/invitation";
import { take } from "rxjs";
import { Error } from "../data/error";

@Injectable({
    providedIn: 'root'
})
export class ErrorService {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    public async SubmitErrorAsync(error: any) {
        if (error instanceof Error) {
            this._httpClient.post<string>(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Error_Post}`, JSON.stringify(error));
        }
        else {
            let errorPost: Error = new Error();
            errorPost.ErrorMessage = error.message;
            errorPost.DateTime = new Date();
            
            var rsvp = localStorage.getItem(ApplicationConstants.AppConstants.RSVP_OBJ_STORAGE);
            if (rsvp) {
                errorPost.RSVP = JSON.parse(rsvp);
            }
            var inv = localStorage.getItem(ApplicationConstants.AppConstants.INVITE_OBJ_STORAGE);
            if (inv) {
                errorPost.Invite = JSON.parse(inv);
            }
            var invId = localStorage.getItem(ApplicationConstants.AppConstants.INVITE_ID_STORAGE);
            if (invId) {
                errorPost.InviteId = invId;
            }

            this._httpClient.post<string>(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Error_Post}`, JSON.stringify(errorPost));
        }
    }
}
