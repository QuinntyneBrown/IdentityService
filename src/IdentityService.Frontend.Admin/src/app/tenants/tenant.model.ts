import { guid } from "../utilities";

export class Tenant { 
    public id:any = 0
    public name: string = "";
    public uniqueId: any = guid();
    public hostUrl: any = "";

    public fromJSON(data: any): Tenant {
        let tenant = new Tenant();
        tenant.name = data.name;
        tenant.uniqueId = data.uniqueId;
        tenant.hostUrl = data.hostUrl;
        return tenant;
    }
}
