import { fetch } from "../utilities";
import { DigitalAsset } from "./digital-asset.model";
import { environment } from "../environment";

export class DigitalAssetService {
    
    private static _instance: DigitalAssetService;

    public static get Instance() {
        this._instance = this._instance || new this();
        return this._instance;
    }

    public get() {
        return fetch({ url: `${environment.baseUrl}api/digitalasset/get` });
    }

    public upload(options: {data: FormData}) {
        return fetch({
            url: `${environment.baseUrl}api/digitalasset/upload`,
            method: `POST`,
            headers: {},
            authRequired: true,
            data: options.data,
            isObjectData: true
        })
    }

    public getById(id) {
        return fetch({ url: `${environment.baseUrl}api/digitalasset/getbyid?id=${id}`, authRequired: true });
    }

    public add(digitalAsset) {
        return fetch({ url: `${environment.baseUrl}api/digitalasset/add`, method: `POST`, data: { digitalAsset }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return fetch({ url: `${environment.baseUrl}api/digitalasset/remove?id=${options.id}`, method: `DELETE`, authRequired: true  });
    }  
}