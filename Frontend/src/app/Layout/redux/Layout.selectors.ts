import { select } from "redux-saga/effects";
import { Reducers } from "@reducers";
import { LayoutStore } from "@app/Layout/redux/Layout.store";

export class LayoutSelectors {
    public static *getStore() {
        const getItems = state => state;
        const stores: Reducers = yield select(getItems);
        return stores.layoutStore;
    }

    public static *getAccessibleAccounts() {
        const store: LayoutStore = yield LayoutSelectors.getStore();
        return store.accessibleAccounts;
    }
}
