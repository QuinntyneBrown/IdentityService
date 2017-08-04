import {Routes, RouterModule} from '@angular/router';
import {IncidentsEditPageComponent,IncidentsListPageComponent} from "./incidents";

export const routes: Routes = [
    {
        path: '',
        component: IncidentsListPageComponent,
        pathMatch:"full"
    },
    {
        path: 'incidents/create',
        component: IncidentsEditPageComponent
    },
    {
        path: 'incidents/:id',
        component:IncidentsEditPageComponent
    }
];

export const RoutingModule = RouterModule.forRoot([
    ...routes
]);

export const routedComponents = [
    IncidentsListPageComponent,
    IncidentsEditPageComponent
];