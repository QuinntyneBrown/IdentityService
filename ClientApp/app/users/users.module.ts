import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { UsersService } from "./users.service";

import { UserEditPageComponent } from "./user-edit-page.component";
import { UserListItemComponent } from "./user-list-item.component";
import { UserPaginatedListComponent } from "./user-paginated-list.component";

const declarables = [
    UserEditPageComponent,
    UserListItemComponent,
    UserPaginatedListComponent
];

const providers = [UsersService];

@NgModule({
    imports: [CommonModule, FormsModule, HttpClientModule, ReactiveFormsModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class UsersModule { }
