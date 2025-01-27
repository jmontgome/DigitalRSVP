import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { ApplicationConstants } from "../application.constants";
import { Invitation } from "../data/invitation";
import { take } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class InvitationService {
    private _httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this._httpClient = httpClient;
    }

    public async GetInvitationAsync(id: string): Promise<Invitation> {
        return new Promise(resolve => {
            this._httpClient.get<Invitation>(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Invitation_GetInvite}=${id}`)
            .pipe(take(1))
            .subscribe((data: Invitation) => {
                resolve(data);
            })
        });
    }
}