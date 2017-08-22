import {Component, ChangeDetectorRef} from "@angular/core";
import {UsersService} from "./users.service";
import {Router} from "@angular/router";
import {pluckOut} from "../shared/utilities/pluck-out";
import {EventHub} from "../shared/services/event-hub";
import {Subscription} from "rxjs/Subscription";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./user-paginated-list-page.component.html",
    styleUrls: ["./user-paginated-list-page.component.css"],
    selector: "ce-user-paginated-list-page"   
})
export class UserPaginatedListPageComponent {
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _usersService: UsersService,
        private _correlationIdsList: CorrelationIdsList,
        private _eventHub: EventHub,
        private _router: Router
    ) {
        this.subscription = this._eventHub.events.subscribe(x => {      
            
            if (this._correlationIdsList.hasId(x.payload.correlationId) && x.type == "[Users] UserAddedOrUpdated") {
                this._usersService.get().toPromise().then(x => {
                    this.unfilteredUsers = x.users;
                    this.users = this.filterTerm != null ? this.filteredUsers : this.unfilteredUsers;
                    this._changeDetectorRef.detectChanges();
                });
            } else if (x.type == "[Users] UserAddedOrUpdated") {
                
            }
        });      
    }
    
    public async ngOnInit() {
        this.unfilteredUsers = (await this._usersService.get().toPromise()).users;   
        this.users = this.filterTerm != null ? this.filteredUsers : this.unfilteredUsers;       
    }

    public tryToDelete($event) {        
        const correlationId = this._correlationIdsList.newId();

        this.unfilteredUsers = pluckOut({
            items: this.unfilteredUsers,
            value: $event.detail.user.id
        });

        this.users = this.filterTerm != null ? this.filteredUsers : this.unfilteredUsers;
        
        this._usersService.remove({ user: $event.detail.user, correlationId });
    }

    public tryToEdit($event) {
        this._router.navigate(["users", $event.detail.user.id]);
    }

    public tryToChangePassword($event) {
        this._router.navigate(["users", $event.detail.user.id,"changePassword"]);
    }

    public handleUsersFilterKeyUp($event) {
        this.filterTerm = $event.detail.value;
        this.pageNumber = 1;
        this.users = this.filterTerm != null ? this.filteredUsers : this.unfilteredUsers;        
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
        this.subscription = null;
    }

    private subscription: Subscription;
    public _users: Array<any> = [];
    public filterTerm: string;
    public pageNumber: number;

    public users: Array<any> = [];
    public unfilteredUsers: Array<any> = [];
    public get filteredUsers() {
        return this.unfilteredUsers.filter((x) => x.email.indexOf(this.filterTerm) > -1);
    }
}
