import { AppAction } from "@utils/types/AppAction";
import { UsersStore } from "./Users.store";
import { UsersActions } from "./Users.actions";

export const UsersReducer = (
    state: UsersStore = new UsersStore(),
    action: AppAction
): UsersStore => {
    switch (action.type) {
        case UsersActions.UPDATE_STORE:
            return {
                ...state,
                ...action.payload
            };

        default:
            return state;
    }
};
