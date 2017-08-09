import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { EventHubService } from "./event-hub.service";
import { Storage } from "./storage";

@Injectable()
export class Cache {
    constructor(private _httpClient: HttpClient, private _eventHubService: EventHubService, private _storage: Storage) {
        _eventHubService.events.subscribe(() => {
            //todo: check type of event
            this.cachedUrls.forEach((url) => this._storage.put({ name: url, value: null }));
        });
    }

    public fromCacheOrService(options: {url:string}) {
        return new Promise((resolve) => {
            const cachedData = this._storage.get({ name: options.url });
            if (!cachedData || !cachedData.value) {
                this._httpClient.get<any>(options.url)
                    .toPromise().then((results) => {
                        this._storage.put({ name: options.url, value: results });
                        resolve(results);
                    });
            } else {
                resolve(cachedData);
            }
        });        
    }

    public cachedUrls: Array<string> = [];
}
