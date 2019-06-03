import { createAction } from "@utils/actions/createAction";
import { UsersStore } from "@app/Main/MainPage/redux/Users.store";

export class UsersActions {
    private static PREFIX = "UsersStore_";

    public static UPDATE_STORE = UsersActions.PREFIX + "UPDATE_STORE";

    public static LOAD_USERS = UsersActions.PREFIX + "LOAD_USERS";
    public static GRANT_ACCESS = UsersActions.PREFIX + "GRANT_ACCESS";
    public static RESTRICT_ACCESS = UsersActions.PREFIX + "RESTRICT_ACCESS";

    public static updateStore = (state: UsersStore) =>
        createAction(UsersActions.UPDATE_STORE, state);

    public static loadUsers = () => createAction(UsersActions.LOAD_USERS);
    public static grantAccess = (accountId: string) => createAction(UsersActions.GRANT_ACCESS, {accountId});
    public static restrictAccess = (accountId: string) => createAction(UsersActions.RESTRICT_ACCESS, accountId);
}
