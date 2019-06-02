import { SagaMiddleware } from "redux-saga";

import { LoginPageWatcher } from "@app/Login/LoginPage/saga/LoginPage.watcher";
import { LayoutWatcher } from "@app/Layout/saga/Layout.watcher";
import { UsersWatcher } from "@app/Main/MainPage/saga/Users.wathcer";

export function rootSaga(sagaMiddleware: SagaMiddleware) {
    const sagaWatchers = [
        LoginPageWatcher.wathcers,
        LayoutWatcher.wathcers,
        UsersWatcher.wathcers
    ];
    sagaWatchers.forEach(sagaWatchers => {
        sagaWatchers.forEach(watcher => sagaMiddleware.run(watcher));
    });
}
