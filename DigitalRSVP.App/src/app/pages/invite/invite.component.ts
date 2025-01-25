import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-rsvp',
  templateUrl: './invite.component.html',
  styleUrl: './invite.component.css'
})
export class InviteComponent {
  private readonly _route = inject(ActivatedRoute);
  private readonly _router: Router;

  private _invitationId: string = "";

  constructor(router: Router) {
    this._router = router;
  }

  ngOnInit() {
    if (sessionStorage.getItem('hasOpenedInv')) {
      this._router.navigateByUrl('/rsvp');
    }
    else {
      let invId = this._route.snapshot.paramMap.get('invite');
      if (invId) {
        this._invitationId = invId;
        /*
        Check id and see if it exists in server.
        If it does not, send them to not found.
        if it does set the id into session storage for safe keeping
        and consistency across the application.
        */
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
