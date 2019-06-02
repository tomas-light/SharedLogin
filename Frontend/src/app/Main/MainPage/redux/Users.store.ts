import { AccountDTO } from "@models/accounts/AccountDTO";

export class UsersStore {
    public allUsers: AccountDTO[];

    constructor() {
        this.allUsers = [];
    }
}
