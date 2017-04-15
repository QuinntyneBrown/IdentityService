import { FeatureAdd, FeatureDelete, FeatureEdit, featureActions } from "./feature.actions";
import { Feature } from "./feature.model";
import { FeatureService } from "./feature.service";

const template = require("./feature-master-detail.component.html");
const styles = require("./feature-master-detail.component.scss");

export class FeatureMasterDetailComponent extends HTMLElement {
    constructor(
        private _featureService: FeatureService = FeatureService.Instance	
	) {
        super();
        this.onFeatureAdd = this.onFeatureAdd.bind(this);
        this.onFeatureEdit = this.onFeatureEdit.bind(this);
        this.onFeatureDelete = this.onFeatureDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "features"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.features = await this._featureService.get();
        this.featureListElement.setAttribute("features", JSON.stringify(this.features));
    }

    private _setEventListeners() {
        this.addEventListener(featureActions.ADD, this.onFeatureAdd);
        this.addEventListener(featureActions.EDIT, this.onFeatureEdit);
        this.addEventListener(featureActions.DELETE, this.onFeatureDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(featureActions.ADD, this.onFeatureAdd);
        this.removeEventListener(featureActions.EDIT, this.onFeatureEdit);
        this.removeEventListener(featureActions.DELETE, this.onFeatureDelete);
    }

    public async onFeatureAdd(e) {

        await this._featureService.add(e.detail.feature);
        this.features = await this._featureService.get();
        
        this.featureListElement.setAttribute("features", JSON.stringify(this.features));
        this.featureEditElement.setAttribute("feature", JSON.stringify(new Feature()));
    }

    public onFeatureEdit(e) {
        this.featureEditElement.setAttribute("feature", JSON.stringify(e.detail.feature));
    }

    public async onFeatureDelete(e) {

        await this._featureService.remove(e.detail.feature.id);
        this.features = await this._featureService.get();
        
        this.featureListElement.setAttribute("features", JSON.stringify(this.features));
        this.featureEditElement.setAttribute("feature", JSON.stringify(new Feature()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "features":
                this.features = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Feature> { return this.features; }

    private features: Array<Feature> = [];
    public feature: Feature = <Feature>{};
    public get featureEditElement(): HTMLElement { return this.querySelector("ce-feature-edit-embed") as HTMLElement; }
    public get featureListElement(): HTMLElement { return this.querySelector("ce-feature-list-embed") as HTMLElement; }
}

customElements.define(`ce-feature-master-detail`,FeatureMasterDetailComponent);
