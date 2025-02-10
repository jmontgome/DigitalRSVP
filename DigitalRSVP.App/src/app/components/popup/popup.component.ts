import { Component } from "@angular/core";

@Component({
    selector: 'app-popup',
    templateUrl: './popup.component.html',
    styleUrl: './popup.component.css'
})
export class PopupComponent {
    private _display: boolean = false;

    private _heading: string = '';
    private _message: string = '';
    
    constructor() {}

    setHeading(heading: string) {
        this._heading = heading;
    }
    getHeading(): string {
        return this._heading;
    }

    setMessage(message: string) {
        this._message = message;
    }
    getMessage(): string {
        return this._message;
    }

    displayPopup(value: boolean) {
        this._display = value;
    }
    shouldDisplay(): boolean {
        return this._display;
    }
}