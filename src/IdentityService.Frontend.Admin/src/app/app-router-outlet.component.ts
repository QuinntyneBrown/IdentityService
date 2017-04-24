import { RouterOutlet } from "./router";
import { AuthorizedRouteMiddleware } from "./users";

export class AppRouterOutletComponent extends RouterOutlet {
    constructor(el: any) {
        super(el);
    }

    connectedCallback() {
        this.setRoutes([
            { path: "/", name: "tenant-master-detail", authRequired: true },
            { path: "/tab/:tabIndex", name: "tenant-master-detail", authRequired: true },
            { path: "/tenant/edit/:tenantId/tab/:tabIndex", name: "tenant-master-detail", authRequired: true },
            { path: "/tenant/edit/:tenantId", name: "tenant-master-detail", authRequired: true },
            { path: "/accounts", name: "account-master-detail", authRequired: true },
            { path: "/subscriptions", name: "subscription-master-detail", authRequired: true },
            { path: "/features", name: "feature-master-detail", authRequired: true },
            { path: "/login", name: "login" },
            { path: "/error", name: "error" },
            { path: "*", name: "not-found" }
        ] as any);

        this.use(new AuthorizedRouteMiddleware());

        super.connectedCallback();
    }

}

customElements.define(`ce-app-router-oulet`, AppRouterOutletComponent);