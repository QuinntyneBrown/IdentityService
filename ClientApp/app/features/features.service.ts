import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Feature } from "./feature.model";
import { Observable } from "rxjs";
import { ErrorService } from "../shared/services/error.service";

@Injectable()
export class FeaturesService {
    constructor(
        private _errorService: ErrorService,
        private _httpClient: HttpClient)
    { }

    public add(entity: Feature) {
        return this._httpClient
            .post(`${this._baseUrl}/api/feature/add`, entity)
            .catch(this._errorService.catchErrorResponse);
    }

    public get(): Observable<{ features: Array<Feature> }> {
        return this._httpClient
            .get<{ features: Array<Feature> }>(`${this._baseUrl}/api/feature/get`)
            .catch(this._errorService.catchErrorResponse);
    }

    public getById(options: { id: number }): Observable<Feature> {
        return this._httpClient
            .get<{feature: Feature}>(`${this._baseUrl}/api/feature/getById?id=${options.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public remove(options: { feature: Feature, correlationId: string }) {
        return this._httpClient
            .delete(`${this._baseUrl}/api/feature/remove?id=${options.feature.id}`)
            .catch(this._errorService.catchErrorResponse);
    }

    public get _baseUrl() { return ""; }
}
