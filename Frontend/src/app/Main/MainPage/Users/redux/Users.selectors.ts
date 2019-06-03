import { select } from "redux-saga/effects";
import { Reducers } from "@reducers";
import { UsersStore } from "./Users.store";

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

    public static *getAccountsThatHaveAccess() {
        const store: UsersStore = yield UsersSelectors.getStore();
        return store.usersThatHaveAccess;
    }
}
