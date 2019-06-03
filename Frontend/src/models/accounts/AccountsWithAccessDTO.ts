import { AccountDTO } from "@models/accounts/AccountDTO";

export class AccountsWithAccessDTO {
    public accounts: AccountDTO[];

    constructor() {
        this.accounts = [];
    }
}
