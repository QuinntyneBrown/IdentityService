import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Tenant } from "./tenant.model";
import { Observable } from "rxjs/Observable";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class TenantsService {
    constructor(
        private _errorService: ErrorService,
        private _httpClient: HttpClient)
    { }

    public addOrUpdate(options: { tenant: Tenant, correlationId: string }) {
        return this._httpClient
            .post(`${this._baseUrl}/api/tenants/add`, options)
            .catch(this._errorService.catchErrorResponse);
    }

    public get(): Observable<{ tenants: Array<Tenant> }> {
        return this._httpClient
            .get<{ tenants: Array<Tenant> }>(`${this._baseUrl}/api/tenants/get`)
            .catch(this._errorService.catchErrorResponse);
    }

    public getById(options: { id: number }): Observable<{ tenant: Tenant }> {
        return this._httpClient
            .get<{tenant: Tenant}>(`${this._baseUrl}/api/tenants/getById?id=${options.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public remove(options: { tenant: Tenant, correlationId: string }) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/tenants/remove?id=${options.tenant.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public get _baseUrl() { return ""; }
}
