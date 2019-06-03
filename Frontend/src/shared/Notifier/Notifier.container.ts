import { connect } from "react-redux";

import { Reducers } from "@reducers";
import { INotifierCallProps, INotifierProps } from "./Notifier.interfaces";
import { NotifierActions } from "./redux/Notifier.actions";
import { Notifier } from "./Notifier";
import { Dispatch } from "redux";
import { AppAction } from "@utils/types/AppAction";

const mapStateToProps = (state: Reducers): INotifierProps => {
    return {
        notifications: state.notifierStore.notifications
    };
};

const mapDispatchToProps = (dispatch: Dispatch<AppAction>): Partial<INotifierCallProps> => {
    return {
        dispatch,
        removeSnackbar: (key: string) =>
            dispatch(NotifierActions.removeSnackbar(key))
    };
};

const NotifierContainer = connect(
    mapStateToProps,
    mapDispatchToProps
)(Notifier);

export default NotifierContainer;
