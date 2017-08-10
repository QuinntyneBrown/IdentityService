import {Component,Input,Output,EventEmitter} from "@angular/core";

@Component({
    templateUrl: "./feature-list-item.component.html",
    styleUrls: [
        "../../styles/list-item.css",
        "./feature-list-item.component.css"
    ],
    selector: "ce-feature-list-item"
})
export class FeatureListItemComponent {  
    constructor() {
        this.edit = new EventEmitter();
        this.delete = new EventEmitter();		
    }
      
    @Input()
    public feature: any = {};
    
    @Output()
    public edit: EventEmitter<any>;

    @Output()
    public delete: EventEmitter<any>;        
}
