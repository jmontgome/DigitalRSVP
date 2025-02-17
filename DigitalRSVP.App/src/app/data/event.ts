export class Event {
    private _id: string = '';
    private _name: string = '';
    private _contactEmail: string = '';
    private _expiryDate: Date | null = null;

    constructor() {}

    get id(): string {
        return this._id;
    }
    set id(value: string) {
        this._id = value;
    }

    get name(): string {
        return this._name;
    }
    set name(value: string) {
        this._name = value;
    }

    get contactEmail(): string {
        return this._contactEmail;
    }
    set contactEmail(value: string) {
        this._contactEmail = value;
    }

    get expiryDate(): Date | null {
        return this._expiryDate;
    }
    set expiryDate(value: Date) {
        this._expiryDate = value;
    }
}