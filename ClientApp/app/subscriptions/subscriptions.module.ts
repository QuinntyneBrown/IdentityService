import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { SubscriptionsService } from "./subscriptions.service";

import { SubscriptionEditPageComponent } from "./subscription-edit-page.component";
import { SubscriptionListItemComponent } from "./subscription-list-item.component";
import { SubscriptionPaginatedListComponent } from "./subscription-paginated-list.component";

const declarables = [
    SubscriptionEditPageComponent,
    SubscriptionListItemComponent,
    SubscriptionPaginatedListComponent
];

const providers = [SubscriptionsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class SubscriptionsModule { }
