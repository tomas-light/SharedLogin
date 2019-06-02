import { AnyAction, Dispatch } from "redux";
import { connect } from "react-redux";

import { Reducers } from "@reducers";
import { IMainPageProps, IMainPageCallProps, MainPage } from "./MainPage";
import { UsersActions } from "@app/Main/MainPage/redux/Users.actions";

const mapStateToProps = (state: Reducers): IMainPageProps => {
    return {
        authenticatedUser: state.layoutStore.authenticatedAccount,
        accessibleAccounts: state.layoutStore.accessibleAccounts,
        allUsers: state.usersStore.allUsers
    };
};

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>): IMainPageCallProps => {
    return {
        loadUsers: () => dispatch(UsersActions.loadUsers())
    };
};

const MainPageContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(MainPage);

export default MainPageContainer;
