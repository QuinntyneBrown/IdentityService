import {Component, Input} from "@angular/core";

@Component({
    templateUrl: "./header.component.html",
    styleUrls: ["./header.component.css"],
    selector: "ce-header"
})
export class HeaderComponent {

    @Input()
    public isAuthenticated: boolean;
}
