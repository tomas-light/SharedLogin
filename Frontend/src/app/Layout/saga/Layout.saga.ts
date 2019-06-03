import { put } from "redux-saga/effects";

import { HttpResponse } from "@utils/http/HttpResponse";
import { AppAction } from "@utils/types/AppAction";
import { AuthController } from "@api/AuthController";
import { urls } from "@app/PageComponentRouter";
import { history } from "@app/App";
import { AccountController } from "@api/AccountController";
import { ActivateAccountDTO } from "@models/accounts/ActivateAccountDTO";
import { LayoutActions } from "@app/Layout/redux/Layout.actions";
import { LayoutSelectors } from "@app/Layout/redux/Layout.selectors";
import { AccountDTO } from "@models/accounts/AccountDTO";
import { LayoutStore } from "@app/Layout/redux/Layout.store";
import { LoginPageSaga } from "@app/Login/LoginPage/saga/LoginPage.saga";
import { AuthJwtTokenDTO } from "@models/auth/AuthJwtTokenDTO";
import {AccountsInformationDTO} from "@models/accounts/AccountsInformationDTO";

export class LayoutSaga {
    public static *load(action: AppAction) {
        const token = yield LoginPageSaga.getJwtToken();
        if (!token) {
            const response: HttpResponse<
                AuthJwtTokenDTO
            > = yield AuthController.getMyToken();
            yield LoginPageSaga.updateJwtToken(response.data.token);
        }

        const accountResponse: HttpResponse<AccountsInformationDTO> =
            yield AccountController.getCurrentInformation();

        if (!accountResponse.data) {
            console.log(accountResponse.errorMessage);
            return;
        }

        yield put(
            LayoutActions.updateStore({
                authenticatedAccount: accountResponse.data.authenticatedAccount,
                activeAccount: accountResponse.data.activeAccount,
                accessibleAccounts: accountResponse.data.accessibleAccounts
            } as LayoutStore)
        );
    }

    public static *loadAccessibleAccounts(action: AppAction) {
        const response: HttpResponse = yield AuthController.postLogin(
            action.payload
        );
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        history.push(urls.rootPath);
    }

    public static *activateAccount(action: AppAction<ActivateAccountDTO>) {
        const response: HttpResponse<
            AuthJwtTokenDTO
        > = yield AccountController.postActivateAccount(action.payload);
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        yield LoginPageSaga.updateJwtToken(response.data.token);

        const accessibleAccounts: AccountDTO[] = yield LayoutSelectors.getAccessibleAccounts();
        const activeAccount = accessibleAccounts.find(
            account => account.id === action.payload.accountId
        );
        yield put(
            LayoutActions.updateStore({
                activeAccount
            } as LayoutStore)
        );

        history.push(urls.rootPath);
    }
}
