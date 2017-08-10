import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "../shared/shared.module";

import { FeaturesService } from "./features.service";

import { FeatureEditComponent } from "./feature-edit.component";
import { FeatureListItemComponent } from "./feature-list-item.component";
import { FeaturePaginatedListComponent } from "./feature-paginated-list.component";

const declarables = [
    FeatureEditComponent,
    FeatureListItemComponent,
    FeaturePaginatedListComponent
];

const providers = [FeaturesService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule, SharedModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class FeaturesModule { }
