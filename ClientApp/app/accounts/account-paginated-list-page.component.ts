import {Component, ChangeDetectorRef} from "@angular/core";
import {AccountsService} from "./accounts.service";
import {Router} from "@angular/router";
import {pluckOut} from "../shared/utilities/pluck-out";
import {EventHub} from "../shared/services/event-hub";
import {Subscription} from "rxjs/Subscription";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./account-paginated-list-page.component.html",
    styleUrls: ["./account-paginated-list-page.component.css"],
    selector: "ce-account-paginated-list-page"   
})
export class AccountPaginatedListPageComponent {
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _accountsService: AccountsService,
        private _correlationIdsList: CorrelationIdsList,
        private _eventHub: EventHub,
        private _router: Router
    ) {
        this.subscription = this._eventHub.events.subscribe(x => {      
            
            if (this._correlationIdsList.hasId(x.payload.correlationId) && x.type == "[Accounts] AccountAddedOrUpdated") {
                this._accountsService.get().toPromise().then(x => {
                    this.unfilteredAccounts = x.accounts;
                    this.accounts = this.filterTerm != null ? this.filteredAccounts : this.unfilteredAccounts;
                    this._changeDetectorRef.detectChanges();
                });
            } else if (x.type == "[Accounts] AccountAddedOrUpdated") {
                
            }
        });      
    }
    
    public async ngOnInit() {
        this.unfilteredAccounts = (await this._accountsService.get().toPromise()).accounts;   
        this.accounts = this.filterTerm != null ? this.filteredAccounts : this.unfilteredAccounts;       
    }

    public tryToDelete($event) {        
        const correlationId = this._correlationIdsList.newId();

        this.unfilteredAccounts = pluckOut({
            items: this.unfilteredAccounts,
            value: $event.detail.account.id
        });

        this.accounts = this.filterTerm != null ? this.filteredAccounts : this.unfilteredAccounts;
        
        this._accountsService.remove({ account: $event.detail.account, correlationId });
    }

    public tryToEdit($event) {
        this._router.navigate(["accounts", $event.detail.account.id]);
    }

    public handleAccountsFilterKeyUp($event) {
        this.filterTerm = $event.detail.value;
        this.pageNumber = 1;
        this.accounts = this.filterTerm != null ? this.filteredAccounts : this.unfilteredAccounts;        
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.subscription = null;
    }

    private subscription: Subscription;
    public _accounts: Array<any> = [];
    public filterTerm: string;
    public pageNumber: number;

    public accounts: Array<any> = [];
    public unfilteredAccounts: Array<any> = [];
    public get filteredAccounts() {
        return this.unfilteredAccounts.filter((x) => x.email.indexOf(this.filterTerm) > -1);
    }
}
