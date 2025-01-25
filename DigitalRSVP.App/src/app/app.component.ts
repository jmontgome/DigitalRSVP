import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationConstants, EnvironmentType } from './application.constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private readonly _route = inject(ActivatedRoute);

  private _router: Router;

  constructor(router: Router) {
    this._router = router;
  }

  ngOnInit() {
    if (this._router.url.includes("localhost")) {
      ApplicationConstants.ApiConstants.Environment = EnvironmentType.DEV;
    }
    else if (this._router.url.includes("digitalrsvp-dev")) {
      ApplicationConstants.ApiConstants.Environment = EnvironmentType.TEST;
    }
    else if (this._router.url.includes("digitalrsvp-staging")) {
      ApplicationConstants.ApiConstants.Environment = EnvironmentType.STAGING;
    }
    else if (this._router.url.includes("digitalrsvp.")) {
      ApplicationConstants.ApiConstants.Environment = EnvironmentType.PRODUCTION;
    }
  }
}
