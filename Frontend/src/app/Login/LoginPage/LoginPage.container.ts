import { connect } from "react-redux";
import { AnyAction, Dispatch } from "redux";

import { Reducers } from "@reducers";
import { LoginPageActions } from "@app/Login/LoginPage/redux/LoginPage.actions";
import {
    ILoginPageCallProps,
    ILoginPageProps,
    inputIds,
    LoginPage
} from "./LoginPage";
import { LoginDTO } from "@models/auth/LoginDTO";

const mapStateToProps = (state: Reducers): ILoginPageProps => {
    return {};
};

const mapDispatchToProps = (
    dispatch: Dispatch<AnyAction>
): ILoginPageCallProps => {
    return {
        onSubmit: () => {
            const emailInput: HTMLInputElement | any = document.getElementById(
                inputIds.email
            );
            const passwordInput:
                | HTMLInputElement
                | any = document.getElementById(inputIds.password);

            const dto: LoginDTO = {
                email: emailInput.value,
                password: passwordInput.value
            };

            dispatch(LoginPageActions.login(dto));
        }
    };
};

const LoginPageContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(LoginPage);

export default LoginPageContainer;
