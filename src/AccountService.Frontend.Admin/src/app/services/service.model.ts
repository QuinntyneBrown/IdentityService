export class Service { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Service {
        let service = new Service();
        service.name = data.name;
        return service;
    }
}
