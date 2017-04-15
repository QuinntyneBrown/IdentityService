import { Tenant } from "./tenant.model";

const template = require("./tenant-list-embed.component.html");
const styles = require("./tenant-list-embed.component.scss");

export class TenantListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "tenants"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.tenants.length; i++) {
            let el = this._document.createElement(`ce-tenant-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.tenants[i]));
            this.appendChild(el);
        }    
    }

    tenants:Array<Tenant> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "tenants":
                this.tenants = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-tenant-list-embed", TenantListEmbedComponent);
