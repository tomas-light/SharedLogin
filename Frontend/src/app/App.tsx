import * as React from "react";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";

import withTheme from "@material-ui/core/styles/withTheme";

import { PageComponentRouter } from "./PageComponentRouter";
import { configureApp } from "../config/configureApp";

const { store, history } = configureApp();

interface IAppProps {}

type Props = IAppProps;

class State {}

class App extends React.Component<Props, State> {
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