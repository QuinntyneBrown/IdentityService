export class Subscription { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Subscription {
        let subscription = new Subscription();
        subscription.name = data.name;
        return subscription;
    }
}
