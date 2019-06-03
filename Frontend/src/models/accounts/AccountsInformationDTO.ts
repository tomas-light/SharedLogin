import {AccountDTO} from "@models/accounts/AccountDTO";

export class AccountsInformationDTO {
    authenticatedAccount: AccountDTO;
    activeAccount: AccountDTO;
    accessibleAccounts: AccountDTO[];
    
    constructor() {
        this.accessibleAccounts = [];
        this.activeAccount = new AccountDTO();
        this.authenticatedAccount = new AccountDTO();
    }
}