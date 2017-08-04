import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import "rxjs/add/observable/fromPromise";
import "rxjs/add/operator/map";
import { Subject } from "rxjs/Subject";
import { constants } from "../constants/constants";

declare var $: any;

@Injectable()
export class EventHubService {    
    public events: Observable<any>;
    
    public connect(): Promise<any> {        
        return new Promise((resolve) => {
            var connection = $.hubConnection(constants.HUB_URL);            
            var eventHub = connection.createHubProxy("eventHub");
            this.events = Observable.create((observer) => {
                eventHub.on("events", (value) => {
                    observer.next(value);
                });
            });
            connection.start().done(() => {
                resolve();
            });
        });
    }
}