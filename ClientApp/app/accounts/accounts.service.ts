import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Account } from "./account.model";
import { Observable } from "rxjs";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class AccountsService {
    constructor(
        private _errorService: ErrorService,
        private _httpClient: HttpClient)
    { }

    public addOrUpdate(options: { account: Account, correlationId: string }) {
        return this._httpClient
            .post(`${this._baseUrl}/api/account/add`, options)
            .catch(this._errorService.catchErrorResponse);
    }

    public get(): Observable<{ accounts: Array<Account> }> {
        return this._httpClient
            .get<{ accounts: Array<Account> }>(`${this._baseUrl}/api/account/get`)
            .catch(this._errorService.catchErrorResponse);
    }

    public getById(options: { id: number }): Observable<{ account: Account }> {
        return this._httpClient
            .get<{account: Account}>(`${this._baseUrl}/api/account/getById?id=${options.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public remove(options: { account: Account, correlationId: string }) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/account/remove?id=${options.account.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public get _baseUrl() { return ""; }
}
