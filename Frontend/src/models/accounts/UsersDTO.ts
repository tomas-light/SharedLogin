import { AccountDTO } from "@models/accounts/AccountDTO";

export class UsersDTO {
    public users: AccountDTO[];

    constructor() {
        this.users = [];
    }
}
