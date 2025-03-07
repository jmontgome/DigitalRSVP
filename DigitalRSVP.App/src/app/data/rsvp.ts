import { Guest } from "./guest";

export class RSVP {
    private _id: string = '';
    private _eventId: string = '';
    private _inviteeId: string = '';
    private _datetime: Date | null = null;
    private _guests: Array<Guest> | null = null;
    private _note: string = '';
    private _createdDate: Date | null = null;
    private _updatedDate: Date | null = null;

    constructor() {
        this._guests = new Array<Guest>();
    }

    get id(): string {
        return this._id;
    }
    set id(value: string) {
        this._id = value;
    }

    get eventId(): string {
        return this._eventId;
    }
    set eventId(value: string) {
        this._eventId = value;
    }

    get inviteeId(): string {
        return this._inviteeId;
    }
    set inviteeId(value: string) {
        this._inviteeId = value;
    }

    get dateTime(): Date | null {
        return this._datetime;
    }
    set dateTime(value: Date) {
        this._datetime = value;
    }

    get guests(): Array<Guest> | null {
        return this._guests;
    }
    set guests(value: Array<Guest>) {
        this._guests = value;
    }

    get note(): string {
        return this._note;
    }
    set note(value: string) {
        this._note = value;
    }

    get createdDate(): Date | null {
        return this._createdDate;
    }
    set createdDate(value: Date) {
        this._createdDate= value;
    }

    get updatedDate(): Date | null { 
        return this._updatedDate;
    }
    set updatedDate(value: Date) {
        this._updatedDate = value;
    }

    SerializeToJson(): string {
        let json = {
            Id: this._id,
            EventId: this._eventId,
            InviteeId: this._inviteeId,
            DateTime: this._datetime,
            Guests: this._guests,
            Note: this._note
        };
        if (this._guests) {
            this._guests.forEach(guest => {
                let index = json.Guests!.indexOf(guest);
                json.Guests![index] = JSON.parse(guest.SerializeToJson());
            });
        }
        return JSON.stringify(json);
    }
}