import { TenantAdd, TenantDelete, TenantEdit, tenantActions } from "./tenant.actions";
import { Tenant } from "./tenant.model";
import { TenantService } from "./tenant.service";

const template = require("./tenant-master-detail.component.html");
const styles = require("./tenant-master-detail.component.scss");

export class TenantMasterDetailComponent extends HTMLElement {
    constructor(
        private _tenantService: TenantService = TenantService.Instance	
	) {
        super();
        this.onTenantAdd = this.onTenantAdd.bind(this);
        this.onTenantEdit = this.onTenantEdit.bind(this);
        this.onTenantDelete = this.onTenantDelete.bind(this);
    }

    static get observedAttributes () {
        return [
            "tenants"
        ];
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.tenants = await this._tenantService.get();
        this.tenantListElement.setAttribute("tenants", JSON.stringify(this.tenants));
    }

    private _setEventListeners() {
        this.addEventListener(tenantActions.ADD, this.onTenantAdd);
        this.addEventListener(tenantActions.EDIT, this.onTenantEdit);
        this.addEventListener(tenantActions.DELETE, this.onTenantDelete);
    }

    disconnectedCallback() {
        this.removeEventListener(tenantActions.ADD, this.onTenantAdd);
        this.removeEventListener(tenantActions.EDIT, this.onTenantEdit);
        this.removeEventListener(tenantActions.DELETE, this.onTenantDelete);
    }

    public async onTenantAdd(e) {

        await this._tenantService.add(e.detail.tenant);
        this.tenants = await this._tenantService.get();
        
        this.tenantListElement.setAttribute("tenants", JSON.stringify(this.tenants));
        this.tenantEditElement.setAttribute("tenant", JSON.stringify(new Tenant()));
    }

    public onTenantEdit(e) {
        this.tenantEditElement.setAttribute("tenant", JSON.stringify(e.detail.tenant));
    }

    public async onTenantDelete(e) {

        await this._tenantService.remove(e.detail.tenant.id);
        this.tenants = await this._tenantService.get();
        
        this.tenantListElement.setAttribute("tenants", JSON.stringify(this.tenants));
        this.tenantEditElement.setAttribute("tenant", JSON.stringify(new Tenant()));
    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            case "tenants":
                this.tenants = JSON.parse(newValue);

                if (this.parentNode)
                    this.connectedCallback();

                break;
        }
    }

    public get value(): Array<Tenant> { return this.tenants; }

    private tenants: Array<Tenant> = [];
    public tenant: Tenant = <Tenant>{};
    public get tenantEditElement(): HTMLElement { return this.querySelector("ce-tenant-edit-embed") as HTMLElement; }
    public get tenantListElement(): HTMLElement { return this.querySelector("ce-tenant-list-embed") as HTMLElement; }
}

customElements.define(`ce-tenant-master-detail`,TenantMasterDetailComponent);
