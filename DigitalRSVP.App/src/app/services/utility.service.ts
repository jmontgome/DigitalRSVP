import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { ApplicationConstants } from "../application.constants";
import { take } from "rxjs";
import { ErrorService } from "./error.service";

@Injectable({
    providedIn: 'root'
})
export class UtilityService {
  private _httpClient: HttpClient;
  private _errorService: ErrorService

  constructor(httpClient: HttpClient, errorService: ErrorService) {
    this._httpClient = httpClient;
    this._errorService = errorService;
  }

  public async GetNewGuidAsync(): Promise<string> {
    return new Promise(resolve => {
      try {
        this._httpClient.get<string>(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Utilities_GetNewGuid}`)
        .pipe(take(1))
        .subscribe((data: string) => {
          resolve(data);
        });
      }
      catch (exc) {
        this._errorService.SubmitErrorAsync(exc);
      }
    });
  }
}
