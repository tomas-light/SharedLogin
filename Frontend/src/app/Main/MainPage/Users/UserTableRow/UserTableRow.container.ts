import { connect } from "react-redux";
import { AnyAction, Dispatch } from "redux";
import { ComponentType } from "react";

import {
    IUserTableRowOwnProps,
    IUserTableRowCallProps,
    UserTableRow
} from "./UserTableRow";
import { UsersActions } from "@app/Main/MainPage/redux/Users.actions";

const mapDispatchToProps = (
    dispatch: Dispatch<AnyAction>
): IUserTableRowCallProps => {
    return {
        addAccess: (accountId: string) =>
            dispatch(UsersActions.addAccess(accountId)),
        removeAccess: (accountId: string) =>
            dispatch(UsersActions.removeAccess(accountId))
    };
};

const UserTableRowContainer: ComponentType<IUserTableRowOwnProps> = connect(
    null,
    mapDispatchToProps
)(UserTableRow);

export default UserTableRowContainer;
