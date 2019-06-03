import { AppAction } from "@utils/types/AppAction";
import { HistoryStore } from "./History.store";
import { HistoryActions } from "./History.actions";

export const HistoryReducer = (
    state: HistoryStore = new HistoryStore(),
    action: AppAction
): HistoryStore => {
    switch (action.type) {
        case HistoryActions.UPDATE_STORE:
            return {
                ...state,
                ...action.payload
            };

        default:
            return state;
    }
};
