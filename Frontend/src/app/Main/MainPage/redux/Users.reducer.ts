import { AppAction } from "@utils/types/AppAction";
import { UsersStore } from "@app/Main/MainPage/redux/Users.store";
import { UsersActions } from "@app/Main/MainPage/redux/Users.actions";

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
