import { AccountDTO } from "@models/accounts/AccountDTO";

export class UsersStore {
    public allUsers: AccountDTO[];
    public usersThatHaveAccess: AccountDTO[];

    constructor() {
        this.allUsers = [];
        this.usersThatHaveAccess = [];
    }
}
