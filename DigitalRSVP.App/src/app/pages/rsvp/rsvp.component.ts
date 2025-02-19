import { Component, OnInit, inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { RsvpService } from "../../services/rsvp.service";
import { ErrorService } from "../../services/error.service";
import { UtilityService } from "../../services/utility.service";
import { InvitationService } from "../../services/invitation.service";
import { ApplicationConstants } from "../../application.constants";
import { Event } from "../../data/event";
import { Invitation } from "../../data/invitation";
import { RSVP } from "../../data/rsvp";
import { Guest } from "../../data/guest";
import { EventService } from "../../services/event.service";

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
    private readonly _eventService: EventService;
    private readonly _errorService: ErrorService;

    private _invitation: Invitation | null = null;
    private _event: Event | null = null;
    private _rsvp: RSVP;

    constructor(router: Router,
        rsvpService: RsvpService,
        inviteService: InvitationService,
        utilityService: UtilityService,
        eventService: EventService,
        errorService: ErrorService
    )
    {
        this._router = router;
        this._rsvpService = rsvpService;
        this._inviteService = inviteService;
        this._utilityService = utilityService;
        this._eventService = eventService;
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
            let inviteJson = localStorage.getItem(ApplicationConstants.AppConstants.INVITE_OBJ_STORAGE);
            if (id && inviteJson) {
                let inviteTemp = await this._inviteService.GetInvitationAsync(id);
                if (inviteTemp) {
                    let eventData = await this._eventService.GetEventByIdAsync(inviteTemp.eventId);
                    if (!eventData) {
                        this._router.navigateByUrl('not-found');
                    }
                    
                    localStorage.setItem(ApplicationConstants.AppConstants.EVENT_OBJ_STORAGE, JSON.stringify(eventData));
                    localStorage.setItem(ApplicationConstants.AppConstants.INVITE_OBJ_STORAGE, JSON.stringify(inviteTemp));
                }
                else {
                    this._router.navigateByUrl('not-found');
                }
            }

            let invite = localStorage.getItem(ApplicationConstants.AppConstants.INVITE_OBJ_STORAGE);
            let event = localStorage.getItem(ApplicationConstants.AppConstants.EVENT_OBJ_STORAGE);
            
            this._event = JSON.parse(event!);
            this._invitation = JSON.parse(invite!);

            let rsvpInServer = await this._rsvpService.GetRsvpByInvitee(this._invitation!.id);
            if (rsvpInServer) {
                //Mark page as RSVP is being edited and make sure RSVP is entered as an edit as opposed to submission.
            }
            else {
                let rsvpGuid = await this._utilityService.GetNewGuidAsync();
                if (rsvpGuid) {
                    this._rsvp.id = rsvpGuid;
                }
                this._rsvp.inviteeId = this._invitation!.id;
                this._rsvp.dateTime = new Date();
            }
        }
        catch (exc) {
            this._errorService.SubmitErrorAsync(exc);
        }
    }

    async submitRsvp() {
        let attendingWeddingInput = document.getElementById("attendingWeddingInput");
        let attendingReceptionInput = document.getElementById("attendingReceptionInput");
        let noteInput = document.getElementById("noteInput");
        this._rsvp.attendingReception = attendingReceptionInput!.innerText == "true" ? true : false;
        this._rsvp.attendingWedding = attendingWeddingInput!.innerText == "true" ? true : false;
        this._rsvp.note = noteInput!.innerText;
        this._router.navigateByUrl('complete');
    }
    
    getEventHeading(): string {
        if (this._event) {
            return this._event.name;
        }
        else {
            return ``;
        }
    }
    getInvitationNote(): string {
        if (this._invitation) {
            return this._invitation.noteToInvitee;
        }
        else {
            return ``;
        }
    }
    getInvitationSpecialDetails(): string {
        if (this._invitation) {
            if (this._invitation.weddingParty) {
                return `You have also been reserved as part of the wedding party, because of how special you are!`;
            }
        }
        return ``;
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
        if (this._rsvp.guests) {
            return this._rsvp.guests;
        }
        else {
            this._rsvp.guests = new Array<Guest>();
            return this._rsvp.guests;
        }
    }

    /* Page Inputs and Element Functions */

    openPopupContainer() {
        let popupContainer = document.getElementById("popupContainer")!;
        popupContainer.style.display = "block";
    }
    closePopupContainer() {
        let popupContainer = document.getElementById("popupContainer")!;
        popupContainer.style.display = "none";
    }

    openDuplicatePopup() {
        this.openPopupContainer();

        let duplicateWindow = document.getElementById("duplicateWindow")!;
        duplicateWindow.style.display = "block";
    }
    closeDuplicatePopup() {
        this.closePopupContainer();

        let duplicateWindow = document.getElementById("duplicateWindow")!;
        duplicateWindow.style.display = "none";
    }

    closeWelcomePopup() {
        this.closePopupContainer();

        let welcomeWindow = document.getElementById("welcomeWindow")!;
        welcomeWindow.style.display = "none";
    }

    openGuestWizard() {
        this.openPopupContainer();
        let guestWizard = document.getElementById("guestWizard")!;
        guestWizard.style.display = "block";
    }
    resetGuestWizardInput() {
        let nameInput: HTMLInputElement = document.getElementById("guestNameInput")! as HTMLInputElement;
        let ageInput: HTMLInputElement = document.getElementById("guestAgeInput")! as HTMLInputElement;

        nameInput.value = "";
        ageInput.value = "";
    }

    addGuest() {

    }
    cancelGuest() {
        this.closePopupContainer();
        let guestWizard = document.getElementById("guestWizard")!;
        guestWizard.style.display = "none";
        this.resetGuestWizardInput();
    }
}
