import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { FeaturesService } from "./features.service";

import { FeatureEditPageComponent } from "./feature-edit-page.component";
import { FeatureListItemComponent } from "./feature-list-item.component";
import { FeaturePaginatedListComponent } from "./feature-paginated-list.component";

const declarables = [
    FeatureEditPageComponent,
    FeatureListItemComponent,
    FeaturePaginatedListComponent
];

const providers = [FeaturesService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class FeaturesModule { }
