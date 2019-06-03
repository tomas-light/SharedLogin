import * as React from "react";

import { Grid, Theme } from "@material-ui/core";
import Avatar from "@material-ui/core/Avatar";
import Typography from "@material-ui/core/Typography";
import makeStyles from "@material-ui/core/styles/makeStyles";
import createStyles from "@material-ui/core/styles/createStyles";

import { AccountDTO } from "@models/accounts/AccountDTO";
import { GreenCheckIcon } from "../../../../shared/icons/GreenCheckIcon";
import Tooltip from "@material-ui/core/Tooltip";
import IconButton from "@material-ui/core/IconButton";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        root: {},
        textContainer: {
            padding: "0 16px",
            flex: 1
        },
        name: {
            fontSize: 14,
            whiteSpace: "nowrap"
        },
        roleName: {
            fontSize: 12,
            whiteSpace: "nowrap"
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
        <Grid container className={classes.root} justify={"center"}>
            <Grid item>
                <Avatar src={account.avatar} />
            </Grid>

            <Grid
                item
                container
                direction={"column"}
                className={classes.textContainer}
            >
                <Typography
                    variant="caption"
                    component={"p"}
                    className={classes.name}
                >
                    {account.name}
                </Typography>
                <Typography
                    variant="caption"
                    component={"p"}
                    className={classes.roleName}
                >
                    {account.roleName}
                </Typography>
            </Grid>

            <Grid item>{renderIcon()}</Grid>
        </Grid>
    );

    function renderIcon() {
        if (isActiveAccount) {
            return (
                <Tooltip title={"Activated account"}>
                    <IconButton>
                        <GreenCheckIcon />
                    </IconButton>
                </Tooltip>
            );
        }

        if (icon) {
            return icon;
        }
    }
};

export { AccountItem };
