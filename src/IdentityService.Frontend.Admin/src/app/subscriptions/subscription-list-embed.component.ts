import { Subscription } from "./subscription.model";

const template = require("./subscription-list-embed.component.html");
const styles = require("./subscription-list-embed.component.scss");

export class SubscriptionListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "subscriptions"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.subscriptions.length; i++) {
            let el = this._document.createElement(`ce-subscription-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.subscriptions[i]));
            this.appendChild(el);
        }    
    }

    subscriptions:Array<Subscription> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "subscriptions":
                this.subscriptions = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-subscription-list-embed", SubscriptionListEmbedComponent);
