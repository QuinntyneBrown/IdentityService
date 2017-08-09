import {Component, ChangeDetectorRef} from "@angular/core";
import {SubscriptionsService} from "./subscriptions.service";
import {Router} from "@angular/router";
import {pluckOut} from "../shared/utilities/pluck-out";
import {EventHub} from "../shared/services/event-hub";
import {Subscription} from "rxjs/Subscription";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./subscription-paginated-list-page.component.html",
    styleUrls: ["./subscription-paginated-list-page.component.css"],
    selector: "ce-subscription-paginated-list-page"   
})
export class SubscriptionPaginatedListPageComponent {
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _subscriptionsService: SubscriptionsService,
        private _correlationIdsList: CorrelationIdsList,
        private _eventHub: EventHub,
        private _router: Router
    ) {
        this.subscription = this._eventHub.events.subscribe(x => {      
            
            if (this._correlationIdsList.hasId(x.payload.correlationId) && x.type == "[Subscriptions] SubscriptionAddedOrUpdated") {
                this._subscriptionsService.get().toPromise().then(x => {
                    this.unfilteredSubscriptions = x.subscriptions;
                    this.subscriptions = this.filterTerm != null ? this.filteredSubscriptions : this.unfilteredSubscriptions;
                    this._changeDetectorRef.detectChanges();
                });
            } else if (x.type == "[Subscriptions] SubscriptionAddedOrUpdated") {
                
            }
        });      
    }
    
    public async ngOnInit() {
        this.unfilteredSubscriptions = (await this._subscriptionsService.get().toPromise()).subscriptions;   
        this.subscriptions = this.filterTerm != null ? this.filteredSubscriptions : this.unfilteredSubscriptions;       
    }

    public tryToDelete($event) {        
        const correlationId = this._correlationIdsList.newId();

        this.unfilteredSubscriptions = pluckOut({
            items: this.unfilteredSubscriptions,
            value: $event.detail.subscription.id
        });

        this.subscriptions = this.filterTerm != null ? this.filteredSubscriptions : this.unfilteredSubscriptions;
        
        this._subscriptionsService.remove({ subscription: $event.detail.subscription, correlationId });
    }

    public tryToEdit($event) {
        this._router.navigate(["subscriptions", $event.detail.subscription.id]);
    }

    public handleSubscriptionsFilterKeyUp($event) {
        this.filterTerm = $event.detail.value;
        this.pageNumber = 1;
        this.subscriptions = this.filterTerm != null ? this.filteredSubscriptions : this.unfilteredSubscriptions;        
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.subscription = null;
    }

    private subscription: Subscription;
    public _subscriptions: Array<any> = [];
    public filterTerm: string;
    public pageNumber: number;

    public subscriptions: Array<any> = [];
    public unfilteredSubscriptions: Array<any> = [];
    public get filteredSubscriptions() {
        return this.unfilteredSubscriptions.filter((x) => x.email.indexOf(this.filterTerm) > -1);
    }
}
