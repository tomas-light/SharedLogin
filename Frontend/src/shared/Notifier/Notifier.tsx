import React, { Component } from "react";
import { withSnackbar } from "notistack";

import { INotifierCallProps, INotifierProps } from "./Notifier.interfaces";

class Notifier extends Component<INotifierProps & INotifierCallProps> {
    private displayed = [];

    private storeDisplayed = (id: string) => {
        this.displayed = [...this.displayed, id];
    };

    public shouldComponentUpdate(
        nextProps: INotifierProps & INotifierCallProps
    ): boolean {
        const oldNotifications = this.props.notifications;
        let notExists = false;
        for (const newSnack of nextProps.notifications) {
            if (notExists) {
                continue;
            }
            notExists =
                notExists ||
                !oldNotifications.find(
                    snack => newSnack.key === snack.key
                );
        }
        return notExists;
    }

    public componentDidUpdate() {
        const { notifications = [], enqueueSnackbar, removeSnackbar } = this.props;

        notifications.forEach(notification => {
            // Do nothing if snackbar is already displayed
            if (this.displayed.find(x => x.key === notification.key)) {
                return;
            }

            console.log(`snack variant is '${notification.options.variant}`);

            enqueueSnackbar(notification.message, notification.options);

            // Keep track of snackbars that we've displayed
            this.storeDisplayed(notification.key);

            // Dispatch action to remove snackbar from redux store
            removeSnackbar(notification.key);
        });
    }

    public render() {
        return null;
    }
}

const componentWithSnackbar = withSnackbar(Notifier);
export { componentWithSnackbar as Notifier }
