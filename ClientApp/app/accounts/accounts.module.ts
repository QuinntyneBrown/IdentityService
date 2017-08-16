import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "../shared/shared.module";
import { RouterModule } from "@angular/router";

import { AccountsService } from "./accounts.service";

import { AccountEditComponent } from "./account-edit.component";
import { AccountListItemComponent } from "./account-list-item.component";
import { AccountPaginatedListComponent } from "./account-paginated-list.component";
import { AccountsLeftNavComponent } from "./accounts-left-nav.component";

const declarables = [
    AccountEditComponent,
    AccountListItemComponent,
    AccountPaginatedListComponent,
    AccountsLeftNavComponent
];

const providers = [AccountsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule, RouterModule, SharedModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class AccountsModule { }
