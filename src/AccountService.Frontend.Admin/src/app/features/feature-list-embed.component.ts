import { Feature } from "./feature.model";

const template = require("./feature-list-embed.component.html");
const styles = require("./feature-list-embed.component.scss");

export class FeatureListEmbedComponent extends HTMLElement {
    constructor(
        private _document: Document = document
    ) {
        super();
    }


    static get observedAttributes() {
        return [
            "features"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
    }

    private async _bind() {        
        for (let i = 0; i < this.features.length; i++) {
            let el = this._document.createElement(`ce-feature-item-embed`);
            el.setAttribute("entity", JSON.stringify(this.features[i]));
            this.appendChild(el);
        }    
    }

    features:Array<Feature> = [];

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "features":
                this.features = JSON.parse(newValue);
                if (this.parentElement)
                    this.connectedCallback();
                break;
        }
    }
}

customElements.define("ce-feature-list-embed", FeatureListEmbedComponent);
