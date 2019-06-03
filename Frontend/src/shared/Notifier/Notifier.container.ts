import { connect } from "react-redux";

import { Reducers } from "@reducers";
import { INotifierCallProps, INotifierProps } from "./Notifier.interfaces";
import { NotifierActions } from "./redux/Notifier.actions";
import { Notifier } from "./Notifier";

const mapStateToProps = (state: Reducers): INotifierProps => {
    return {
        notifications: state.notifierStore.notifications
    };
};

const mapDispatchToProps = (dispatch): Partial<INotifierCallProps> => {
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
