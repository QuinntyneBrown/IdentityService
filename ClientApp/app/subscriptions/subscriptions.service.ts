import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Subscription } from "./subscription.model";
import { Observable } from "rxjs";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class SubscriptionsService {
    constructor(
        private _errorService: ErrorService,
        private _httpClient: HttpClient)
    { }

    public add(entity: Subscription) {
        return this._httpClient
            .post(`${this._baseUrl}/api/subscription/add`, entity)
            .catch(this._errorService.catchErrorResponse);
    }

    public get(): Observable<{ subscriptions: Array<Subscription> }> {
        return this._httpClient
            .get<{ subscriptions: Array<Subscription> }>(`${this._baseUrl}/api/subscription/get`)
            .catch(this._errorService.catchErrorResponse);
    }

    public getById(options: { id: number }): Observable<Subscription> {
        return this._httpClient
            .get<{subscription: Subscription}>(`${this._baseUrl}/api/subscription/getById?id=${options.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public remove(options: { subscription: Subscription, correlationId: string }) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/subscription/remove?id=${options.subscription.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public get _baseUrl() { return ""; }
}
