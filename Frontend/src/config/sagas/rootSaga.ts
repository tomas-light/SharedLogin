import { SagaMiddleware } from "redux-saga";

import { LoginPageWatcher } from "@app/Login/LoginPage/saga/LoginPage.watcher";
import { LayoutWatcher } from "@app/Layout/saga/Layout.watcher";

export function rootSaga(moddleware: SagaMiddleware) {
    const sagaWatchers = [LoginPageWatcher.wathcers, LayoutWatcher.wathcers];
    sagaWatchers.forEach(sagaWatchers => {
        sagaWatchers.forEach(watcher => moddleware.run(watcher));
    });
}
