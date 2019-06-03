import { AppAction } from "@utils/types/AppAction";
import { NotifierStore } from "./Notifier.store";
import { NotifierActions } from "./Notifier.actions";

export const NotifierReducer = (
    state: NotifierStore = new NotifierStore(),
    action: AppAction
): NotifierStore => {
    switch (action.type) {
        case NotifierActions.REMOVE_SNACKBAR:
            const notifications = state.notifications.filter(
                notification => notification.key !== action.payload.key
            );

            return { notifications };

        case NotifierActions.ENQUEUE_SNACKBAR: {
            const notifications = [
                ...state.notifications,
                {
                    ...action.payload.notification
                }
            ];

            console.log(`snack variant is '${action.payload.notification.options.variant} (received in store)`);

            return { notifications };
        }

        default:
            return state;
    }
};
