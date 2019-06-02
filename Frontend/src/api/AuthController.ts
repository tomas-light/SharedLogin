import { call, CallEffect } from "redux-saga/effects";

import { Http } from "@utils/http/Http";
import { LoginDTO } from "@models/auth/LoginDTO";

export class AuthController {
    public static postLogin(dto: LoginDTO): CallEffect {
        return call(Http.post, "api/auth/login", dto);
    }

    public static postLogout(): CallEffect {
        return call(Http.post, "api/auth/logout");
    }

    public static getMyToken(): CallEffect {
        return call(Http.get, "api/auth/token");
    }
}
