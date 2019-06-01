import axios, { AxiosError } from "axios";
import { HttpResponse } from "@utils/requests/HttpResponse";

export class Http {
    public static get<T = any>(url: string, isResolveResponse: boolean = true): Promise<T | HttpResponse<T>> {
        const promise = axios.get<T>(url);

        return isResolveResponse ? Http.resolvePromise(promise) : promise;
    }

    public static post<T = any>(url: string, data?: any, isResolveResponse: boolean = true): Promise<T | HttpResponse<T>> {
        const promise = axios.post<T>(url, data);

        return isResolveResponse ? Http.resolvePromise(promise) : promise;
    }

    public static put<T = any>(url: string, data?: any, isResolveResponse: boolean = true): Promise<T | HttpResponse<T>> {
        const promise = axios.put<T>(url, data);

        return isResolveResponse ? Http.resolvePromise(promise) : promise;
    }

    public static delete(url: string, isResolveResponse: boolean = true): Promise<HttpResponse> {
        const promise = axios.delete(url);

        return isResolveResponse ? Http.resolvePromise(promise) : promise;
    }

    private static resolvePromise<T>(promise: Promise<HttpResponse<T>>): Promise<HttpResponse<T>> {
        return promise
            .then(
                response => response
            )
            .catch(
                error => Http.extractErrorDataResponse(error)
            );
    }

    private static extractErrorDataResponse(error: AxiosError): HttpResponse {
        return {
            errorMessage: error.response.data
        };
    }
}