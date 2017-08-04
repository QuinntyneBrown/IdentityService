import {Component} from "@angular/core";
import {EventHubService} from "./shared/services/event-hub.service";

@Component({
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.css"],
    selector: "app"
})
export class AppComponent {
    constructor(private _eventHubService: EventHubService) { }

    ngOnInit() {
        this._eventHubService.connect();
    }
}