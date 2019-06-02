import { createAction } from "@utils/actions/createAction";
import { LayoutStore } from "@app/Layout/redux/Layout.store";

export class LayoutActions {
    private static PREFIX = "LayoutStore_";

    public static UPDATE_STORE = LayoutActions.PREFIX + "UPDATE_STORE";

    public static LOAD = LayoutActions.PREFIX + "LOAD";
    public static LOAD_ACCESSIBLE_ACCOUNTS =
        LayoutActions.PREFIX + "LOAD_ACCESSIBLE_ACCOUNTS";
    public static ACTIVATE_ACCOUNT =
        LayoutActions.PREFIX + "ACTIVATE_ACCOUNT";

    public static updateStore = (state: LayoutStore) =>
        createAction(LayoutActions.UPDATE_STORE, state);

    public static load = () => createAction(LayoutActions.LOAD);
    public static loadAccessibleAccounts = () =>
        createAction(LayoutActions.LOAD_ACCESSIBLE_ACCOUNTS);
    public static activateAccount = (accountId: string) =>
        createAction(LayoutActions.ACTIVATE_ACCOUNT, { accountId });
}
