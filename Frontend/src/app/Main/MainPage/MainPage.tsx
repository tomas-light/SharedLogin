import * as React from "react";

import Grid from "@material-ui/core/Grid";

import ActivatedAccountContainer from "@app/Main/MainPage/ActivatedAccount/ActivatedAccount.container";
import { Callback } from "@utils/types/Callback";
import UsersContainer from "@app/Main/MainPage/Users/Users.container";

export interface IMainPageProps {}

export interface IMainPageCallProps {
    loadUsers: Callback;
}

type Props = IMainPageProps & IMainPageCallProps;

export class MainPage extends React.Component<Props> {
    public componentDidMount(): void {
        this.props.loadUsers();
    }

    public render() {
        return (
            <Grid container>
                <Grid item>
                    <ActivatedAccountContainer />
                </Grid>

                <Grid item>
                    <UsersContainer />
                </Grid>
            </Grid>
        );
    }
}
