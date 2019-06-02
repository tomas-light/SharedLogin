import { AccountDTO } from "@models/accounts/AccountDTO";

export class LayoutStore {
    authenticatedAccount: AccountDTO;
    activeAccount: AccountDTO;
    accessibleAccounts: AccountDTO[];

    constructor() {
        this.authenticatedAccount = new AccountDTO();
        this.activeAccount = new AccountDTO();
        this.accessibleAccounts = [];
    }
}
