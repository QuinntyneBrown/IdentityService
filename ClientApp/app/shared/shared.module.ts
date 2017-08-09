import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule } from "@angular/router";

import { HeaderComponent } from './components/header.component';
import { PagerComponent } from "./components/pager.component";
import { TenantInterceptor } from "./interceptors/tenant.interceptor";
import { EventHubService } from "./services/event-hub.service";
import { Dispatcher } from "./services/dispatcher";
import { ErrorService } from "./services/error.service";

import { TabContentComponent } from "./components/tab-content.component";
import { TabTitleComponent } from "./components/tab-title.component";
import { TabsComponent } from "./components/tabs.component";

const declarables = [
    HeaderComponent,
    PagerComponent,
    TabContentComponent,
    TabsComponent,
    TabTitleComponent
];

const providers = [
    EventHubService,
    Dispatcher,
    ErrorService,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: TenantInterceptor,
        multi: true
    }
];

@NgModule({
    imports: [CommonModule, HttpClientModule, RouterModule],
    exports: [declarables],
    declarations: [declarables],
    providers: providers
})
export class SharedModule { }