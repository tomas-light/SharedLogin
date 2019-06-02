import * as React from "react";

import TableRow from "@material-ui/core/TableRow";
import TableCell from "@material-ui/core/TableCell";
import IconButton from "@material-ui/core/IconButton";
import Tooltip from "@material-ui/core/Tooltip";
import AddIcon from "@material-ui/icons/Add";
import RemoveIcon from "@material-ui/icons/Remove";

import { AccountDTO } from "@models/accounts/AccountDTO";
import { GreenCheckIcon } from "../../../../../shared/icons/GreenCheckIcon";
import { Avatar } from "@material-ui/core";

export interface IUserTableRowOwnProps {
    user: AccountDTO;
    isCurrent: boolean;
    isAvailable: boolean;
}

export interface IUserTableRowCallProps {
    addAccess: (accountId: string) => void;
    removeAccess: (accountId: string) => void;
}

type Props = IUserTableRowOwnProps & IUserTableRowCallProps;

const UserTableRow: React.FunctionComponent<Props> = props => {
    const { user, isCurrent, isAvailable, addAccess, removeAccess } = props;

    return (
        <TableRow>
            <TableCell component="th" scope="row">
                <Avatar src={user.avatar} />
            </TableCell>
            <TableCell>{user.name}</TableCell>
            <TableCell>{user.roleName}</TableCell>
            <TableCell>
                {isCurrent ? (
                    <Tooltip title={"It's you"}>
                        <IconButton>
                            <GreenCheckIcon />
                        </IconButton>
                    </Tooltip>
                ) : isAvailable ? (
                    <IconButton onClick={() => removeAccess(user.id)}>
                        <RemoveIcon />
                    </IconButton>
                ) : (
                    <IconButton onClick={() => addAccess(user.id)}>
                        <AddIcon />
                    </IconButton>
                )}
            </TableCell>
        </TableRow>
    );
};

export { UserTableRow };
