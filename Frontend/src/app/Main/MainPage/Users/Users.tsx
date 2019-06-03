import * as React from "react";

import Paper from "@material-ui/core/Paper/Paper";
import { makeStyles, createStyles } from "@material-ui/core/styles";
import Table from "@material-ui/core/Table";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";

import { AccountDTO } from "@models/accounts/AccountDTO";
import UserTableRowContainer from "@app/Main/MainPage/Users/UserTableRow/UserTableRow.container";

const useStyles = makeStyles(theme =>
    createStyles({
        root: {
            width: "100%",
            margin: 24,
            overflowY: "auto"
        },
        table: {
        }
    })
);

export interface IUsersProps {
    authenticatedUser: AccountDTO;
    allUsers: AccountDTO[];
    usersThatHaveAccess: AccountDTO[];
}

type Props = IUsersProps;

const Users: React.FunctionComponent<Props> = props => {
    const {
        authenticatedUser,
        allUsers,
        usersThatHaveAccess
    } = props;

    const classes = useStyles();

    return (
        <Paper className={classes.root}>
            <Table className={classes.table}>
                <TableHead>
                    <TableRow>
                        <TableCell>Avatar</TableCell>
                        <TableCell>Name</TableCell>
                        <TableCell>Role</TableCell>
                        <TableCell>Access</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {allUsers.map(user => {
                        const isCurrent = user.id === authenticatedUser.id;
                        const hasAccess = usersThatHaveAccess.some(
                            account => account.id === user.id
                        );
                        return (
                            <UserTableRowContainer
                                key={"user-row-" + user.id}
                                user={user}
                                isCurrent={isCurrent}
                                hasAccess={hasAccess}
                            />
                        );
                    })}
                </TableBody>
            </Table>
        </Paper>
    );
};

export { Users };
