import { put } from "redux-saga/effects";

import { HttpResponse } from "@utils/http/HttpResponse";
import { AppAction } from "@utils/types/AppAction";
import { HistoryController } from "@api/HistoryController";
import { AccessHistoriesDTO } from "@models/history/AccessHistoriesDTO";
import { HistoryActions } from "@app/Main/MainPage/Histories/redux/History.actions";
import { HistoryStore } from "@app/Main/MainPage/Histories/redux/History.store";
import {HistoryItem} from "@app/Main/MainPage/Histories/HistoryItem";
import {AccountDTO} from "@models/accounts/AccountDTO";
import {UsersSelectors} from "@app/Main/MainPage/Users/redux/Users.selectors";

export class HistorySaga {
    public static *load(action: AppAction) {
        const response: HttpResponse<AccessHistoriesDTO> =
            yield HistoryController.getAccessHistories();

        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        const allUsers: AccountDTO[] = yield UsersSelectors.getAllUsers();
        const histories: HistoryItem[] = response.data.histories.map(history => {
            const account = allUsers.find(user => user.id === history.userId);

            return {
                account,
                loginDateTime: history.loginDateTime,
                logoutDateTime: history.logoutDateTime
            };
        });

        yield put(HistoryActions.updateStore({
            histories: histories,
        } as HistoryStore));
    }
}
