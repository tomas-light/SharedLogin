import { takeLatest, ForkEffect } from "redux-saga/effects";

import { LayoutStoreActions } from "../redux/LayoutStore.actions";
import { LayoutSaga } from "./Layout.saga";

export class LayoutWatcher {
    public static *watchLoadLoad(): IterableIterator<ForkEffect> {
        yield takeLatest(
            LayoutStoreActions.LOAD,
            LayoutSaga.load
        );
    }

    public static *watchLoadAccessibleAccounts(): IterableIterator<ForkEffect> {
        yield takeLatest(
            LayoutStoreActions.LOAD_ACCESSIBLE_ACCOUNTS,
            LayoutSaga.loadAccessibleAccounts
        );
    }

    public static *watchLoadActivateAccount(): IterableIterator<ForkEffect> {
        yield takeLatest(
            LayoutStoreActions.ACTIVATE_ACCOUNT,
            LayoutSaga.activateAccount
        );
    }

    public static get wathcers(): Array<() => IterableIterator<ForkEffect>> {
        return [
            LayoutWatcher.watchLoadLoad,
            LayoutWatcher.watchLoadAccessibleAccounts,
            LayoutWatcher.watchLoadActivateAccount
        ];
    }
}
