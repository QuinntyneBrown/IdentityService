import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { User } from "./user.model";
import { Observable } from "rxjs";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class UsersService {
    constructor(
        private _errorService: ErrorService,
        private _httpClient: HttpClient)
    { }

    public add(options: { user: User, correlationId: string }) {
        return this._httpClient
            .post(`${this._baseUrl}/api/user/add`, options)
            .catch(this._errorService.catchErrorResponse);
    }

    public get(): Observable<{ users: Array<User> }> {
        return this._httpClient
            .get<{ users: Array<User> }>(`${this._baseUrl}/api/user/get`)
            .catch(this._errorService.catchErrorResponse);
    }

    public getById(options: { id: number }): Observable<User> {
        return this._httpClient
            .get<{user: User}>(`${this._baseUrl}/api/user/getById?id=${options.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public remove(options: { user: User, correlationId: string }) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/user/remove?id=${options.user.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public get _baseUrl() { return ""; }
}
