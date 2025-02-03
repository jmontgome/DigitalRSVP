import { Component, OnInit, inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { RsvpService } from "../../services/rsvp.service";
import { ErrorService } from "../../services/error.service";
import { InvitationService } from "../../services/invitation.service";

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
    private readonly _errorService: ErrorService;

    constructor(router: Router,
        rsvpService: RsvpService,
        inviteService: InvitationService,
        errorService: ErrorService
    )
    {
        this._router = router;
        this._rsvpService = rsvpService;
        this._inviteService = inviteService;
        this._errorService = errorService;
    }

    async ngOnInit() {

    }
}