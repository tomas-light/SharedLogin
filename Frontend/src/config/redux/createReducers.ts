import { combineReducers } from "redux";
import { connectRouter } from "connected-react-router";
import { History } from "history";

import { LayoutStoreReducer } from "@app/Layout/redux/LayoutStore.reducer";
import { Reducers } from "@reducers";
import { SessionStoreReducer } from "./SessionStore/SessionStore.reducer";

export function createReducers(history: History) {
    return combineReducers<Reducers>({
        router: connectRouter(history),
        session: SessionStoreReducer,
        layoutStore: LayoutStoreReducer
    });
}
