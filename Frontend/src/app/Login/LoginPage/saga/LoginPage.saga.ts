import { put } from "redux-saga/effects";

import { HttpResponse } from "@utils/http/HttpResponse";
import { AppAction } from "@utils/types/AppAction";
import { AuthController } from "@api/AuthController";
import { LoginDTO } from "@models/auth/LoginDTO";
import { urls } from "@app/PageComponentRouter";
import { history } from "@app/App";
import { LayoutActions } from "@app/Layout/redux/Layout.actions";
import { AuthJwtTokenDTO } from "@models/auth/AuthJwtTokenDTO";

export const sessionStorageKeys = {
    jwtToken: "shared-auth-token"
};

export class LoginPageSaga {
    public static * updateJwtToken(token: string) {
        sessionStorage.setItem(sessionStorageKeys.jwtToken, token);
    }

    public static getJwtToken() {
        return sessionStorage.getItem(sessionStorageKeys.jwtToken);
    }

    public static *login(action: AppAction<LoginDTO>) {
        const response: HttpResponse<
            AuthJwtTokenDTO
        > = yield AuthController.postLogin(action.payload);
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        yield LoginPageSaga.updateJwtToken(response.data.token);

        yield put(LayoutActions.load());

        history.push(urls.rootPath);
    }
}
