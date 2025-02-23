import { Component, NgModule, OnInit, inject } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { RsvpService } from "../../services/rsvp.service";
import { ErrorService } from "../../services/error.service";
import { UtilityService } from "../../services/utility.service";
import { InvitationService } from "../../services/invitation.service";
import { ApplicationConstants } from "../../application.constants";
import { IsNullOrWhitespace } from "../../internal/string";
import { Event } from "../../data/event";
import { Invitation } from "../../data/invitation";
import { RSVP } from "../../data/rsvp";
import { Guest } from "../../data/guest";
import { EventService } from "../../services/event.service";
import { CommonModule } from "@angular/common";

@Component({
    standalone: true,
    imports: [CommonModule],
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

    private _editMode: boolean = false;

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

            if (this._event && this._invitation) {
                let rsvpInServer = await this._rsvpService.GetRsvpByInvitee(this._invitation!.id);
                if (rsvpInServer) {
                    //Mark page as RSVP is being edited and make sure RSVP is entered as an edit as opposed to submission.
                    this._rsvp = rsvpInServer;
                    this.setupEditMode();
                }
                else {
                    let rsvpGuid = await this._utilityService.GetNewGuidAsync();
                    if (rsvpGuid) {
                        this._rsvp.id = rsvpGuid;
                    }
                    this._rsvp.inviteeId = this._invitation.id;
                    this._rsvp.eventId = this._event.id;
                    this._rsvp.dateTime = new Date();
                }
            }
        }
        catch (exc) {
            this._errorService.SubmitErrorAsync(exc);
        }
    }

    async submitRsvp() {
        this.syncHtmlInputToRsvp();
        localStorage.setItem(ApplicationConstants.AppConstants.RSVP_OBJ_STORAGE, JSON.stringify(this._rsvp));

        if (this._editMode) {
            await this._rsvpService.SubmitRSVPEdit(this._rsvp);
        }
        else {
            await this._rsvpService.SubmitRSVP(this._rsvp);
        }

        this._router.navigateByUrl('complete');
    }

    syncHtmlInputToRsvp() {
        let noteInput = document.getElementById("noteInput") as HTMLTextAreaElement
        this._rsvp.note = noteInput.value;
    }
    syncRsvpToHtmlInput() {
        let noteInput = document.getElementById("noteInput") as HTMLTextAreaElement;
        noteInput.value = this._rsvp.note;        
    }

    setupEditMode() {
        this._editMode = true;
        this.syncRsvpToHtmlInput();
    }

    isEditMode(): boolean {
        return this._editMode;
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
            return this._rsvp.guests as Array<Guest>;
        }
        else {
            this._rsvp.guests = new Array<Guest>();
            return this._rsvp.guests;
        }
    }
    getGuestAgeString(guest: Guest) {
        let guestTyped: Guest = new Guest();
        guestTyped.age = guest.age!;
        return guestTyped.ageAsString();
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

    async addGuest() {
        let nameInput: HTMLInputElement = document.getElementById("guestNameInput")! as HTMLInputElement;
        let ageInput: HTMLSelectElement = document.getElementById("guestAgeInput")! as HTMLSelectElement;
        let weddingInput: HTMLSelectElement = document.getElementById("attendingWeddingInput")! as HTMLSelectElement;
        let receptionInput: HTMLSelectElement = document.getElementById("attendingReceptionInput")! as HTMLSelectElement;

        if (!IsNullOrWhitespace(nameInput.value) && !IsNullOrWhitespace(ageInput.value)) {
            let newGuest: Guest = new Guest();
            let newGuestId: string = await this._utilityService.GetNewGuidAsync();
            newGuest.id = newGuestId;
            newGuest.rsvpId = this._rsvp.id;
            newGuest.age = Number.parseInt(ageInput.value);
            newGuest.name = nameInput.value;
            newGuest.attendingReception = (receptionInput.value == 'true' ? true : false);
            newGuest.attendingWedding = (weddingInput.value == 'true' ? true : false);

            if (!this._rsvp.guests) {
                this._rsvp.guests = new Array<Guest>();
            }
            this._rsvp.guests.push(newGuest);

            //Functionally closes and resets the Guest wizard
            this.cancelGuest();
        }
        else {

        }
    }
    cancelGuest() {
        this.closePopupContainer();
        let guestWizard = document.getElementById("guestWizard")!;
        guestWizard.style.display = "none";
        this.resetGuestWizardInput();
    }

    removeGuestFromList(guest: Guest) {
        let newArray: Array<Guest> = new Array<Guest>();
        this._rsvp.guests!.forEach(iteratorGuest => {
            if (iteratorGuest.id != guest.id) {
                newArray.push(iteratorGuest);
            }
        });

        this._rsvp.guests = newArray;
    }
}
