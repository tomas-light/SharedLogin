import { select } from "redux-saga/effects";
import { Reducers } from "@reducers";
import { SessionStore } from "@config/redux/SessionStore/SessionStore";

export class SessionStoreSelectors {
    public static *getStore() {
        const getItems = state => state;
        const stores: Reducers = yield select(getItems);
        return stores.layoutStore;
    }

    public static *getAuthentificationToken() {
        const store: SessionStore = yield SessionStoreSelectors.getStore();
        return store.jwtToken;
    }
}
