import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { ApplicationConstants } from "../application.constants";
import { Invitation } from "../data/invitation";
import { take } from "rxjs";
import { ErrorService } from "./error.service";

@Injectable({
    providedIn: 'root'
})
export class InvitationService {
    private _httpClient: HttpClient;

    private _errorService: ErrorService;

    constructor(httpClient: HttpClient, errorService: ErrorService) {
        this._httpClient = httpClient;
        this._errorService = errorService;
    }

    public async GetInvitationAsync(id: string): Promise<Invitation> {
        return new Promise(resolve => {
            try {
                this._httpClient.get<Invitation>(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Invitation_GetInvite}=${id}`)
                .pipe(take(1))
                .subscribe((data: Invitation) => {
                    resolve(data);
                });
            }
            catch (exc) {
                this._errorService.SubmitErrorAsync(exc);
            }
        });
    }
}