import {Routes, RouterModule} from '@angular/router';
import {LoginPageComponent} from "./users/login-page.component";
import {TenantPaginatedListPageComponent} from "./tenants/tenant-paginated-list-page.component";

export const routes: Routes = [
    {
        path: "",
        component: TenantPaginatedListPageComponent
    },
    {
        path: "login",
        component: LoginPageComponent
    }
];

export const RoutingModule = RouterModule.forRoot([
    ...routes
]);

export const routedComponents = [
    LoginPageComponent,
    TenantPaginatedListPageComponent
];