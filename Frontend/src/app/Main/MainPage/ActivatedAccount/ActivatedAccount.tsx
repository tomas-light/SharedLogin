import * as React from "react";
import { Paper } from "@material-ui/core";
import makeStyles from "@material-ui/core/styles/makeStyles";
import createStyles from "@material-ui/core/styles/createStyles";
import { AccountDTO } from "@models/accounts/AccountDTO";
import Avatar from "@material-ui/core/Avatar";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";

const useStyles = makeStyles(theme =>
    createStyles({
        paper: {
            maxWidth: 250,
            margin: 24,
            padding: 16
        }
    })
);

export interface IActivatedAccountProps {
    activatedAccount: AccountDTO;
}

export interface IActivatedAccountCallProps {}

type Props = IActivatedAccountProps & IActivatedAccountCallProps;

const ActivatedAccount: React.FunctionComponent<Props> = props => {
    const { activatedAccount } = props;
    const classes = useStyles();

    return (
        <Paper className={classes.paper}>
            <Grid container wrap="nowrap" spacing={2}>
                <Grid item>
                    <Avatar src={activatedAccount.avatar} />
                </Grid>
                <Grid item xs>
                    <Typography>Hello {activatedAccount.name}.</Typography>
                    <Typography color={"textSecondary"}>
                        Your role is {activatedAccount.roleName}.
                    </Typography>
                </Grid>
            </Grid>
        </Paper>
    );
};

export { ActivatedAccount as ActivatedAccount};
