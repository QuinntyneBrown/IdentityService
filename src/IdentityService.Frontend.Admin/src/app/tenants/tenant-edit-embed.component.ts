import { Tenant } from "./tenant.model";
import { EditorComponent } from "../shared";
import {  TenantDelete, TenantEdit, TenantAdd } from "./tenant.actions";

const template = require("./tenant-edit-embed.component.html");
const styles = require("./tenant-edit-embed.component.scss");

export class TenantEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "tenant",
            "tenant-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = (this.tenant && this.tenant.id) ? `Edit Tenant: ${this.tenant.name}`: "Create Tenant";

        this._nameInputElement.value = this.tenant.name;
        this._hostUrlInputElement.value = this.tenant.hostUrl;
        this._uniqueIdInputElement.value = this.tenant.uniqueId; 

        if (!this.tenant.id)           
            this._deleteButtonElement.style.display = "none";  
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
        const tenant = {
            id: this.tenant != null ? this.tenant.id : null,
            name: this._nameInputElement.value,
            hostUrl: this._hostUrlInputElement.value,
            uniqueId: this._uniqueIdInputElement.value
        } as Tenant;
        
        this.dispatchEvent(new TenantAdd(tenant));            
    }

    public onCreate() {        
        this.dispatchEvent(new TenantEdit(new Tenant()));            
    }

    public onDelete() {        
        const tenant = {
            id: this.tenant != null ? this.tenant.id : null,
            name: this._nameInputElement.value,
            hostUrl: this._hostUrlInputElement.value,
            uniqueId: this._uniqueIdInputElement.value
        } as Tenant;

        this.dispatchEvent(new TenantDelete(tenant));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "tenant-id":
                this.tenantId = newValue;
                break;
            case "tenant":
                this.tenant = JSON.parse(newValue);
                if (this.parentNode) {
                    this.tenantId = this.tenant.id;
                    this._nameInputElement.value = this.tenant.name != undefined ? this.tenant.name : "";
                    this._hostUrlInputElement.value = this.tenant.hostUrl != undefined ? this.tenant.hostUrl : "";
                    this._uniqueIdInputElement.value = this.tenant.uniqueId != undefined ? this.tenant.uniqueId : "";
                    this._titleElement.textContent = this.tenantId ? `Edit Tenant: ${this.tenant.name}` : "Create Tenant";
                }
                break;
        }           
    }

    public tenantId: any;
    
    public tenant: Tenant = new Tenant();
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".tenant-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".tenant-name") as HTMLInputElement; }

    private get _uniqueIdInputElement(): HTMLInputElement { return this.querySelector(".tenant-unique-id") as HTMLInputElement; }

    private get _hostUrlInputElement(): HTMLInputElement { return this.querySelector(".tenant-host-url") as HTMLInputElement; }

}

customElements.define(`ce-tenant-edit-embed`,TenantEditEmbedComponent);
