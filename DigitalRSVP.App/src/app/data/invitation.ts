export class Invitation {
    private _id: string = '';
    private _name: string = '';
    private _weddingParty: boolean = false;
    private _designatedSeating: boolean = false;
    private _noteToInvitee: string = '';
    private _createdDate: Date = new Date();
    private _updatedDate: Date = new Date();

    constructor(id: string) {
        this._id = id;
    }

    get Id(): string {
        return this._id;
    }
    set Id(value: string) {
        this._id = value;
    }

    get Name(): string {
        return this._name;
    }
    set Name(value: string) {
        this._name = value;
    }

    get WeddingParty(): boolean {
        return this._weddingParty;
    }
    set WeddingParty(value: boolean) {
        this._weddingParty = value;
    }

    get DesignatedSeating(): boolean {
        return this._designatedSeating;
    }
    set DesignatedSeating(value: boolean) {
        this._designatedSeating = value;
    }

    get NoteToInvitee(): string {
        return this._noteToInvitee;
    }
    set NoteToInvitee(value: string) {
        this._noteToInvitee = value;
    }

    get Created_Date(): Date {
        return this._createdDate;
    }
    set Created_Date(value: Date) {
        this._createdDate = value;
    }

    get Updated_Date(): Date {
        return this._updatedDate;
    }
    set Updated_Date(value: Date) {
        this._updatedDate = value;
    }
}