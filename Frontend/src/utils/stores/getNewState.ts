import { AppAction } from "@utils/types/AppAction";

export function getNewState<TStore>(
    state: TStore,
    action: AppAction,
    updatedProperty: string
): TStore {
    return {
        ...state,
        [updatedProperty]: action.payload
    };
}
