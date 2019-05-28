import { SagaMiddleware } from "redux-saga";

// import {UsersSagaWatcher} from "@core/Users/PageUsers/redux/saga/UsersSaga.watcher";

export function rootSaga(moddleware: SagaMiddleware) {
    const sagaWatchers = [
        // UsersSagaWatcher.wathcers
    ];
    sagaWatchers.forEach(sagaWatchers => {
        sagaWatchers.forEach(watcher => moddleware.run(watcher));
    });
}
