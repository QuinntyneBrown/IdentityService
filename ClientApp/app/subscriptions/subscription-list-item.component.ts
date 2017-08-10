import {Component,Input,Output,EventEmitter} from "@angular/core";

@Component({
    templateUrl: "./subscription-list-item.component.html",
    styleUrls: [
        "../../styles/list-item.css",
        "./subscription-list-item.component.css"
    ],
    selector: "ce-subscription-list-item"
})
export class SubscriptionListItemComponent {  
    constructor() {
        this.edit = new EventEmitter();
        this.delete = new EventEmitter();		
    }
      
    @Input()
    public subscription: any = {};
    
    @Output()
    public edit: EventEmitter<any>;

    @Output()
    public delete: EventEmitter<any>;        
}
