import { RouterState } from "connected-react-router";

import { LayoutStore } from "@app/Layout/redux/Layout.store";
import { UsersStore } from "@app/Main/MainPage/redux/Users.store";

export { createReducers } from "./createReducers";

export class Reducers {
    router: RouterState;
    layoutStore: LayoutStore;
    usersStore: UsersStore;
}
