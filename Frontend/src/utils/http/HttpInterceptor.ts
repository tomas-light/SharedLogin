import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import { toast } from "react-toastify";

import { urls } from "@app/PageComponentRouter";
import { sessionStorageKeys } from "@app/Login/LoginPage/saga/LoginPage.saga";

export class HttpInterceptor {
    constructor() {
        axios.interceptors.request.use(
            this.requestFulfilled,
            this.requestRejected
        );

        axios.interceptors.response.use(
            this.responseFulfilled,
            this.responseRejected
        );
    }

    private requestFulfilled = (
        config: AxiosRequestConfig
    ): AxiosRequestConfig => {
        try {
            const jwtToken = sessionStorage.getItem(
                sessionStorageKeys.jwtToken
            );

            if (jwtToken) {
                config.headers.Authorization = jwtToken;
            } else {
                config.headers.Authorization = null;
            }
        } catch (e) {}

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

        // return Promise.reject(error);
        return Promise.resolve(error);
    };

    private showError(error: AxiosError) {
        const message = error.response.data;
        toast.error(message);
    }
}
