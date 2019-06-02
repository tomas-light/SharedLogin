import { put } from "redux-saga/effects";

import { HttpResponse } from "@utils/http/HttpResponse";
import { AppAction } from "@utils/types/AppAction";
import { AccountController } from "@api/AccountController";
import { ActivateAccountDTO } from "@models/accounts/ActivateAccountDTO";
import { LayoutActions } from "@app/Layout/redux/Layout.actions";
import { LayoutSelectors } from "@app/Layout/redux/Layout.selectors";
import { AccountDTO } from "@models/accounts/AccountDTO";
import { LayoutStore } from "@app/Layout/redux/Layout.store";
import { UsersDTO } from "@models/accounts/UsersDTO";
import { UsersActions } from "@app/Main/MainPage/redux/Users.actions";
import { UsersStore } from "@app/Main/MainPage/redux/Users.store";
import {UsersSelectors} from "@app/Main/MainPage/redux/Users.selectors";

export class UsersSaga {
    public static *loadUsers(action: AppAction) {
        const response: HttpResponse<UsersDTO> = yield AccountController.getUsers();
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        yield put(UsersActions.updateStore({
            allUsers: response.data.users
        } as UsersStore));
    }

    public static *addAccess(action: AppAction<ActivateAccountDTO>) {
        const response: HttpResponse<ActivateAccountDTO> = yield AccountController.postAccess(action.payload);
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        const accessibleAccounts: AccountDTO[] = yield LayoutSelectors.getAccessibleAccounts();
        const allUsers: AccountDTO[] = yield UsersSelectors.getAllUsers();

        const user = allUsers.find(account => account.id === action.payload.accountId);
        const updatedAccessibleAccounts = [...accessibleAccounts];
        updatedAccessibleAccounts.push(user);

        yield put(
            LayoutActions.updateStore({
                accessibleAccounts: updatedAccessibleAccounts
            } as LayoutStore)
        );
    }

    public static *removeAccess(action: AppAction<string>) {
        const response: HttpResponse = yield AccountController.deleteAccess(action.payload);
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        const accessibleAccounts: AccountDTO[] = yield LayoutSelectors.getAccessibleAccounts();
        const updatedAccessibleAccounts = accessibleAccounts.filter(
            account => account.id !== action.payload
        );

        yield put(
            LayoutActions.updateStore({
                accessibleAccounts: updatedAccessibleAccounts
            } as LayoutStore)
        );
    }
}
