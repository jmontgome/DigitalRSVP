export class Invitation {
    private _id: string = '';
    private _eventId: string = '';
    private _name: string = '';
    private _weddingParty: boolean = false;
    private _designatedSeating: boolean = false;
    private _noteToInvitee: string = '';
    private _createdDate: Date = new Date();
    private _updatedDate: Date = new Date();

    constructor(id: string) {
        this._id = id;
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

    get name(): string {
        return this._name;
    }
    set name(value: string) {
        this._name = value;
    }

    get weddingParty(): boolean {
        return this._weddingParty;
    }
    set weddingParty(value: boolean) {
        this._weddingParty = value;
    }

    get designatedSeating(): boolean {
        return this._designatedSeating;
    }
    set designatedSeating(value: boolean) {
        this._designatedSeating = value;
    }

    get noteToInvitee(): string {
        return this._noteToInvitee;
    }
    set noteToInvitee(value: string) {
        this._noteToInvitee = value;
    }

    get created_Date(): Date {
        return this._createdDate;
    }
    set created_Date(value: Date) {
        this._createdDate = value;
    }

    get updated_Date(): Date {
        return this._updatedDate;
    }
    set updated_Date(value: Date) {
        this._updatedDate = value;
    }
}