import { connect } from "react-redux";
import { AnyAction, Dispatch } from "redux";

import { Reducers } from "@reducers";
import {
    IActivatedAccountProps,
    IActivatedAccountCallProps,
    ActivatedAccount
} from "./ActivatedAccount";

const mapStateToProps = (state: Reducers): IActivatedAccountProps => {
    return {
        activatedAccount: state.layoutStore.activeAccount
    };
};

const mapDispatchToProps = (
    dispatch: Dispatch<AnyAction>
): IActivatedAccountCallProps => {
    return {};
};

const ActivatedAccountContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(ActivatedAccount);

export default ActivatedAccountContainer;
