import {Component} from "@angular/core";
import {FeaturesService} from "./features.service";
import {Router,ActivatedRoute} from "@angular/router";
import {guid} from "../shared/utilities/guid";
import {CorrelationIdsList} from "../shared/services/correlation-ids-list";

@Component({
    templateUrl: "./feature-edit-page.component.html",
    styleUrls: ["./feature-edit-page.component.css"],
    selector: "ce-feature-edit-page"
})
export class FeatureEditPageComponent {
    constructor(private _featuresService: FeaturesService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _correlationIdsList: CorrelationIdsList
    ) { }

    public async ngOnInit() {
        if (this._activatedRoute.snapshot.params["id"]) {            
            this.feature = (await this._featuresService.getById({ id: this._activatedRoute.snapshot.params["id"] }).toPromise()).feature;
        }
    }

    public tryToSave($event) {
        const correlationId = this._correlationIdsList.newId();
        this._featuresService.addOrUpdate({ feature: $event.detail.feature, correlationId });
        this._router.navigateByUrl("/features");
    }

    public feature = {};
}
