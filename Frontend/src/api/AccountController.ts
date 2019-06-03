import { call, CallEffect } from "redux-saga/effects";

import { Http } from "@utils/http/Http";
import {ActivateAccountDTO} from "@models/accounts/ActivateAccountDTO";

export class AccountController {
    public static getCurrentInformation(): CallEffect {
        return call(Http.post, "api/account/get-info");
    }

    public static postActivateAccount(dto: ActivateAccountDTO): CallEffect {
        return call(Http.post, "api/account/activate", dto);
    }

    public static getUsers(): CallEffect {
        return call(Http.get, "api/account/users");
    }

    public static postAccess(dto: ActivateAccountDTO): CallEffect {
        return call(Http.post, "api/account/access", dto);
    }

    public static deleteAccess(accountId: string): CallEffect {
        return call(Http.delete, "api/account/access/" + accountId);
    }

    public static getAccountsThatHaveAccess(): CallEffect {
        return call(Http.get, "api/account/access");
    }
}
