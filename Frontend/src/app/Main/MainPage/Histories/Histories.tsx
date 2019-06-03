import * as React from "react";

import { createStyles, makeStyles, TableBody } from "@material-ui/core";
import Table from "@material-ui/core/Table";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TableCell from "@material-ui/core/TableCell";
import Paper from "@material-ui/core/Paper";
import Typography from "@material-ui/core/Typography/Typography";

import { History } from "./History/History";
import { HistoryItem } from "./HistoryItem";

const useStyles = makeStyles(theme =>
    createStyles({
        root: {
            margin: 24,
            overflowY: "auto"
        },
        title: {
            padding: "0 24px"
        }
    })
);

export interface IHistoriesProps {
    histories: HistoryItem[];
}

type Props = IHistoriesProps;

const Histories: React.FunctionComponent<Props> = props => {
    const { histories } = props;
    const classes = useStyles();

    if (histories.length === 0) {
        return <></>;
    }

    return (
        <>
            <Typography variant={"h5"} className={classes.title}>
                Access history to your account
            </Typography>
            <Paper className={classes.root}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>Avatar</TableCell>
                            <TableCell>User</TableCell>
                            <TableCell>Login time</TableCell>
                            <TableCell>Logout time</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {histories.map(history => (
                            <History
                                history={history}
                                key={"history-item-" + history.loginDateTime}
                            />
                        ))}
                    </TableBody>
                </Table>
            </Paper>
        </>
    );
};

export { Histories };
