import { connect } from "react-redux";
import { AnyAction, Dispatch } from "redux";

import { Reducers } from "@reducers";
import { INavBarMenuProps, INavBarMenuCallProps, INavBarMenuOwnProps, NavBarMenu } from "./NavBarMenu";
import {ComponentType} from "react";
import {LayoutActions} from "@app/Layout/redux/Layout.actions";

const mapStateToProps = (state: Reducers): INavBarMenuProps => {
    return {
        activeAccount: state.layoutStore.activeAccount,
        accessibleAccounts: state.layoutStore.accessibleAccounts
    };
};

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>): INavBarMenuCallProps => {
    return {
        onAccountClick: (accountId: string) =>
            dispatch(LayoutActions.activateAccount(accountId))
    };
};

export const NavBarMenuContainer: ComponentType<INavBarMenuOwnProps> = connect(
    mapStateToProps,
    mapDispatchToProps
)(NavBarMenu);
