import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Invitation } from '../../data/invitation';
import { InvitationService } from '../../services/invitation.service';
import { ErrorService } from '../../services/error.service';
import { ApplicationConstants } from '../../application.constants';

@Component({
  selector: 'app-invite',
  templateUrl: './invite.component.html',
  styleUrl: './invite.component.css'
})
export class InviteComponent {
  private readonly _route = inject(ActivatedRoute);
  private readonly _router: Router;

  private readonly _inviteService: InvitationService;
  private readonly _errorService: ErrorService;

  private _screenReady: boolean = false;

  constructor(router: Router,
    inviteService: InvitationService,
    errorService: ErrorService) {
    this._router = router;
    
    this._inviteService = inviteService;
    this._errorService = errorService;
  }

  async ngOnInit() {
    setTimeout(() => {
      if (this._screenReady != true) {
        this._router.navigateByUrl('/not-found');
      }
    }, 30000);
    if (sessionStorage.getItem(ApplicationConstants.AppConstants.HASOPENED_FLAG_STORAGE)) {
      this._router.navigateByUrl('/rsvp');
    }
    else {
      let invId = this._route.snapshot.paramMap.get('invite');
      if (invId) {
        localStorage.setItem(ApplicationConstants.AppConstants.INVITE_ID_STORAGE, invId);
        try {
          let invite: Invitation = await this._inviteService.GetInvitationAsync(invId);
          if (invite) {
            localStorage.setItem(ApplicationConstants.AppConstants.INVITE_OBJ_STORAGE, JSON.stringify(invite));
            this._screenReady = true;
          }
          else {
            this._router.navigateByUrl('not-found');
          }
        }
        catch (exc) {
          this._errorService.SubmitErrorAsync(exc);
        }
      }
      else {
        this._router.navigateByUrl('/not-found');
      }
    }
  }

  screenReady() {
    return this._screenReady;
  }

  openInvitation() {
    sessionStorage.setItem('hasOpenedInv', 'true');
    if (this._screenReady) {
      this._router.navigateByUrl('/rsvp');
    }
  }
}
