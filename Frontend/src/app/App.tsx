import * as React from "react";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";
import { ToastContainer } from "react-toastify";

import withTheme from "@material-ui/core/styles/withTheme";

import { PageComponentRouter } from "./PageComponentRouter";
import { configureApp } from "../config/configureApp";
import {HttpInterceptor} from "@utils/http/HttpInterceptor";

export const { store, history } = configureApp();

interface IAppProps {}

type Props = IAppProps;

class State {}

class App extends React.Component<Props, State> {
    private httpInterceptor;

    public componentDidMount(): void {
        this.httpInterceptor = new HttpInterceptor(store);
    }

    public render() {
        return (
            <Provider store={store}>
                <ConnectedRouter history={history}>
                    <PageComponentRouter />
                </ConnectedRouter>
            </Provider>
        );
    }
}

const appWithTheme = withTheme(App);
export { appWithTheme as App }