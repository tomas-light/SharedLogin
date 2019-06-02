import { select } from "redux-saga/effects";
import { Reducers } from "@reducers";
import { LayoutStore } from "@app/Layout/redux/LayoutStore";

export class LayoutStoreSelectors {
    public static *getStore() {
        const getItems = state => state;
        const stores: Reducers = yield select(getItems);
        return stores.layoutStore;
    }

    public static *getAccessibleAccounts() {
        const store: LayoutStore = yield LayoutStoreSelectors.getStore();
        return store.accessibleAccounts;
    }
}
