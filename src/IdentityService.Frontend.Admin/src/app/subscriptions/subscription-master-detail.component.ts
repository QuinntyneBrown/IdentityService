import { SubscriptionAdd, SubscriptionDelete, SubscriptionEdit, subscriptionActions } from "./subscription.actions";
import { Subscription } from "./subscription.model";
import { SubscriptionService } from "./subscription.service";

const template = require("./subscription-master-detail.component.html");
const styles = require("./subscription-master-detail.component.scss");

export class SubscriptionMasterDetailComponent extends HTMLElement {
    constructor(
        private _subscriptionService: SubscriptionService = SubscriptionService.Instance	
	) {
        super();
        this.onSubscriptionAdd = this.onSubscriptionAdd.bind(this);
        this.onSubscriptionEdit = this.onSubscriptionEdit.bind(this);
        this.onSubscriptionDelete = this.onSubscriptionDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "subscriptions"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.subscriptions = await this._subscriptionService.get();
        this.subscriptionListElement.setAttribute("subscriptions", JSON.stringify(this.subscriptions));
    }

    private _setEventListeners() {
        this.addEventListener(subscriptionActions.ADD, this.onSubscriptionAdd);
        this.addEventListener(subscriptionActions.EDIT, this.onSubscriptionEdit);
        this.addEventListener(subscriptionActions.DELETE, this.onSubscriptionDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(subscriptionActions.ADD, this.onSubscriptionAdd);
        this.removeEventListener(subscriptionActions.EDIT, this.onSubscriptionEdit);
        this.removeEventListener(subscriptionActions.DELETE, this.onSubscriptionDelete);
    }

    public async onSubscriptionAdd(e) {

        await this._subscriptionService.add(e.detail.subscription);
        this.subscriptions = await this._subscriptionService.get();
        
        this.subscriptionListElement.setAttribute("subscriptions", JSON.stringify(this.subscriptions));
        this.subscriptionEditElement.setAttribute("subscription", JSON.stringify(new Subscription()));
    }

    public onSubscriptionEdit(e) {
        this.subscriptionEditElement.setAttribute("subscription", JSON.stringify(e.detail.subscription));
    }

    public async onSubscriptionDelete(e) {

        await this._subscriptionService.remove(e.detail.subscription.id);
        this.subscriptions = await this._subscriptionService.get();
        
        this.subscriptionListElement.setAttribute("subscriptions", JSON.stringify(this.subscriptions));
        this.subscriptionEditElement.setAttribute("subscription", JSON.stringify(new Subscription()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "subscriptions":
                this.subscriptions = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Subscription> { return this.subscriptions; }

    private subscriptions: Array<Subscription> = [];
    public subscription: Subscription = <Subscription>{};
    public get subscriptionEditElement(): HTMLElement { return this.querySelector("ce-subscription-edit-embed") as HTMLElement; }
    public get subscriptionListElement(): HTMLElement { return this.querySelector("ce-subscription-list-embed") as HTMLElement; }
}

customElements.define(`ce-subscription-master-detail`,SubscriptionMasterDetailComponent);
