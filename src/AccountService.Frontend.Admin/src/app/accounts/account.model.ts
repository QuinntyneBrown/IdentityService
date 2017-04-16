export class Account { 
    public id:any;
    public name:string;
    public firstname: string;
    public lastname: string;
    public email: string;

    public fromJSON(data: any): Account {
        let account = new Account();
        account.name = data.name;
        account.firstname = data.firstname;
        account.lastname = data.lastname;
        account.email = data.email;
        return account;
    }
}
