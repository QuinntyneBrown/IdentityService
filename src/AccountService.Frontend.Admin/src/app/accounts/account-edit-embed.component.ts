import { Account } from "./account.model";
import { EditorComponent } from "../shared";
import { AccountDelete, AccountEdit, AccountAdd } from "./account.actions";

const template = require("./account-edit-embed.component.html");
const styles = require("./account-edit-embed.component.scss");

export class AccountEditEmbedComponent extends HTMLElement {
    constructor() {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
    }

    static get observedAttributes() {
        return [
            "account",
            "account-id"
        ];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = (this.account && this.account.id) ? `Edit Account: ${this.account.name}`: "Create Account";

        if (this.account) {                
            this._nameInputElement.value = this.account.name;
            this._firstnameInputElement.value = this.account.firstname;
            this._lastnameInputElement.value = this.account.lastname;
            this._emailInputElement.value = this.account.email;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
    }

    public onSave() {
        const account = {
            id: this.account != null ? this.account.id : null,
            name: this._nameInputElement.value,
            firstname: this._firstnameInputElement.value,
            lastname: this._lastnameInputElement.value,
            email: this._emailInputElement.value
        } as Account;
        
        this.dispatchEvent(new AccountAdd(account));            
    }

    public onDelete() {        
        const account = {
            id: this.account != null ? this.account.id : null,
            name: this._nameInputElement.value
        } as Account;

        this.dispatchEvent(new AccountDelete(account));         
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "account-id":
                this.accountId = newValue;
                break;
            case "account":
                this.account = JSON.parse(newValue);
                if (this.parentNode) {
                    this.accountId = this.account.id;
                    this._nameInputElement.value = this.account.name != undefined ? this.account.name : "";
                    this._firstnameInputElement.value = this.account.firstname != undefined ? this.account.firstname : "";
                    this._lastnameInputElement.value = this.account.lastname != undefined ? this.account.lastname: "";
                    this._emailInputElement.value = this.account.email != undefined ? this.account.email : "";
                    this._titleElement.textContent = this.accountId ? `Edit Account: ${this.account.name}` : "Create Account";
                }
                break;
        }           
    }

    public accountId: any;

    public account: Account;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }

    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };

    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };

    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".account-name") as HTMLInputElement; }

    private get _firstnameInputElement(): HTMLInputElement { return this.querySelector(".account-firstname") as HTMLInputElement; }

    private get _lastnameInputElement(): HTMLInputElement { return this.querySelector(".account-lastname") as HTMLInputElement; }

    private get _emailInputElement(): HTMLInputElement { return this.querySelector(".account-email") as HTMLInputElement; }
}

customElements.define(`ce-account-edit-embed`,AccountEditEmbedComponent);
