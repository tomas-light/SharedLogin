import { HttpResponse } from "@utils/requests/HttpResponse";
import { AppAction } from "@utils/types/AppAction";
import { AuthController } from "@api/AuthController";
import { LoginDTO } from "@models/auth/LoginDTO";
import { urls } from "@app/PageComponentRouter";
import { history } from "@app/App";


export class LoginPageSaga {
    public static *login(action: AppAction<LoginDTO>) {
        const response: HttpResponse = yield AuthController.postLogin(action.payload);
        if (response.errorMessage) {
            console.log(response.errorMessage);
            return;
        }

        history.push(urls.rootPath);
    }
}
