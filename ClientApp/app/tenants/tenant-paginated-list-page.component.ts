import {Component, ChangeDetectorRef} from "@angular/core";
import {TenantsService} from "./tenants.service";
import {Router} from "@angular/router";
import {pluckOut} from "../shared/utilities/pluck-out";
import {EventHub} from "../shared/services/event-hub";
import {Subscription} from "rxjs/Subscription";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./tenant-paginated-list-page.component.html",
    styleUrls: ["./tenant-paginated-list-page.component.css"],
    selector: "ce-tenant-paginated-list-page"   
})
export class TenantPaginatedListPageComponent {
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _tenantsService: TenantsService,
        private _correlationIdsList: CorrelationIdsList,
        private _eventHub: EventHub,
        private _router: Router
    ) {
        this.subscription = this._eventHub.events.subscribe(x => {      
            
            if (this._correlationIdsList.hasId(x.payload.correlationId) && x.type == "[Tenants] TenantAddedOrUpdated") {
                this._tenantsService.get().toPromise().then(x => {
                    this.unfilteredTenants = x.tenants;
                    this.tenants = this.filterTerm != null ? this.filteredTenants : this.unfilteredTenants;
                    this._changeDetectorRef.detectChanges();
                });
            } else if (x.type == "[Tenants] TenantAddedOrUpdated") {
                
            }
        });      
    }
    
    public async ngOnInit() {
        this.unfilteredTenants = (await this._tenantsService.get().toPromise()).tenants;   
        this.tenants = this.filterTerm != null ? this.filteredTenants : this.unfilteredTenants;       
    }

    public tryToDelete($event) {        
        const correlationId = this._correlationIdsList.newId();

        this.unfilteredTenants = pluckOut({
            items: this.unfilteredTenants,
            value: $event.detail.tenant.id
        });

        this.tenants = this.filterTerm != null ? this.filteredTenants : this.unfilteredTenants;
        
        this._tenantsService.remove({ tenant: $event.detail.tenant, correlationId });
    }

    public tryToEdit($event) {
        this._router.navigate(["tenants", $event.detail.tenant.id]);
    }

    public handleTenantsFilterKeyUp($event) {
        this.filterTerm = $event.detail.value;
        this.pageNumber = 1;
        this.tenants = this.filterTerm != null ? this.filteredTenants : this.unfilteredTenants;        
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.subscription = null;
    }

    private subscription: Subscription;
    public _tenants: Array<any> = [];
    public filterTerm: string;
    public pageNumber: number;

    public tenants: Array<any> = [];
    public unfilteredTenants: Array<any> = [];
    public get filteredTenants() {
        return this.unfilteredTenants.filter((x) => x.email.indexOf(this.filterTerm) > -1);
    }
}
