import { Router } from "../router";
import { BehaviorSubject } from "rxjs";

export class CurrentUser extends BehaviorSubject<any> {
    constructor(private _router: Router = Router.Instance) {
        super(null);
    }
    
    private static _instance: CurrentUser;

    public static get Instance() {
        this._instance = this._instance || new CurrentUser();
        return this._instance;
    }

    private _username: string;

    public get username(): string {
        return this._username;
    }

    public set username(value: string) {        
        this._username = value;
        this.next(CurrentUser.Instance);
    }
}