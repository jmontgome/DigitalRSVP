import { Component, OnInit, inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { RsvpService } from "../../services/rsvp.service";
import { ErrorService } from "../../services/error.service";
import { UtilityService } from "../../services/utility.service";
import { InvitationService } from "../../services/invitation.service";
import { ApplicationConstants } from "../../application.constants";
import { Invitation } from "../../data/invitation";
import { RSVP } from "../../data/rsvp";
import { Guest } from "../../data/guest";

@Component({
    selector: 'app-rsvp',
    templateUrl: './rsvp.component.html',
    styleUrl: './rsvp.component.css'
})
export class RsvpComponent {
    private readonly _route = inject(ActivatedRoute);
    private readonly _router: Router;

    private readonly _rsvpService: RsvpService;
    private readonly _inviteService: InvitationService
    private readonly _utilityService: UtilityService;
    private readonly _errorService: ErrorService;

    private _invitation: Invitation | null = null;
    private _rsvp: RSVP;

    constructor(router: Router,
        rsvpService: RsvpService,
        inviteService: InvitationService,
        utilityService: UtilityService,
        errorService: ErrorService
    )
    {
        this._router = router;
        this._rsvpService = rsvpService;
        this._inviteService = inviteService;
        this._utilityService = utilityService;
        this._errorService = errorService;

        this._rsvp = new RSVP();
    }

    //Some of this is a little hard to read on a tired mind...
    //Maybe look at again later...
    async ngOnInit() {
        if (!sessionStorage.getItem(ApplicationConstants.AppConstants.HASOPENED_FLAG_STORAGE)) {
            this._router.navigateByUrl('not-found');
        }
        if (!localStorage.getItem(ApplicationConstants.AppConstants.INVITE_ID_STORAGE)) {
            this._router.navigateByUrl('not-found');
        }

        try {
            let id = localStorage.getItem(ApplicationConstants.AppConstants.INVITE_ID_STORAGE);
            if (id && !localStorage.getItem(ApplicationConstants.AppConstants.INVITE_OBJ_STORAGE)) {
                let invite = await this._inviteService.GetInvitationAsync(id);
                if (invite) {
                    localStorage.setItem(ApplicationConstants.AppConstants.INVITE_OBJ_STORAGE, JSON.stringify(invite));
                }
                else {
                    this._router.navigateByUrl('not-found');
                }
            }

            let invite = localStorage.getItem(ApplicationConstants.AppConstants.INVITE_OBJ_STORAGE);
            this._invitation = JSON.parse(invite!);

            let rsvpGuid = await this._utilityService.GetNewGuidAsync();
            if (rsvpGuid) {
                this._rsvp.Id = rsvpGuid;
            }

            this._rsvp.DateTime = new Date();
        }
        catch (exc) {
            this._errorService.SubmitErrorAsync(exc);
        }
    }

    getInvitationHeading(): string {
        if (this._invitation) {
            return `Hey, ${this._invitation.name}!`
        }
        else {
            return ``;
        }
    }

    getGuestsFromRSVP(): Array<Guest> {
        if (this._rsvp.Guests) {
            return this._rsvp.Guests;
        }
        else {
            this._rsvp.Guests = new Array<Guest>();
            return this._rsvp.Guests;
        }
    }
}