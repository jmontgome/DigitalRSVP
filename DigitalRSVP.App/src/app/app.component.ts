import { Component, OnInit, Inject, InjectionToken, FactoryProvider } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationConstants, EnvironmentType } from './application.constants';

export const WINDOW = new InjectionToken<Window>('window');

const windowProvider: FactoryProvider = {
  provide: WINDOW,
  useFactory: () => window
};

export const WINDOW_PROVIDERS = [
  windowProvider
]

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  providers: WINDOW_PROVIDERS
})
export class AppComponent implements OnInit {
  private _router: Router;

  constructor(@Inject(WINDOW) private window: Window, router: Router) {
    this._router = router;
  }

  ngOnInit() {
    if (this.window.location.hostname.includes("localhost")) {
      ApplicationConstants.ApiConstants.Environment = EnvironmentType.DEV;
    }
    else if (this.window.location.hostname.includes("digital-rsvp-dev")) {
      ApplicationConstants.ApiConstants.Environment = EnvironmentType.TEST;
    }
    else if (this.window.location.hostname.includes("digital-rsvp-staging")) {
      ApplicationConstants.ApiConstants.Environment = EnvironmentType.STAGING;
    }
    else if (this.window.location.hostname.includes("digital-rsvp.")) {
      ApplicationConstants.ApiConstants.Environment = EnvironmentType.PRODUCTION;
    }
  }
}
