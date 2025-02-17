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

    get id(): string {
        return this._id;
    }
    set id(value: string) {
        this._id = value;
    }

    get rsvpId(): string {
        return this._rsvpId;
    }
    set rsvpId(value: string) {
        this._rsvpId = value;
    }

    get name(): string {
        return this._name;
    }
    set name(value: string) {
        this._name = value;
    }

    get age(): Age | null {
        return this._age;
    }
    set age(value: Age) {
        this._age = value;
    }
}