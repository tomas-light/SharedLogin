import { connect } from "react-redux";
import { AnyAction, Dispatch } from "redux";

import { Reducers } from "@reducers";
import { ILoginPageCallProps, ILoginPageProps, LoginPage } from "./LoginPage";

const mapStateToProps = (state: Reducers): ILoginPageProps => {
    return {};
};

const mapDispatchToProps = (
    dispatch: Dispatch<AnyAction>
): ILoginPageCallProps => {
    return {};
};

const LoginPageContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(LoginPage);

export default LoginPageContainer;