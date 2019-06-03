import { RouterState } from "connected-react-router";

import { LayoutStore } from "@app/Layout/redux/Layout.store";
import { UsersStore } from "@app/Main/MainPage/redux/Users.store";
import { NotifierStore } from "../../shared/Notifier/redux/Notifier.store";

export { createReducers } from "./createReducers";

export class Reducers {
    router: RouterState;
    layoutStore: LayoutStore;
    usersStore: UsersStore;
    notifierStore: NotifierStore;
}
