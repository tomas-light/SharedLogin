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
        grantAccess: (accountId: string) =>
            dispatch(UsersActions.grantAccess(accountId)),
        restrictAccess: (accountId: string) =>
            dispatch(UsersActions.restrictAccess(accountId))
    };
};

const UserTableRowContainer: ComponentType<IUserTableRowOwnProps> = connect(
    null,
    mapDispatchToProps
)(UserTableRow);

export default UserTableRowContainer;
