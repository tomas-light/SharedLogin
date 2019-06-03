import { HistoryItem } from "@app/Main/MainPage/Histories/HistoryItem";

export class HistoryStore {
    public histories: HistoryItem[];

    constructor() {
        this.histories = [];
    }
}
