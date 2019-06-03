import { createAction } from "@utils/actions/createAction";
import { HistoryStore } from "./History.store";

export class HistoryActions {
    private static PREFIX = "HistoryStore_";

    public static UPDATE_STORE = HistoryActions.PREFIX + "UPDATE_STORE";

    public static LOAD = HistoryActions.PREFIX + "LOAD";

    public static updateStore = (state: HistoryStore) =>
        createAction(HistoryActions.UPDATE_STORE, state);

    public static load = () => createAction(HistoryActions.LOAD);
}
