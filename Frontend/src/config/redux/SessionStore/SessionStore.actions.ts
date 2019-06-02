import {createAction} from "@utils/actions/createAction";

export class SessionStoreActions {
    public static SET_TOKEN = "SESSION_SET_TOKEN";

    public static setToken = (jwtToken: string) =>
        createAction(SessionStoreActions.SET_TOKEN, {jwtToken});
}