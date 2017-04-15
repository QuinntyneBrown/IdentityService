import { Subscription } from "./subscription.model";

export const subscriptionActions = {
    ADD: "[Subscription] Add",
    EDIT: "[Subscription] Edit",
    DELETE: "[Subscription] Delete",
    SUBSCRIPTIONS_CHANGED: "[Subscription] Subscriptions Changed"
};

export class SubscriptionEvent extends CustomEvent {
    constructor(eventName:string, subscription: Subscription) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { subscription }
        });
    }
}

export class SubscriptionAdd extends SubscriptionEvent {
    constructor(subscription: Subscription) {
        super(subscriptionActions.ADD, subscription);        
    }
}

export class SubscriptionEdit extends SubscriptionEvent {
    constructor(subscription: Subscription) {
        super(subscriptionActions.EDIT, subscription);
    }
}

export class SubscriptionDelete extends SubscriptionEvent {
    constructor(subscription: Subscription) {
        super(subscriptionActions.DELETE, subscription);
    }
}

export class SubscriptionsChanged extends CustomEvent {
    constructor(subscriptions: Array<Subscription>) {
        super(subscriptionActions.SUBSCRIPTIONS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { subscriptions }
        });
    }
}
