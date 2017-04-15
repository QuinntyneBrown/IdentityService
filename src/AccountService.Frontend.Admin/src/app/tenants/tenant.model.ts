export class Tenant { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Tenant {
        let tenant = new Tenant();
        tenant.name = data.name;
        return tenant;
    }
}
