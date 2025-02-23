import { Component, OnInit, inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { InvitationService } from "../../services/invitation.service";
import { ErrorService } from "../../services/error.service";
import { ApplicationConstants } from "../../application.constants";
import { Invitation } from "../../data/invitation";
import { RsvpService } from "../../services/rsvp.service";
import { RSVP } from "../../data/rsvp";

@Component({
    selector: 'app-complete',
    templateUrl: './complete.component.html',
    styleUrl: './complete.component.css'
})
export class CompleteComponent {
    private readonly _route = inject(ActivatedRoute);
    private readonly _router: Router;

    private readonly _inviteService: InvitationService;
    private readonly _rsvpService: RsvpService;
    private readonly _errorService: ErrorService;

    private _invitation: Invitation | null = null;
    private _rsvp: RSVP | null = null;

    constructor(router: Router,
        inviteService: InvitationService,
        rsvpService: RsvpService,
        errorService: ErrorService
    ) {
        this._router = router;
        this._inviteService = inviteService;
        this._rsvpService = rsvpService;
        this._errorService = errorService;
    }

    async ngOnInit() {
        if (!sessionStorage.getItem(ApplicationConstants.AppConstants.HASOPENED_FLAG_STORAGE)) {
            this._router.navigateByUrl('not-found');
        }
        if (!localStorage.getItem(ApplicationConstants.AppConstants.INVITE_ID_STORAGE)) {
            this._router.navigateByUrl('not-found');
        }

        try {            
            let inviteJson = localStorage.getItem(ApplicationConstants.AppConstants.INVITE_OBJ_STORAGE);
            if (inviteJson) {
                this._invitation = JSON.parse(inviteJson);
            }
            else {
                let inviteId = localStorage.getItem(ApplicationConstants.AppConstants.INVITE_ID_STORAGE);
                if (inviteId) {
                    let inviteTemp = await this._inviteService.GetInvitationAsync(inviteId);
                    if (inviteTemp) {
                        this._invitation = inviteTemp;
                    }
                    else {
                        this._router.navigateByUrl('not-found');
                    }
                }
                else {
                    this._router.navigateByUrl('not-found');
                }
            }
            
            this._rsvp = await this._rsvpService.GetRsvpByInvitee(this._invitation!.id);
            if (!this._rsvp) {
                this._router.navigateByUrl('rsvp');
            }
        }
        catch (exc) {
            this._errorService.SubmitErrorAsync(exc);
        }
    }

    getMessageBodyForPage() {
        let message = 'Your RSVP has been submitted!';
    }
}