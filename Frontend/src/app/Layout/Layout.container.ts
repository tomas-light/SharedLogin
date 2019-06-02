import { connect } from "react-redux";
import { AnyAction, Dispatch } from "redux";

import { ILayoutOwnProps, ILayoutCallProps, Layout } from "./Layout";
import { ComponentType } from "react";
import { LayoutActions } from "@app/Layout/redux/Layout.actions";

// const mapStateToProps = (state: Reducers) => {
//     return {
//         activeAccount: state.layoutStore.activeAccount,
//         authenticatedAccount: state.layoutStore.authenticatedAccount
//     };
// };

const mapDispatchToProps = (dispatch: Dispatch<AnyAction>): ILayoutCallProps => {
    return {
        load: () => dispatch(LayoutActions.load())
    };
};

export const LayoutContainer: ComponentType<ILayoutOwnProps> = connect(
    null,
    mapDispatchToProps
)(Layout);
