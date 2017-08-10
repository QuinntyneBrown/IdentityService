import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { TenantsService } from "./tenants.service";

import { TenantEditPageComponent } from "./tenant-edit-page.component";
import { TenantListItemComponent } from "./tenant-list-item.component";
import { TenantPaginatedListComponent } from "./tenant-paginated-list.component";

const declarables = [
    TenantEditPageComponent,
    TenantListItemComponent,
    TenantPaginatedListComponent
];

const providers = [TenantsService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class TenantsModule { }
