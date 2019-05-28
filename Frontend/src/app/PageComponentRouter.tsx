import * as React from "react";
import { Route, RouteComponentProps, Switch } from "react-router";
import loadable from "@loadable/component";

import { Layout } from "./Layout/Layout";

export const urls = {
    rootPath: "/",
    loginPath: "/#login",
    users: "/users",
    profile: "/profile",
    settings: "/settings"
};

export interface IPageComponentRouterProps {}

export class PageComponentRouter extends React.Component<
    IPageComponentRouterProps
> {
    private static LoadingComponent = <div>loading...</div>;

    /*
    private readonly PageLogin = loadable(
        () => import("../PageLogin/PageLogin"),
        {
            fallback: PageComponentRouter.LoadingComponent
        }
    );
    */

    render() {
        const {} = this.props;
        //const { PageLogin } = this;

        return (
            <Layout>
                <Switch>
                    {/*
                    <Route exact path={urls.loginPath} component={PageLogin} />
                    <Route exact path={urls.editBugPath}
                           component={(props: RouteComponentProps<{id: string}>) => <PageBugEditor/>}/>
                    */}
                </Switch>
            </Layout>
        );
    }
}
