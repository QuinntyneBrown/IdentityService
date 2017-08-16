import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "../shared/shared.module";
import { RouterModule } from "@angular/router";
import { TenantsService } from "./tenants.service";

import { TenantEditComponent } from "./tenant-edit.component";
import { TenantListItemComponent } from "./tenant-list-item.component";
import { TenantPaginatedListComponent } from "./tenant-paginated-list.component";
import { TenantsLeftNavComponent } from "./tenants-left-nav.component";
import { SetTenantFormComponent } from "./set-tenant-form.component";

const declarables = [
    SetTenantFormComponent,
    TenantEditComponent,
    TenantListItemComponent,
    TenantPaginatedListComponent,
    TenantsLeftNavComponent
];

const providers = [TenantsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule, RouterModule, SharedModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class TenantsModule { }
