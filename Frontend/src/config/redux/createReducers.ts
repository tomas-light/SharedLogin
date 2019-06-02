import { combineReducers } from "redux";
import { connectRouter } from "connected-react-router";
import { History } from "history";

import { LayoutReducer } from "@app/Layout/redux/Layout.reducer";
import { Reducers } from "@reducers";

export function createReducers(history: History) {
    return combineReducers<Reducers>({
        router: connectRouter(history),
        layoutStore: LayoutReducer
    });
}
