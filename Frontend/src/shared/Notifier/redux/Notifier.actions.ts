import uuidv4 from "uuid/v4";
import { INotification } from "../Notifier.interfaces";
import { createAction } from "@utils/actions/createAction";

export class NotifierActions {
    public static readonly ENQUEUE_SNACKBAR = "ENQUEUE_SNACKBAR";
    public static readonly REMOVE_SNACKBAR = "REMOVE_SNACKBAR";

    public static enqueueSnackbar = (notification: INotification) =>
        createAction(NotifierActions.ENQUEUE_SNACKBAR, {
            notification: {
                message: notification.message,
                options: notification.options,
                key: uuidv4()
            }
        });

    public static removeSnackbar = (key: string) =>
        createAction(NotifierActions.REMOVE_SNACKBAR, { key });
}
