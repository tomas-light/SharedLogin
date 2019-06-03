import { combineReducers } from "redux";
import { connectRouter } from "connected-react-router";
import { History } from "history";

import { LayoutReducer } from "@app/Layout/redux/Layout.reducer";
import { Reducers } from "@reducers";
import { UsersReducer } from "@app/Main/MainPage/redux/Users.reducer";
import { NotifierReducer } from "../../shared/Notifier/redux/Notifier.reducer";

export function createReducers(history: History) {
    return combineReducers<Reducers>({
        router: connectRouter(history),
        layoutStore: LayoutReducer,
        usersStore: UsersReducer,
        notifierStore: NotifierReducer
    });
}
