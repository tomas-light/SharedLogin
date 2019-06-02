import { put } from "redux-saga/effects";

import { HttpResponse } from "@utils/http/HttpResponse";
import { AppAction } from "@utils/types/AppAction";
import { AuthController } from "@api/AuthController";
import { LoginDTO } from "@models/auth/LoginDTO";
import { urls } from "@app/PageComponentRouter";
import { history } from "@app/App";
import { AccountDTO } from "@models/accounts/AccountDTO";
import { LayoutStoreActions } from "@app/Layout/redux/LayoutStore.actions";
import { LayoutStore } from "@app/Layout/redux/LayoutStore";
import { AccountController } from "@api/AccountController";
import { AuthJwtTokenDTO } from "@models/auth/AuthJwtTokenDTO";
import { SessionStoreActions } from "@config/redux/SessionStore/SessionStore.actions";

export class LoginPageSaga {
    public static *login(action: AppAction<LoginDTO>) {
        const response: HttpResponse<
            AuthJwtTokenDTO
        > = yield AuthController.postLogin(action.payload);
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        yield put(SessionStoreActions.setToken(response.data.token));

        const accountResponse: HttpResponse<{
            authenticatedAccount: AccountDTO;
            activeAccount: AccountDTO;
            accessibleAccounts: AccountDTO[];
        }> = yield AccountController.getCurrentInformation();

        if (!accountResponse.data) {
            console.log(response.errorMessage);
            return;
        }

        yield put(LayoutStoreActions.updateStore({
            authenticatedAccount: accountResponse.data.authenticatedAccount,
            activeAccount: accountResponse.data.activeAccount,
            accessibleAccounts: accountResponse.data.accessibleAccounts
        } as LayoutStore));

        history.push(urls.rootPath);
    }
}
