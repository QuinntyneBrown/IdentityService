import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {HttpModule} from '@angular/http';
import {HttpClientModule } from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import { AccountsModule } from "./accounts/accounts.module";
import { FeaturesModule } from "./features/features.module";
import { SubscriptionsModule } from "./subscriptions/subscriptions.module";
import { TenantsModule } from "./tenants/tenants.module";
import { UsersModule } from "./users/users.module";

import {SharedModule} from "../app/shared";

import {AppComponent} from './app.component';

import {
    RoutingModule,
    routedComponents
} from "./app.routing";

const declarables = [
    AppComponent,
    routedComponents
];

const providers = [];

@NgModule({
    imports: [
        RoutingModule,
        BrowserModule,
        HttpModule,
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,

        AccountsModule,
        FeaturesModule,
        SubscriptionsModule,
        TenantsModule,
        UsersModule,

        SharedModule
    ],
    providers: providers,
    declarations: [declarables],
    exports: [declarables],
    bootstrap: [AppComponent]
})
export class AppModule { }