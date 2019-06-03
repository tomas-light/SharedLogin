import { AccountDTO } from "@models/accounts/AccountDTO";

export class UsersDTO {
    public users: AccountDTO[];
    public usersThatHaveAccess: AccountDTO[];

    constructor() {
        this.users = [];
        this.usersThatHaveAccess = [];
    }
}
