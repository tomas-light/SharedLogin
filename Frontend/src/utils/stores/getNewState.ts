import { AppAction } from "@utils/types/AppAction";

export function getNewState<TStore>(
    state: TStore,
    action: AppAction,
    updatedProperty: string
): TStore {
    if (typeof(action.payload) !== "object") {
        return {
            ...state,
            [updatedProperty]: action.payload
        };
    }

    const propValue = action.payload[updatedProperty];
    if (propValue != undefined) {
        return {
            ...state,
            [updatedProperty]: propValue
        };
    }
}
