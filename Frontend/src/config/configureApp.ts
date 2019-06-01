import { applyMiddleware, createStore, Store } from "redux";
import createSagaMiddleware, { SagaMiddleware } from "redux-saga";

import { routerMiddleware } from "connected-react-router";
import { createBrowserHistory, History } from "history";

import { createReducers } from "@reducers";
import { rootSaga } from "./sagas/rootSaga";

export function configureApp() {
    const history: History = createBrowserHistory();
    const sagaMiddleware: SagaMiddleware = createSagaMiddleware();
    const middleware = applyMiddleware(
        routerMiddleware(history),
        sagaMiddleware
    );
    const store = createStore(createReducers(history), middleware);
    rootSaga(sagaMiddleware);

    return { store, history };
}
