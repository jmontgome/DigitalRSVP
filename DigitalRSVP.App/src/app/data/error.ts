import { Invitation } from "./invitation";
import { RSVP } from "./rsvp";

export class Error {
    private _inviteId: string = '';
    private _errorMessage: string = '';
    private _invite: Invitation | null = null;
    private _rsvp: RSVP | null = null;
    private _dateTime: Date | null = null;

    constructor() { }

    get InviteId(): string {
        return this._inviteId;
    }
    set InviteId(value: string) {
        this._inviteId = value;
    }

    get ErrorMessage(): string {
        return this._errorMessage;
    }
    set ErrorMessage(value: string) {
        this._errorMessage = value;
    }

    get Invite(): Invitation | null {
        return this._invite;
    }
    set Invite(value: Invitation) {
        this._invite = value;
    }

    get RSVP(): RSVP | null {
        return this._rsvp;
    }
    set RSVP(value: RSVP) {
        this._rsvp = value;
    }

    get DateTime(): Date | null {
        return this._dateTime;
    }
    set DateTime(value: Date) {
        this._dateTime = value;
    }
}