import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ErrorService } from "./error.service";
import { ApplicationConstants } from "../application.constants";
import { take } from "rxjs";
import { Event } from "../data/event";

@Injectable({
    providedIn: 'root'
})
export class EventService {
    private _httpClient: HttpClient;

    private _errorService: ErrorService;

    constructor(httpClient: HttpClient, errorService: ErrorService) {
        this._httpClient = httpClient;
        this._errorService = errorService;
    }

    public async GetEventByIdAsync(eventId: string): Promise<Event> {
        return new Promise(resolve => {
            try {
                this._httpClient.get<Event>(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Event_GetEvent}=${eventId}`)
                .pipe(take(1))
                .subscribe((data: Event) => {
                    resolve(data);
                });
            }
            catch (exc) {
                this._errorService.SubmitErrorAsync(exc);
            }
        });
    }
}