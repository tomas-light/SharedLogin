import { takeLatest, ForkEffect } from "redux-saga/effects";

import { HistoryActions } from "../redux/History.actions";
import { HistorySaga } from "./History.saga";

export class HistoryWatcher {
    public static *watchLoad(): IterableIterator<ForkEffect> {
        yield takeLatest(
            HistoryActions.LOAD,
            HistorySaga.load
        );
    }

    public static get wathcers(): Array<() => IterableIterator<ForkEffect>> {
        return [
            HistoryWatcher.watchLoad,
        ];
    }
}
