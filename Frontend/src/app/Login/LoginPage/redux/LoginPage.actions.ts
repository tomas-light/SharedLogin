import { createAction } from "@utils/actions/createAction";
import {LoginDTO} from "@models/auth/LoginDTO";

export class LoginPageActions {
    private static readonly PREFIX = "LoginPage_";

    public static readonly LOGIN = LoginPageActions.PREFIX + "LOGIN";

    public static login = (dto: LoginDTO) => createAction(LoginPageActions.LOGIN, dto);
}
