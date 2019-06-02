import { connect } from "react-redux";
import { AnyAction, Dispatch } from "redux";

import { Reducers } from "@reducers";
import { urls } from "@app/PageComponentRouter";
import { INavBarMenuProps, INavBarMenuCallProps, INavBarMenuOwnProps, NavBarMenu } from "./NavBarMenu";
import { AuthController } from "@api/AuthController";
import { history } from "@app/App";
import {ComponentType} from "react";

const mapStateToProps = (state: Reducers): INavBarMenuProps => {
    return {
        activeAccount: state.layoutStore.activeAccount,
        accessibleAccounts: state.layoutStore.accessibleAccounts
    };
};

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>): INavBarMenuCallProps => {
    return {
        onAccountClick: () => {
            // dispatch(AuthController.postLogout());
            // history.push(urls.loginPath);
        }
    };
};

export const NavBarMenuContainer: ComponentType<INavBarMenuOwnProps> = connect(
    mapStateToProps,
    mapDispatchToProps
)(NavBarMenu);
