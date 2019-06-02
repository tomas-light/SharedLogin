import * as React from "react";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";

import withTheme from "@material-ui/core/styles/withTheme";

import { configureApp } from "@config/configureApp";
import { HttpInterceptor } from "@utils/http/HttpInterceptor";
import { PageComponentRouter } from "./PageComponentRouter";

export const { store, history } = configureApp();

new HttpInterceptor();

interface IAppProps {}

type Props = IAppProps;

class State {}

class App extends React.Component<Props, State> {
    // private httpInterceptor;

    public componentDidMount(): void {
        // this.httpInterceptor = new HttpInterceptor();
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
export { appWithTheme as App };
