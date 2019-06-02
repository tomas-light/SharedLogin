import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { Store } from "redux";

import { Reducers } from "@reducers";
import { urls } from "@app/PageComponentRouter";

export class HttpInterceptor {
    private store: Store<Reducers>;

    constructor(store: Store<Reducers>) {
        axios.interceptors.request.use(
            this.requestFulfilled,
            this.requestRejected
        );

        axios.interceptors.response.use(
            this.responseFulfilled,
            this.responseRejected
        );

        this.store = store;
    }

    private requestFulfilled = (config: AxiosRequestConfig): AxiosRequestConfig => {
        const jwtToken = this.store.getState().session.jwtToken;

        if (jwtToken) {
            config.headers.Authorization = jwtToken;
        }
        else {
            config.headers.Authorization = null;
        }

        return config;
    };

    private requestRejected = (error: AxiosError): Promise<any> => {
        return Promise.reject(error);
    };

    private responseFulfilled = (response: AxiosResponse): AxiosResponse => {
        return response;
    };

    private responseRejected = (error: AxiosError): Promise<any> => {
        switch (error.response.status) {
            case 401:
                window.location.pathname = urls.loginPath;
                break;

            case 400:
            default:
                this.showError(error);
                break;
        }

        return Promise.reject(error);
    };

    private showError(error: AxiosError) {
        const message = error.response.data;
        toast.error(message);
    }
}
