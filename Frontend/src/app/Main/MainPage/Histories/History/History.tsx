import * as React from "react";

import Avatar from "@material-ui/core/Avatar/Avatar";
import TableRow from "@material-ui/core/TableRow";
import TableCell from "@material-ui/core/TableCell";
import Typography from "@material-ui/core/Typography/Typography";
import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";

import { HistoryItem } from "@app/Main/MainPage/Histories/HistoryItem";
import { AccountDTO } from "@models/accounts/AccountDTO";

const useStyles = makeStyles((theme: Theme) =>
    createStyles({
        textContainer: {
            padding: "0 16px"
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

export interface IHistoryOwnProps {
    history: HistoryItem;
}

export interface IHistoryCallProps {
    // TODO: show history details
}

type Props = IHistoryOwnProps & IHistoryCallProps;

const History: React.FunctionComponent<Props> = props => {
    const { history } = props;
    const classes = useStyles();

    let account = history.account;
    if (!account) {
        account = new AccountDTO();
    }
    return (
        <TableRow>
            <TableCell component="th" scope="row">
                <Avatar src={account.avatar} />
            </TableCell>
            <TableCell>
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
            </TableCell>
            <TableCell>{history.loginDateTime.toString()}</TableCell>
            <TableCell>
                {history.logoutDateTime
                    ? history.logoutDateTime.toString()
                    : "-"}
            </TableCell>
        </TableRow>
    );
};

export { History };
