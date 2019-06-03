import { call, CallEffect } from "redux-saga/effects";

import { Http } from "@utils/http/Http";

export class HistoryController {
    public static getAccessHistories(): CallEffect {
        return call(Http.get, "api/history/access");
    }
}
