import {Component} from "@angular/core";
import {SubscriptionsService} from "./subscriptions.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./subscription-edit-page.component.html",
    styleUrls: ["./subscription-edit-page.component.css"],
    selector: "ce-subscription-edit-page"
})
export class SubscriptionEditPageComponent {
    constructor(private _subscriptionsService: SubscriptionsService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList
    ) { }

    public async ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {            
            this.subscription = (await this._subscriptionsService.getById({ id: this._activatedRoute.snapshot.params["id"] }).toPromise()).subscription;
        }
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._subscriptionsService.addOrUpdate({ subscription: $event.detail.subscription, correlationId });
        this._router.navigateByUrl("/subscriptions");
    }

    public subscription = {};
}
