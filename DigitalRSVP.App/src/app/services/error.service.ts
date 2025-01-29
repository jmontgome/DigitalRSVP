
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
            
        }
        else {

        }
    }
}
