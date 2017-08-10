import {Component} from "@angular/core";
import {TenantsService} from "./tenants.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./tenant-edit-page.component.html",
    styleUrls: ["./tenant-edit-page.component.css"],
    selector: "ce-tenant-edit-page"
})
export class TenantEditPageComponent {
    constructor(private _tenantsService: TenantsService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList
    ) { }

    public async ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {            
            this.tenant = (await this._tenantsService.getById({ id: this._activatedRoute.snapshot.params["id"] })).tenant;
        }
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._tenantsService.addOrUpdate({ tenant: $event.detail.tenant, correlationId });
        this._router.navigateByUrl("/tenants");
    }

    public tenant = {};
}
