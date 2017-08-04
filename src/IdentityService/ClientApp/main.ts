import {enableProdMode} from "@angular/core";
import {AppModule} from "./app";
import {environment} from "./app/environments/environment";
import {platformBrowserDynamic} from '@angular/platform-browser-dynamic';

if (environment.production) {
    enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
    .catch(err => console.error(err));