import { put } from "redux-saga/effects";

import { HttpResponse } from "@utils/http/HttpResponse";
import { AppAction } from "@utils/types/AppAction";
import { AccountController } from "@api/AccountController";
import { ActivateAccountDTO } from "@models/accounts/ActivateAccountDTO";
import { AccountDTO } from "@models/accounts/AccountDTO";
import { UsersDTO } from "@models/accounts/UsersDTO";
import { UsersActions } from "../redux/Users.actions";
import { UsersStore } from "../redux/Users.store";
import { UsersSelectors } from "../redux/Users.selectors";
import { NotifierActions } from "@shared/Notifier/redux/Notifier.actions";
import { INotification } from "@shared/Notifier/Notifier.interfaces";

export class UsersSaga {
    public static *loadUsers(action: AppAction) {
        const response: HttpResponse<UsersDTO> = yield AccountController.getUsers();
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        yield put(UsersActions.updateStore({
            allUsers: response.data.users,
            usersThatHaveAccess: response.data.usersThatHaveAccess
        } as UsersStore));
    }

    public static *grantAccess(action: AppAction<ActivateAccountDTO>) {
        const response: HttpResponse<ActivateAccountDTO> = yield AccountController.postAccess(action.payload);
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        const usersThatHaveAccess: AccountDTO[] = yield UsersSelectors.getAccountsThatHaveAccess();
        const allUsers: AccountDTO[] = yield UsersSelectors.getAllUsers();

        const user = allUsers.find(account => account.id === action.payload.accountId);
        const updatedUsersThatHaveAccess = [...usersThatHaveAccess];
        updatedUsersThatHaveAccess.push(user);

        yield put(
            UsersActions.updateStore({
                usersThatHaveAccess: updatedUsersThatHaveAccess
            } as UsersStore)
        );

        const notification: INotification = {
            message: `You granted access ${user.name} to your account`,
            options: {
                variant: "success"
            }
        };
        yield put(NotifierActions.enqueueSnackbar(notification));
    }

    public static *restrictAccess(action: AppAction<string>) {
        const response: HttpResponse = yield AccountController.deleteAccess(action.payload);
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        const usersThatHaveAccess: AccountDTO[] = yield UsersSelectors.getAccountsThatHaveAccess();
        const updatedUsersThatHaveAccess = usersThatHaveAccess.filter(
            account => account.id !== action.payload
        );

        yield put(
            UsersActions.updateStore({
                usersThatHaveAccess: updatedUsersThatHaveAccess
            } as UsersStore)
        );

        const allUsers: AccountDTO[] = yield UsersSelectors.getAllUsers();
        const user = allUsers.find(account => account.id === action.payload);

        const notification: INotification = {
            message: `You restricted access ${user.name} to your account`,
            options: {
                variant: "info",
            }
        };
        yield put(NotifierActions.enqueueSnackbar(notification));
    }
}
