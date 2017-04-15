import { Tenant } from "./tenant.model";

export const tenantActions = {
    ADD: "[Tenant] Add",
    EDIT: "[Tenant] Edit",
    DELETE: "[Tenant] Delete",
    TENANTS_CHANGED: "[Tenant] Tenants Changed"
};

export class TenantEvent extends CustomEvent {
    constructor(eventName:string, tenant: Tenant) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { tenant }
        });
    }
}

export class TenantAdd extends TenantEvent {
    constructor(tenant: Tenant) {
        super(tenantActions.ADD, tenant);        
    }
}

export class TenantEdit extends TenantEvent {
    constructor(tenant: Tenant) {
        super(tenantActions.EDIT, tenant);
    }
}

export class TenantDelete extends TenantEvent {
    constructor(tenant: Tenant) {
        super(tenantActions.DELETE, tenant);
    }
}

export class TenantsChanged extends CustomEvent {
    constructor(tenants: Array<Tenant>) {
        super(tenantActions.TENANTS_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { tenants }
        });
    }
}
