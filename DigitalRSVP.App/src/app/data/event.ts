export class Event {
    private _id: string = '';
    private _name: string = '';
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

    get expiryDate(): Date | null {
        return this._expiryDate;
    }
    set expiryDate(value: Date) {
        this._expiryDate = value;
    }
}