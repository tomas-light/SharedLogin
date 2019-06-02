import { createAction } from "@utils/actions/createAction";
import { UsersStore } from "@app/Main/MainPage/redux/Users.store";

export class UsersActions {
    private static PREFIX = "UsersStore_";

    public static UPDATE_STORE = UsersActions.PREFIX + "UPDATE_STORE";

    public static LOAD_USERS = UsersActions.PREFIX + "LOAD_USERS";
    public static ADD_ACCESS = UsersActions.PREFIX + "ADD_ACCESS";
    public static REMOVE_ACCESS = UsersActions.PREFIX + "REMOVE_ACCESS";

    public static updateStore = (state: UsersStore) =>
        createAction(UsersActions.UPDATE_STORE, state);

    public static loadUsers = () => createAction(UsersActions.LOAD_USERS);
    public static addAccess = (accountId: string) => createAction(UsersActions.ADD_ACCESS, {accountId});
    public static removeAccess = (accountId: string) => createAction(UsersActions.REMOVE_ACCESS, accountId);
}
