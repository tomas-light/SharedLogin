import { HttpResponse } from "@utils/http/HttpResponse";
import { AppAction } from "@utils/types/AppAction";
import { AuthController } from "@api/AuthController";
import { urls } from "@app/PageComponentRouter";
import { history } from "@app/App";
import {AccountController} from "@api/AccountController";
import {ActivateAccountDTO} from "@models/accounts/ActivateAccountDTO";
import {LayoutStoreActions} from "@app/Layout/redux/LayoutStore.actions";
import {LayoutStoreSelectors} from "@app/Layout/redux/LayoutStore.selectors";
import {AccountDTO} from "@models/accounts/AccountDTO";
import {LayoutStore} from "@app/Layout/redux/LayoutStore";

export class LayoutSaga {
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
        const response: HttpResponse = yield AccountController.postActivateAccount(
            action.payload
        );
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        const accessibleAccounts: AccountDTO[] = yield LayoutStoreSelectors.getAccessibleAccounts();
        const activeAccount = accessibleAccounts.find(
            account => account.id === action.payload.accountId
        );
        yield LayoutStoreActions.updateStore({
            activeAccount
        } as LayoutStore);

        history.push(urls.rootPath);
    }
}
