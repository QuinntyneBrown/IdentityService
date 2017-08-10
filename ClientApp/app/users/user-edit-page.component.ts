import {Component} from "@angular/core";
import {UsersService} from "./users.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./user-edit-page.component.html",
    styleUrls: ["./user-edit-page.component.css"],
    selector: "ce-user-edit-page"
})
export class UserEditPageComponent {
    constructor(private _usersService: UsersService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList
    ) { }

    public async ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {            
            this.user = (await this._usersService.getById({ id: this._activatedRoute.snapshot.params["id"] })).user;
        }
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._usersService.addOrUpdate({ user: $event.detail.user, correlationId });
        this._router.navigateByUrl("/users");
    }

    public user = {};
}
