import { AccountDTO } from "@models/accounts/AccountDTO";

export class HistoryItem {
    public account: AccountDTO;
    public loginDateTime: Date;
    public logoutDateTime?: Date;
}
