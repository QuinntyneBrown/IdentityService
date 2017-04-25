import { fetch } from "../utilities";
import { Tenant } from "./tenant.model";
import { environment } from "../environment";

export class TenantService {
    constructor(private _fetch = fetch) { }

    private static _instance: TenantService;

    public static get Instance() {
        this._instance = this._instance || new TenantService();
        return this._instance;
    }

    public get(): Promise<Array<Tenant>> {
        return this._fetch({ url: `${environment.baseUrl}/api/tenant/get`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { tenants: Array<Tenant> }).tenants;
        });
    }

    public getById(id): Promise<Tenant> {
        return this._fetch({ url: `${environment.baseUrl}api/tenant/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { tenant: Tenant }).tenant;
        });
    }

    public add(tenant) {
        return this._fetch({ url: `${environment.baseUrl}api/tenant/add`, method: `POST`, data: { tenant }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `${environment.baseUrl}api/tenant/remove?id=${options.id}`, method: `DELETE`, authRequired: true  });
    }
    
}
