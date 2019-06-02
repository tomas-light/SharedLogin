import { LayoutStore } from "@app/Layout/redux/LayoutStore";
import { AppAction } from "@utils/types/AppAction";
import { getNewState } from "@utils/stores/getNewState";
import {LayoutStoreActions} from "@app/Layout/redux/LayoutStore.actions";

export const LayoutStoreReducer = (
    state: LayoutStore = new LayoutStore(),
    action: AppAction
): LayoutStore => {
    switch (action.type) {
        case LayoutStoreActions.UPDATE_STORE:
            return {
                ...state,
                ...action.payload
            };

        case "asdqwe":
            return getNewState(
                state,
                action,
                nameof(() => state.activeAccount)
            );

        default:
            return state;
    }
};
