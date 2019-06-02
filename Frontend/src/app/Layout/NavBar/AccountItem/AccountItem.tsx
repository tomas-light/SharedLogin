import * as React from "react";

import { Grid, Theme } from "@material-ui/core";
import Avatar from "@material-ui/core/Avatar";
import Typography from "@material-ui/core/Typography";
import makeStyles from "@material-ui/core/styles/makeStyles";
import createStyles from "@material-ui/core/styles/createStyles";

import { AccountDTO } from "@models/accounts/AccountDTO";
import {GreenCheckIcon} from "../../../../shared/icons/GreenCheckIcon";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        textGridItem: {
            width: "100%"
        },
        roleName: {
            fontSize: 14,
            whiteSpace: "nowrap"
        },
        chevron: {
            margin: "auto"
        }
    })
);

export interface IAccountItemOwnProps {
    account: AccountDTO;
    isActiveAccount?: boolean;
    icon: any;
}

type Props = IAccountItemOwnProps;

const AccountItem: React.FunctionComponent<Props> = props => {
    const { account, isActiveAccount, icon } = props;

    const classes = useStyles();

    return (
        <Grid container>
            <Grid item>
                <Avatar src={account.avatar} />
            </Grid>

            <Grid item container direction={"column"}>
                <Grid item className={classes.textGridItem}>
                    {account.name}
                </Grid>
                <Grid item className={classes.textGridItem}>
                    <Typography variant="caption" className={classes.roleName}>
                        {account.roleName}
                    </Typography>
                </Grid>
            </Grid>

            <Grid item>
                {renderIcon()}
            </Grid>
        </Grid>
    );

    function renderIcon() {
        if (isActiveAccount) {
            return (
                <GreenCheckIcon />
            );
        }

        if (icon) {
            return icon;
        }
    }
};

export { AccountItem };
