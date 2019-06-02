import * as React from "react";
import { Route, RouteComponentProps, Switch } from "react-router";
import { ToastContainer } from "react-toastify";
// import Loadable from 'react-loadable';

// import CircularProgress from "@material-ui/core/CircularProgress";

// const LoadingComponent = <CircularProgress />;
// const PageLogin = Loadable({
//     loader: () => import("./Login/LoginPage/LoginPage"),
//     loading: LoadingComponent
// });

import PageLogin from "./Login/LoginPage/LoginPage.container";
import Layout from "@app/Layout/Layout.container";
import MainPage from "@app/Main/MainPage/MainPage.container";

export const urls = {
    rootPath: "/",
    loginPath: "/login",
    registerPath: "/register",
    users: "/users",
    profile: "/profile",
    settings: "/settings"
};

export interface IPageComponentRouterProps {}

type Props = IPageComponentRouterProps;

const PageComponentRouter: React.FunctionComponent<Props> = props => {
    const {} = props;

    return (
        <Switch>
            <Route exact path={urls.loginPath} component={PageLogin} />
            <Layout>
                <Route exact path={urls.rootPath} component={MainPage}/>
                <ToastContainer />
            </Layout>
        </Switch>
    );
};

export { PageComponentRouter };
