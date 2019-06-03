import { takeLatest, ForkEffect } from "redux-saga/effects";

import { LayoutActions } from "../redux/Layout.actions";
import { LayoutSaga } from "./Layout.saga";

export class LayoutWatcher {
    public static *watchLoad(): IterableIterator<ForkEffect> {
        yield takeLatest(
            LayoutActions.LOAD,
            LayoutSaga.load
        );
    }

    public static *watchLoadAccessibleAccounts(): IterableIterator<ForkEffect> {
        yield takeLatest(
            LayoutActions.LOAD_ACCESSIBLE_ACCOUNTS,
            LayoutSaga.loadAccessibleAccounts
        );
    }

    public static *watchActivateAccount(): IterableIterator<ForkEffect> {
        yield takeLatest(
            LayoutActions.ACTIVATE_ACCOUNT,
            LayoutSaga.activateAccount
        );
    }

    public static get wathcers(): Array<() => IterableIterator<ForkEffect>> {
        return [
            LayoutWatcher.watchLoad,
            LayoutWatcher.watchLoadAccessibleAccounts,
            LayoutWatcher.watchActivateAccount
        ];
    }
}
