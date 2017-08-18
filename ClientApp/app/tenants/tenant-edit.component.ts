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
    templateUrl: "./tenant-edit.component.html",
    styleUrls: [
        "../../styles/forms.css",
        "../../styles/edit.css",
        "./tenant-edit.component.css"],
    selector: "ce-tenant-edit"
})
export class TenantEditComponent {
    constructor() {
        this.tryToSave = new EventEmitter();
    }

    @Output()
    public tryToSave: EventEmitter<any>;

    private _tenant: any = {};

    @Input("tenant")
    public set tenant(value) {
        this._tenant = value;

        this.form.patchValue({
            id: this._tenant.id,
            name: this._tenant.name,
            uniqueId: this._tenant.uniqueId
        });
    }
   
    public form = new FormGroup({
        id: new FormControl(0, []),
        name: new FormControl('', [Validators.required]),
        uniqueId: new FormControl('',[Validators.required])
    });
}
