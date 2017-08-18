import {Component} from "@angular/core";
import {Observable} from "rxjs/Observable";
import {UsersService} from "./users.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";
import {TenantsService} from "../tenants/tenants.service";
import 'rxjs/add/observable/forkJoin';

@Component({
    templateUrl: "./user-edit-page.component.html",
    styleUrls: ["./user-edit-page.component.css"],
    selector: "ce-user-edit-page"
})
export class UserEditPageComponent {
    constructor(private _usersService: UsersService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList,
        private _tenantsService: TenantsService
    ) { }

    public async ngOnInit() {
        let observables: Array<Observable<any>> = [this._tenantsService.get()];

        if (this.userId)            
            observables.push(this._usersService.getById({ id: this.userId }));

        Observable.forkJoin(observables).subscribe(results => {
            this.tenants = results[0].tenants;
            this.user = this.userId != null ? results[1].user : {};            
        });
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._usersService.addOrUpdate({ user: $event.detail.user, correlationId });
        this._router.navigateByUrl("/users");
    }

    private get userId() { return this._activatedRoute.snapshot.params["id"]; }
    public user = {};
    public tenants = [];
}
