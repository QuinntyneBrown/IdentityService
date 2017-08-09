import {Component, ChangeDetectorRef} from "@angular/core";
import {FeaturesService} from "./features.service";
import {Router} from "@angular/router";
import {pluckOut} from "../shared/utilities/pluck-out";
import {EventHub} from "../shared/services/event-hub";
import {Subscription} from "rxjs/Subscription";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./feature-paginated-list-page.component.html",
    styleUrls: ["./feature-paginated-list-page.component.css"],
    selector: "ce-feature-paginated-list-page"   
})
export class FeaturePaginatedListPageComponent {
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _featuresService: FeaturesService,
        private _correlationIdsList: CorrelationIdsList,
        private _eventHub: EventHub,
        private _router: Router
    ) {
        this.subscription = this._eventHub.events.subscribe(x => {      
            
            if (this._correlationIdsList.hasId(x.payload.correlationId) && x.type == "[Features] FeatureAddedOrUpdated") {
                this._featuresService.get().toPromise().then(x => {
                    this.unfilteredFeatures = x.features;
                    this.features = this.filterTerm != null ? this.filteredFeatures : this.unfilteredFeatures;
                    this._changeDetectorRef.detectChanges();
                });
            } else if (x.type == "[Features] FeatureAddedOrUpdated") {
                
            }
        });      
    }
    
    public async ngOnInit() {
        this.unfilteredFeatures = (await this._featuresService.get().toPromise()).features;   
        this.features = this.filterTerm != null ? this.filteredFeatures : this.unfilteredFeatures;       
    }

    public tryToDelete($event) {        
        const correlationId = this._correlationIdsList.newId();

        this.unfilteredFeatures = pluckOut({
            items: this.unfilteredFeatures,
            value: $event.detail.feature.id
        });

        this.features = this.filterTerm != null ? this.filteredFeatures : this.unfilteredFeatures;
        
        this._featuresService.remove({ feature: $event.detail.feature, correlationId });
    }

    public tryToEdit($event) {
        this._router.navigate(["features", $event.detail.feature.id]);
    }

    public handleFeaturesFilterKeyUp($event) {
        this.filterTerm = $event.detail.value;
        this.pageNumber = 1;
        this.features = this.filterTerm != null ? this.filteredFeatures : this.unfilteredFeatures;        
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.subscription = null;
    }

    private subscription: Subscription;
    public _features: Array<any> = [];
    public filterTerm: string;
    public pageNumber: number;

    public features: Array<any> = [];
    public unfilteredFeatures: Array<any> = [];
    public get filteredFeatures() {
        return this.unfilteredFeatures.filter((x) => x.email.indexOf(this.filterTerm) > -1);
    }
}
