import {Component,Input,Output,EventEmitter} from "@angular/core";

@Component({
    templateUrl: "./user-list-item.component.html",
    styleUrls: [
        "../../styles/list-item.css",
        "./user-list-item.component.css"
    ],
    selector: "ce-user-list-item"
})
export class UserListItemComponent {  
    constructor() {
        this.edit = new EventEmitter();
        this.delete = new EventEmitter();	
        this.changePassword = new EventEmitter();
    }
      
    @Input()
    public user: any = {};
    
    @Output()
    public edit: EventEmitter<any>;

    @Output()
    public delete: EventEmitter<any>;        

    @Output()
    public changePassword: EventEmitter<any>;
}
