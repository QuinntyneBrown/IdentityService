import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "../shared/shared.module";

import { TenantsService } from "./tenants.service";

import { TenantEditComponent } from "./tenant-edit.component";
import { TenantListItemComponent } from "./tenant-list-item.component";
import { TenantPaginatedListComponent } from "./tenant-paginated-list.component";

const declarables = [
    TenantEditComponent,
    TenantListItemComponent,
    TenantPaginatedListComponent
];

const providers = [TenantsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule, SharedModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class TenantsModule { }
