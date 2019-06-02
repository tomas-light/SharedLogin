import { select } from "redux-saga/effects";
import { Reducers } from "@reducers";
import { UsersStore } from "@app/Main/MainPage/redux/Users.store";

export class UsersSelectors {
    public static *getStore() {
        const getItems = state => state;
        const stores: Reducers = yield select(getItems);
        return stores.usersStore;
    }

    public static *getAllUsers() {
        const store: UsersStore = yield UsersSelectors.getStore();
        return store.allUsers;
    }
}
