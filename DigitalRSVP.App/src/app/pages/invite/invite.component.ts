import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Invitation } from '../../data/invitation';
import { InvitationService } from '../../services/invitation.service';

@Component({
  selector: 'app-rsvp',
  templateUrl: './invite.component.html',
  styleUrl: './invite.component.css'
})
export class InviteComponent {
  private readonly _route = inject(ActivatedRoute);
  private readonly _router: Router;

  private readonly _inviteService: InvitationService;

  constructor(router: Router,
    inviteService: InvitationService) {
    this._router = router;
    
    this._inviteService = inviteService;
  }

  async ngOnInit() {
    if (sessionStorage.getItem('hasOpenedInv')) {
      this._router.navigateByUrl('/rsvp');
    }
    else {
      let invId = this._route.snapshot.paramMap.get('invite');
      if (invId) {
        try {
          let invite: Invitation = await this._inviteService.GetInvitationAsync(invId);
          if (invite) {
  
          }
          /*
          Check id and see if it exists in server.
          If it does not, send them to not found.
          if it does set the id into session storage for safe keeping
          and consistency across the application.
          */
        }
        catch (exc) {
          
        }
      }
      else {
        this._router.navigateByUrl('/not-found');
      }
    }
  }

  openInvitation() {
    sessionStorage.setItem('hasOpenedInv', 'true');
  }
}
