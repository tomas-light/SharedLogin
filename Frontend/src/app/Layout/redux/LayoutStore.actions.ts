import { createAction } from "@utils/actions/createAction";
import { LayoutStore } from "@app/Layout/redux/LayoutStore";

export class LayoutStoreActions {
    private static PREFIX = "LayoutStore_";

    public static UPDATE_STORE = LayoutStoreActions.PREFIX + "UPDATE_STORE";

    public static LOAD = LayoutStoreActions.PREFIX + "LOAD";
    public static LOAD_ACCESSIBLE_ACCOUNTS =
        LayoutStoreActions.PREFIX + "LOAD_ACCESSIBLE_ACCOUNTS";
    public static ACTIVATE_ACCOUNT =
        LayoutStoreActions.PREFIX + "ACTIVATE_ACCOUNT";

    public static updateStore = (state: LayoutStore) =>
        createAction(LayoutStoreActions.UPDATE_STORE, state);

    public static load = () => createAction(LayoutStoreActions.LOAD);
    public static loadAccessibleAccounts = () =>
        createAction(LayoutStoreActions.LOAD_ACCESSIBLE_ACCOUNTS);
    public static activateAccount = (accountId: string) =>
        createAction(LayoutStoreActions.ACTIVATE_ACCOUNT, { accountId });
}
