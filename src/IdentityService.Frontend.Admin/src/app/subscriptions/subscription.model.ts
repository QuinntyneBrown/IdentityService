export class Subscription { 
    public id:any;

    public name: any;

    public accountId: any;

    public featureId: any;

    public expiresOn: any;

    public effectiveDate: any;

    public fromJSON(data: any): Subscription {

        let subscription = new Subscription();

        subscription.name = data.name;

        subscription.accountId = data.accountId;

        subscription.featureId = data.featureId;

        subscription.effectiveDate = data.effectiveDate;

        subscription.expiresOn = data.expiresOn;

        return subscription;
    }
}
