import { takeLatest, ForkEffect } from "redux-saga/effects";

import { UsersActions } from "../redux/Users.actions";
import { UsersSaga } from "./Users.saga";

export class UsersWatcher {
    public static *watchLoadUsers(): IterableIterator<ForkEffect> {
        yield takeLatest(
            UsersActions.LOAD_USERS,
            UsersSaga.loadUsers
        );
    }

    public static *watchGrantAccess(): IterableIterator<ForkEffect> {
        yield takeLatest(
            UsersActions.GRANT_ACCESS,
            UsersSaga.grantAccess
        );
    }

    public static *watchRestrictAccess(): IterableIterator<ForkEffect> {
        yield takeLatest(
            UsersActions.RESTRICT_ACCESS,
            UsersSaga.restrictAccess
        );
    }

    public static get wathcers(): Array<() => IterableIterator<ForkEffect>> {
        return [
            UsersWatcher.watchLoadUsers,
            UsersWatcher.watchGrantAccess,
            UsersWatcher.watchRestrictAccess
        ];
    }
}
