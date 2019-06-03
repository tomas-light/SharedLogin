import { AnyAction, Dispatch } from "redux";
import { connect } from "react-redux";

import { Reducers } from "@reducers";
import { IMainPageProps, IMainPageCallProps, MainPage } from "./MainPage";
import { UsersActions } from "@app/Main/MainPage/Users/redux/Users.actions";
import { HistoryActions } from "@app/Main/MainPage/Histories/redux/History.actions";

const mapStateToProps = (state: Reducers): IMainPageProps => {
    return {
        authenticatedUser: state.layoutStore.authenticatedAccount,
        accessibleAccounts: state.layoutStore.accessibleAccounts,
        allUsers: state.usersStore.allUsers
    };
};

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>): IMainPageCallProps => {
    return {
        loadUsers: () => dispatch(UsersActions.loadUsers()),
        loadAccessHistories: () => dispatch(HistoryActions.load())
    };
};

const MainPageContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(MainPage);

export default MainPageContainer;
