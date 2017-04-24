import { Subscription } from "./subscription.model";
import { EditorComponent } from "../shared";
import { SubscriptionDelete, SubscriptionEdit, SubscriptionAdd } from "./subscription.actions";
import { FeatureService, Feature } from "../features";
import { IdentityService, Account } from "../accounts";

const template = require("./subscription-edit-embed.component.html");
const styles = require("./subscription-edit-embed.component.scss");

export class SubscriptionEditEmbedComponent extends HTMLElement {
    constructor(
        private _IdentityService: IdentityService = IdentityService.Instance,
        private _featureService: FeatureService = FeatureService.Instance
    ) {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
    }

    static get observedAttributes() {
        return [
            "subscription",
            "subscription-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {

        var resultsArray = await Promise.all([
            this._IdentityService.get(),
            this._featureService.get()
        ]);

        rome(this._effectiveDateInputElement);
        rome(this._expiresOnInputElement);
        
        this.accounts = resultsArray[0];
        this.features = resultsArray[1];
        
        for (let i = 0; i < this.accounts.length; i++) {
            let option = document.createElement("option");
            option.textContent = `${this.accounts[i].firstname}, ${this.accounts[i].lastname}`;
            option.value = this.accounts[i].id;
            this._accountSelectElement.appendChild(option);
        }

        for (let i = 0; i < this.features.length; i++) {
            let option = document.createElement("option");
            option.textContent = this.features[i].name;
            option.value = this.features[i].id;
            this._featureSelectElement.appendChild(option);
        }

        this._titleElement.textContent = this.subscription ? `Edit Subscription: ${this.subscription.name}`: "Create Subscription";

        if (this.subscription) {                
            this._nameInputElement.value = this.subscription.name;  
            this._accountSelectElement.selectedIndex = this.subscription.accountId;
            this._featureSelectElement.selectedIndex = this.subscription.featureId;
            this._effectiveDateInputElement.value = this.subscription.effectiveDate;
            this._expiresOnInputElement.value = this.subscription.expiresOn;
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
        const subscription = {
            id: this.subscription != null ? this.subscription.id : null,
            name: this._nameInputElement.value,
            featureId: this._featureSelectElement.value,
            accountId: this._accountSelectElement.value,
            effectiveDate: this._effectiveDateInputElement.value,
            expiresOn: this._expiresOnInputElement.value
        } as Subscription;
        
        this.dispatchEvent(new SubscriptionAdd(subscription));            
    }

    public onCreate() {        
        this.dispatchEvent(new SubscriptionEdit(new Subscription()));            
    }

    public onDelete() {        
        const subscription = {
            id: this.subscription != null ? this.subscription.id : null,
            name: this._nameInputElement.value
        } as Subscription;

        this.dispatchEvent(new SubscriptionDelete(subscription));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "subscription-id":
                this.subscriptionId = newValue;
                break;
            case "subscription":
                this.subscription = JSON.parse(newValue);
                if (this.parentNode) {
                    this.subscriptionId = this.subscription.id;
                    this._nameInputElement.value = this.subscription.name != undefined ? this.subscription.name : "";
                    this._featureSelectElement.value = this.subscription.featureId;
                    this._accountSelectElement.value = this.subscription.accountId;
                    this._effectiveDateInputElement.value = this.subscription.effectiveDate != undefined ? this.subscription.effectiveDate : "";
                    this._expiresOnInputElement.value = this.subscription.expiresOn != undefined ? this.subscription.expiresOn : "";
                    this._titleElement.textContent = this.subscriptionId ? `Edit Subscription: ${this.subscription.name}` : "Create Subscription";
                }
                break;
        }           
    }

    public subscriptionId: any;
    
    public subscription: Subscription;

    public features: Array<Feature> = [];

    public accounts: Array<Account> = [];

    private get _accountSelectElement(): HTMLSelectElement { return this.querySelector(".subscription-account") as HTMLSelectElement; }

    private get _featureSelectElement(): HTMLSelectElement { return this.querySelector(".subscription-feature") as HTMLSelectElement; }
    
    private get _createButtonElement(): HTMLElement { return this.querySelector(".subscription-create") as HTMLElement; }
    
	private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    
	private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    
	private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".subscription-name") as HTMLInputElement; }

    private get _effectiveDateInputElement(): HTMLInputElement { return this.querySelector(".subscription-effective-date") as HTMLInputElement; }

    private get _expiresOnInputElement(): HTMLInputElement { return this.querySelector(".subscription-expires-on") as HTMLInputElement; }
}

customElements.define(`ce-subscription-edit-embed`,SubscriptionEditEmbedComponent);
