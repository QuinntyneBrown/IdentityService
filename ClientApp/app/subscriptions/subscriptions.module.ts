import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "../shared/shared.module";

import { SubscriptionsService } from "./subscriptions.service";

import { SubscriptionEditComponent } from "./subscription-edit.component";
import { SubscriptionListItemComponent } from "./subscription-list-item.component";
import { SubscriptionPaginatedListComponent } from "./subscription-paginated-list.component";

const declarables = [
    SubscriptionEditComponent,
    SubscriptionListItemComponent,
    SubscriptionPaginatedListComponent
];

const providers = [SubscriptionsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule, SharedModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class SubscriptionsModule { }
