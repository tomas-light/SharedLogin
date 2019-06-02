import { call, CallEffect } from "redux-saga/effects";

import { Http } from "@utils/http/Http";
import {ActivateAccountDTO} from "@models/accounts/ActivateAccountDTO";

export class AccountController {
    public static getAuthenticatedAccount(): CallEffect {
        return call(Http.get, "api/account/auth");
    }

    public static getActiveAccount(): CallEffect {
        return call(Http.get, "api/account/active");
    }

    public static getCurrentInformation(): CallEffect {
        return call(Http.post, "api/account/get-info");
    }

    public static postActivateAccount(dto: ActivateAccountDTO): CallEffect {
        return call(Http.post, "api/account/activate", dto);
    }
}
