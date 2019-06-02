import { AppAction } from "@utils/types/AppAction";
import { SessionStore } from "./SessionStore";
import { getNewState } from "@utils/stores/getNewState";
import { SessionStoreActions } from "./SessionStore.actions";

export const SessionStoreReducer = (state: SessionStore = new SessionStore(), action: AppAction): SessionStore => {
    if (action.type === SessionStoreActions.SET_TOKEN) {
        return getNewState(state, action, nameof<SessionStore>(store => store.jwtToken));
    }
    
    return state;
};