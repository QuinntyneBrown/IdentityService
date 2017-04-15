import { fetch } from "../utilities";
import { Subscription } from "./subscription.model";

export class SubscriptionService {
    constructor(private _fetch = fetch) { }

    private static _instance: SubscriptionService;

    public static get Instance() {
        this._instance = this._instance || new SubscriptionService();
        return this._instance;
    }

    public get(): Promise<Array<Subscription>> {
        return this._fetch({ url: "/api/subscription/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { subscriptions: Array<Subscription> }).subscriptions;
        });
    }

    public getById(id): Promise<Subscription> {
        return this._fetch({ url: `/api/subscription/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { subscription: Subscription }).subscription;
        });
    }

    public add(subscription) {
        return this._fetch({ url: `/api/subscription/add`, method: "POST", data: { subscription }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/subscription/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}
