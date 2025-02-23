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
    private _attendingWedding: boolean = false;
    private _attendingReception: boolean = false;

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

    get attendingWedding(): boolean {
        return this._attendingWedding;
    }
    set attendingWedding(value: boolean) {
        this._attendingWedding = value;
    }

    get attendingReception(): boolean {
        return this._attendingReception;
    }
    set attendingReception(value: boolean) {
        this._attendingReception = value;
    }

    ageAsString(): string {
        switch (this._age) {
            case Age.INFANT: return "Infant/Toddler";
            case Age.MINOR: return "Child/Minor";
            case Age.ADULT: return "Adult";
            default: return "";
        }
    }
    
    SerializeToJson() {
        return JSON.stringify({
            Id: this._id,
            RSVPId: this._rsvpId,
            Name: this._name,
            Age: this._age,
            AttendingWedding: this._attendingWedding,
            AttendingReception: this._attendingReception
        })
    }
}