import { LayoutStore } from "@app/Layout/redux/Layout.store";
import { AppAction } from "@utils/types/AppAction";
import { LayoutActions } from "@app/Layout/redux/Layout.actions";

export const LayoutReducer = (
    state: LayoutStore = new LayoutStore(),
    action: AppAction
): LayoutStore => {
    switch (action.type) {
        case LayoutActions.UPDATE_STORE:
            return {
                ...state,
                ...action.payload
            };

        default:
            return state;
    }
};
