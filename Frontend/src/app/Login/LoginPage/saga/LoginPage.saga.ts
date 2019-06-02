import { put } from "redux-saga/effects";

import { HttpResponse } from "@utils/http/HttpResponse";
import { AppAction } from "@utils/types/AppAction";
import { AuthController } from "@api/AuthController";
import { LoginDTO } from "@models/auth/LoginDTO";
import { urls } from "@app/PageComponentRouter";
import { history } from "@app/App";
import { LayoutStoreActions } from "@app/Layout/redux/LayoutStore.actions";
import { AuthJwtTokenDTO } from "@models/auth/AuthJwtTokenDTO";
import { SessionStoreActions } from "@config/redux/SessionStore/SessionStore.actions";

export const sessionStorageKeys = {
    jwtToken: "shared-auth-token"
};

export class LoginPageSaga {
    public static *login(action: AppAction<LoginDTO>) {
        const response: HttpResponse<
            AuthJwtTokenDTO
        > = yield AuthController.postLogin(action.payload);
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        // unnecessary store
        yield put(SessionStoreActions.setToken(response.data.token));

        sessionStorage.setItem(sessionStorageKeys.jwtToken, response.data.token);

        yield put(LayoutStoreActions.load());

        history.push(urls.rootPath);
    }
}
