import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from "@angular/common/http";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AccountsModule } from "./accounts/accounts.module";
import { FeaturesModule } from "./features/features.module";
import { SharedModule } from "../app/shared";
import { SettingsModule } from "./settings/settings.module";
import { SubscriptionsModule } from "./subscriptions/subscriptions.module";
import { TenantsModule } from "./tenants/tenants.module";
import { UsersModule } from "./users/users.module";

import { AppComponent } from './app.component';

import {
    RoutingModule,
    routedComponents
} from "./app.routing";

const declarables = [
    AppComponent,
    routedComponents
];

@NgModule({
    imports: [        
        BrowserModule,        
        CommonModule,
        FormsModule,
        HttpModule,
        ReactiveFormsModule,
        RouterModule,
        
        AccountsModule,
        FeaturesModule,
        RoutingModule,
        SettingsModule,
        SharedModule,
        SubscriptionsModule,
        TenantsModule,
        UsersModule        
    ],
    declarations: [declarables],
    exports: [declarables],
    bootstrap: [AppComponent]
})
export class AppModule { }