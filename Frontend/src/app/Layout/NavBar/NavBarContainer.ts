import { connect } from "react-redux";
import { AnyAction, Dispatch } from "redux";
import { push } from "connected-react-router";

import { Reducers } from "@reducers";
import { urls } from "@app/PageComponentRouter";
import { INavBarCallProps, INavBarProps, NavBar } from "./NavBar";

const mapStateToProps = (
    state: Reducers,
    ownProps: INavBarProps
): INavBarProps => {
    return {};
};

const mapDispatchToProps = (
    dispatch: Dispatch<AnyAction>,
    ownProps: INavBarCallProps
): INavBarCallProps => {
    return {
        redirectToLogin: () => dispatch(push(urls.loginPath)),

        redirectToBugList: () => {},
        redirectToUserList: () => {},

        redirectToNewBug: () => {},
        redirectToNewUser: () => {},

        logout: () => {}
    };
};

export const NavBarContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(NavBar);
