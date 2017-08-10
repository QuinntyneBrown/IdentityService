import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { AccountsService } from "./accounts.service";

import { AccountEditPageComponent } from "./account-edit-page.component";
import { AccountListItemComponent } from "./account-list-item.component";
import { AccountPaginatedListComponent } from "./account-paginated-list.component";

const declarables = [
    AccountEditPageComponent,
    AccountListItemComponent,
    AccountPaginatedListComponent
];

const providers = [AccountsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class AccountsModule { }
