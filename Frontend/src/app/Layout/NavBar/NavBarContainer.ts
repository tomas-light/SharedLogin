import { connect } from "react-redux";
import { AnyAction, Dispatch } from "redux";

import { Reducers } from "@reducers";
import { urls } from "@app/PageComponentRouter";
import { INavBarCallProps, INavBarProps, NavBar } from "./NavBar";
import { AuthController } from "@api/AuthController";
import { history } from "@app/App";

const mapStateToProps = (state: Reducers): INavBarProps => {
    return {
        activeAccount: state.layoutStore.activeAccount,
        authenticatedAccount: state.layoutStore.authenticatedAccount
    };
};

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>): INavBarCallProps => {
    return {
        logout: () => {
            dispatch(AuthController.postLogout());
            history.push(urls.loginPath);
        }
    };
};

export const NavBarContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(NavBar);
