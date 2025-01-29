export enum Age {
    INFANT = 0,
    MINOR = 1,
    ADULT = 2
}

export class Guest {
    private _id: string = '';
    private _rsvpId: string = '';
    private _name: string = '';
    private _age: Age | null = null;

    constructor() { }

    get Id(): string {
        return this._id;
    }
    set Id(value: string) {
        this._id = value;
    }

    get RSVPId(): string {
        return this._rsvpId;
    }
    set RSVPId(value: string) {
        this._rsvpId = value;
    }

    get Name(): string {
        return this._name;
    }
    set Name(value: string) {
        this._name = value;
    }

    get Age(): Age | null {
        return this._age;
    }
    set Age(value: Age) {
        this._age = value;
    }
}