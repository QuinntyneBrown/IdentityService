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

import {FormGroup,FormControl,Validators} from "@angular/forms";

@Component({
    templateUrl: "./user-edit.component.html",
    styleUrls: [
        "../../styles/forms.css",
        "../../styles/edit.css",
        "./user-edit.component.css"],
    selector: "ce-user-edit"
})
export class UserEditComponent {
    constructor() {
        this.tryToSave = new EventEmitter();
    }

    @Output()
    public tryToSave: EventEmitter<any>;

    @Input()
    public tenants: Array<any> = [];

    private _user: any = {};

    @Input("user")
    public set user(value) {
        this._user = value;
        
        this.form.patchValue({
            id: this._user.id,
            username: this._user.username,
            tenantId: this._user.tenantId
        });
    }
   
    public form = new FormGroup({
        id: new FormControl(0, []),
        username: new FormControl('', [Validators.required]),
        tenantId: new FormControl('', [Validators.required]),
        newPassword: new FormControl(''),
        confirmNewPassword: new FormControl('')
    });
}
