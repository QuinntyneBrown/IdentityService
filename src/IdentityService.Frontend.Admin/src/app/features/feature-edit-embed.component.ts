import { Feature } from "./feature.model";
import { EditorComponent } from "../shared";
import {  FeatureDelete, FeatureEdit, FeatureAdd } from "./feature.actions";

const template = require("./feature-edit-embed.component.html");
const styles = require("./feature-edit-embed.component.scss");

export class FeatureEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "feature",
            "feature-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.feature ? "Edit Feature": "Create Feature";

        if (this.feature) {                
            this._nameInputElement.value = this.feature.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._createButtonElement.addEventListener("click", this.onCreate);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._createButtonElement.removeEventListener("click", this.onCreate);
    }

    public onSave() {
        const feature = {
            id: this.feature != null ? this.feature.id : null,
            name: this._nameInputElement.value
        } as Feature;
        
        this.dispatchEvent(new FeatureAdd(feature));            
    }

    public onCreate() {        
        this.dispatchEvent(new FeatureEdit(new Feature()));            
    }

    public onDelete() {        
        const feature = {
            id: this.feature != null ? this.feature.id : null,
            name: this._nameInputElement.value
        } as Feature;

        this.dispatchEvent(new FeatureDelete(feature));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "feature-id":
                this.featureId = newValue;
                break;
            case "feature":
                this.feature = JSON.parse(newValue);
                if (this.parentNode) {
                    this.featureId = this.feature.id;
                    this._nameInputElement.value = this.feature.name != undefined ? this.feature.name : "";
                    this._titleElement.textContent = this.featureId ? "Edit Feature" : "Create Feature";
                }
                break;
        }           
    }

    public featureId: any;
    
	public feature: Feature;
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".feature-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
	private get _nameInputElement(): HTMLInputElement { return this.querySelector(".feature-name") as HTMLInputElement;}
}

customElements.define(`ce-feature-edit-embed`,FeatureEditEmbedComponent);
