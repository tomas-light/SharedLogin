import * as React from "react";
import { Route, RouteComponentProps, Switch } from "react-router";
// import Loadable from 'react-loadable';

// import CircularProgress from "@material-ui/core/CircularProgress";

import { Layout } from "./Layout/Layout";

// const LoadingComponent = <CircularProgress />;
// const PageLogin = Loadable({
//     loader: () => import("./Login/LoginPage/LoginPage"),
//     loading: LoadingComponent
// });

import PageLogin from "./Login/LoginPage/LoginPage.container";
import {ConnectedRouter} from "connected-react-router";
import {ToastContainer} from "react-toastify";

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
        <>
            <Switch>
                <Route exact path={urls.loginPath} component={PageLogin} />
                <Layout>
                    {/*
                    <Route exact path={urls.editBugPath}
                           component={(props: RouteComponentProps<{id: string}>) => <PageBugEditor/>}/>
                    */}
                    <ToastContainer />
                </Layout>
            </Switch>
        </>
    );
};

export { PageComponentRouter as PageComponentRouter}