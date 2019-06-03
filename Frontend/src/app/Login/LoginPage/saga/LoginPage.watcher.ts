import { takeLatest, ForkEffect } from "redux-saga/effects";

import { LoginPageActions } from "@app/Login/LoginPage/redux/LoginPage.actions";
import { LoginPageSaga } from "@app/Login/LoginPage/saga/LoginPage.saga";

export class LoginPageWatcher {
    public static *watchLogin(): IterableIterator<ForkEffect> {
        yield takeLatest(LoginPageActions.LOGIN, LoginPageSaga.login);
    }

    public static get wathcers(): Array<() => IterableIterator<ForkEffect>> {
        return [LoginPageWatcher.watchLogin];
    }
}
