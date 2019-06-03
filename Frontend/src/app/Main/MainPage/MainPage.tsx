import * as React from "react";

import Grid from "@material-ui/core/Grid/Grid";

import ActivatedAccountContainer from "@app/Main/MainPage/ActivatedAccount/ActivatedAccount.container";
import { Callback } from "@utils/types/Callback";
import UsersContainer from "@app/Main/MainPage/Users/Users.container";
import HistoriesContainer from "@app/Main/MainPage/Histories/Histories.container";

export interface IMainPageProps {}

export interface IMainPageCallProps {
    loadUsers: Callback;
    loadAccessHistories: Callback;
}

type Props = IMainPageProps & IMainPageCallProps;

class MainPage extends React.Component<Props> {
    public componentDidMount(): void {
        this.props.loadUsers();
        this.props.loadAccessHistories();
    }

    public render() {
        return (
            <Grid container>
                <Grid item>
                    <ActivatedAccountContainer />
                    <HistoriesContainer />
                </Grid>

                <Grid item>
                    <UsersContainer />
                </Grid>
            </Grid>
        );
    }
}

export { MainPage };
