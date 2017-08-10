import {Component,Input,Output,EventEmitter} from "@angular/core";

@Component({
    templateUrl: "./tenant-list-item.component.html",
    styleUrls: [
        "../../styles/list-item.css",
        "./tenant-list-item.component.css"
    ],
    selector: "ce-tenant-list-item"
})
export class TenantListItemComponent {  
    constructor() {
        this.edit = new EventEmitter();
        this.delete = new EventEmitter();		
    }
      
    @Input()
    public tenant: any = {};
    
    @Output()
    public edit: EventEmitter<any>;

    @Output()
    public delete: EventEmitter<any>;        
}
