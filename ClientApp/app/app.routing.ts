import { Routes, RouterModule } from '@angular/router';
import { TenantGuardService } from "./shared/guards/tenant-guard.service";
import { EventHubConnectionGuardService } from "./shared/guards/event-hub-connection-guard.service";
import { AuthGuardService } from "./shared/guards/auth-guard.service";
import { LoginPageComponent } from "./users/login-page.component";

import { TenantPaginatedListPageComponent } from "./tenants/tenant-paginated-list-page.component";
import { TenantEditPageComponent } from "./tenants/tenant-edit-page.component";
import { SetTenantPageComponent } from "./tenants/set-tenant-page.component";

import { UserPaginatedListPageComponent } from "./users/user-paginated-list-page.component";
import { UserEditPageComponent } from "./users/user-edit-page.component";
import { ChangePasswordPageComponent } from "./users/change-password-page.component";

const canActivate = [
    TenantGuardService,
    AuthGuardService,
    EventHubConnectionGuardService
];

export const routes: Routes = [
    {
        path: '',
        pathMatch:'full',
        component: TenantPaginatedListPageComponent,
        canActivate
    },
    {
        path: 'tenants',
        component: TenantPaginatedListPageComponent,
        canActivate
    },
    {
        path: 'tenants/create',
        component: TenantEditPageComponent,
        canActivate
    },
    {
        path: 'tenants/set',
        component: SetTenantPageComponent
    },
    {
        path: 'tenants/:id',
        component: TenantEditPageComponent,
        canActivate
    },
    {
        path: 'login',
        component: LoginPageComponent,
        canActivate: [
            TenantGuardService
        ]
    },
    {
        path: 'users',
        component: UserPaginatedListPageComponent,
        canActivate
    },
    {
        path: 'users/create',
        component: UserEditPageComponent,
        canActivate
    },
    {
        path: 'users/:id/changePassword',
        component: ChangePasswordPageComponent,
        canActivate
    },
    {
        path: 'users/:id',
        component: UserEditPageComponent,
        canActivate
    }
];

export const RoutingModule = RouterModule.forRoot([
    ...routes
]);

export const routedComponents = [
    LoginPageComponent,
    SetTenantPageComponent,
    TenantEditPageComponent,
    TenantPaginatedListPageComponent,
    UserEditPageComponent,
    UserPaginatedListPageComponent,
    ChangePasswordPageComponent
];