import {Component,Input,Output,EventEmitter} from "@angular/core";

@Component({
    templateUrl: "./account-list-item.component.html",
    styleUrls: [
        "../../styles/list-item.css",
        "./account-list-item.component.css"
    ],
    selector: "ce-account-list-item"
})
export class AccountListItemComponent {    
    constructor() {
        this.edit = new EventEmitter();
        this.delete = new EventEmitter();
    }
            
    @Input()
    public account: any = {};
    
    @Output()
    public edit: EventEmitter<any>;

    @Output()
    public delete: EventEmitter<any>;        
}
