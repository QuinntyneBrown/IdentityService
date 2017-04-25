import { fetch } from "../utilities";
import { Account } from "./account.model";
import { environment } from "../environment";

export class IdentityService {
    constructor(private _fetch = fetch) { }

    private static _instance: IdentityService;

    public static get Instance() {
        this._instance = this._instance || new IdentityService();
        return this._instance;
    }

    public get(): Promise<Array<Account>> {
        return this._fetch({ url: `${environment.baseUrl}api/account/get`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { accounts: Array<Account> }).accounts;
        });
    }

    public getById(id): Promise<Account> {
        return this._fetch({ url: `${environment.baseUrl}api/account/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { account: Account }).account;
        });
    }

    public add(account) {
        return this._fetch({ url: `${environment.baseUrl}api/account/add`, method: `POST`, data: { account }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `${environment.baseUrl}api/account/remove?id=${options.id}`, method: `DELETE`, authRequired: true  });
    }
    
}
