import {ChangePasswordValidator} from "../shared/validators/change-password-validator";

import {
    Component,
    Input,
    OnInit,
    EventEmitter,
    Output,
    AfterViewInit,
    AfterContentInit,
    Renderer,
    ElementRef,
} from "@angular/core";

import { FormGroup, FormControl, Validators } from "@angular/forms";

@Component({
    templateUrl: "./change-password.component.html",
    styleUrls: [
        "../../styles/forms.css",
        "./change-password.component.css"
    ],
    selector: "ce-change-password",
})
export class ChangePasswordComponent {
    constructor(private _renderer: Renderer, private _elementRef: ElementRef) {
        this.tryToChangePassword = new EventEmitter();
    }

    public get passwordNativeElement(): HTMLElement {
        return this._elementRef.nativeElement.querySelector("#password");
    }

    ngAfterViewInit() {
        this._renderer.invokeElementMethod(this.passwordNativeElement, 'focus', []);
    }
    
    @Output() public tryToChangePassword: EventEmitter<any>;

    public form = new FormGroup({        
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required]),
    }, ChangePasswordValidator.MatchPassword);
}
