import * as React from "react";
import { Provider } from "react-redux";

import { ConnectedRouter } from "connected-react-router";

import { PageComponentRouter } from "./PageComponentRouter";
import { configureApp } from "../config/configureApp";

const { store, history } = configureApp();

interface IAppProps {}

type Props = IAppProps;

class State {}

export class App extends React.Component<Props, State> {
    render() {
        return (
            <Provider store={store}>
                <ConnectedRouter history={history}>
                    <PageComponentRouter />
                </ConnectedRouter>
            </Provider>
        );
    }
}
