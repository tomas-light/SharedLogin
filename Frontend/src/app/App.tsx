import * as React from "react";
import { Provider } from "react-redux";
import { ConnectedRouter } from "connected-react-router";
import { SnackbarProvider } from "notistack";

import withTheme from "@material-ui/core/styles/withTheme";

import { configureApp } from "@config/configureApp";
import { HttpInterceptor } from "@utils/http/HttpInterceptor";
import { PageComponentRouter } from "./PageComponentRouter";

export const { store, history } = configureApp();

new HttpInterceptor();

interface IAppProps {}

type Props = IAppProps;

class App extends React.Component<Props> {
    public render() {
        return (
            <SnackbarProvider
                maxSnack={3}
                autoHideDuration={2000}
                anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
            >
                <Provider store={store}>
                    <ConnectedRouter history={history}>
                        <PageComponentRouter />
                    </ConnectedRouter>
                </Provider>
            </SnackbarProvider>
        );
    }
}

const appWithTheme = withTheme(App);
export { appWithTheme as App };
