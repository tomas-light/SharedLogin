import { SagaMiddleware } from "redux-saga";

import { LoginPageWatcher } from "@app/Login/LoginPage/saga/LoginPage.watcher";

export function rootSaga(moddleware: SagaMiddleware) {
    const sagaWatchers = [LoginPageWatcher.wathcers];
    sagaWatchers.forEach(sagaWatchers => {
        sagaWatchers.forEach(watcher => moddleware.run(watcher));
    });
}
