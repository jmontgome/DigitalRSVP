import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

import { ApplicationConstants } from "../application.constants";
import { take } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class UtilityService {
  private _httpClient: HttpClient;

  constructor(httpClient: HttpClient) {
    this._httpClient = httpClient;
  }

  public async GetNewGuid(): Promise<string> {
    return new Promise(resolve => {
      this._httpClient.get<string>(`${ApplicationConstants.ApiConstants.GetApiUrl()}${ApplicationConstants.ApiConstants.Utilities_GetNewGuid}`)
      .pipe(take(1))
      .subscribe((data: string) => {
        resolve(data);
      });
    });
  }
}
