import { Guest } from "./guest";

export class RSVP {
    private _id: string = '';
    private _inviteeId: string = '';
    private _datetime: Date | null = null;
    private _guests: Array<Guest> | null = null;
    private _attendingWedding: boolean = false;
    private _attendingReception: boolean = false;
    private _note: string = '';
    private _createdDate: Date | null = null;
    private _updatedDate: Date | null = null;

    constructor() {}

    get Id(): string {
        return this._id;
    }
    set Id(value: string) {
        this._id = value;
    }

    get InviteeId(): string {
        return this._inviteeId;
    }
    set InviteeId(value: string) {
        this._inviteeId = value;
    }

    get DateTime(): Date | null {
        return this._datetime;
    }
    set DateTime(value: Date) {
        this._datetime = value;
    }

    get Guests(): Array<Guest> | null {
        return this._guests;
    }
    set Guests(value: Array<Guest>) {
        this._guests = value;
    }

    get AttendingWedding(): boolean {
        return this._attendingWedding;
    }
    set AttendingWedding(value: boolean) {
        this._attendingWedding = value;
    }

    get AttendingReception(): boolean {
        return this._attendingReception;
    }
    set AttendingReception(value: boolean) {
        this._attendingReception = value;
    }

    get Note(): string {
        return this._note;
    }
    set Note(value: string) {
        this._note = value;
    }

    get CreatedDate(): Date | null {
        return this._createdDate;
    }
    set CreatedDate(value: Date) {
        this._createdDate= value;
    }

    get UpdatedDate(): Date | null { 
        return this._updatedDate;
    }
    set UpdatedDate(value: Date) {
        this._updatedDate = value;
    }
}