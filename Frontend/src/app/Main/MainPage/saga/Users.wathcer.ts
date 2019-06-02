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

    public static *watchAddAccess(): IterableIterator<ForkEffect> {
        yield takeLatest(
            UsersActions.ADD_ACCESS,
            UsersSaga.addAccess
        );
    }

    public static *watchRemoveAccess(): IterableIterator<ForkEffect> {
        yield takeLatest(
            UsersActions.REMOVE_ACCESS,
            UsersSaga.removeAccess
        );
    }

    public static get wathcers(): Array<() => IterableIterator<ForkEffect>> {
        return [
            UsersWatcher.watchLoadUsers,
            UsersWatcher.watchAddAccess,
            UsersWatcher.watchRemoveAccess
        ];
    }
}
