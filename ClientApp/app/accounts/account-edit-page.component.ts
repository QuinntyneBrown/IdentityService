import {Component} from "@angular/core";
import {AccountsService} from "./accounts.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./account-edit-page.component.html",
    styleUrls: ["./account-edit-page.component.css"],
    selector: "ce-account-edit-page"
})
export class AccountEditPageComponent {
    constructor(private _accountsService: AccountsService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList
    ) { }

    public async ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {            
            this.account = (await this._accountsService.getById({ id: this._activatedRoute.snapshot.params["id"] }).toPromise()).account;
        }
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._accountsService.addOrUpdate({ account: $event.detail.account, correlationId });
        this._router.navigateByUrl("/accounts");
    }

    public account = {};
}
