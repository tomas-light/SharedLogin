import * as React from "react";
import { OptionsObject, WithSnackbarProps } from "notistack";
import { AppAction } from "@utils/types/AppAction";
import { Dispatch } from "redux";

export interface INotification {
    message: string | React.ReactNode;
    options?: OptionsObject;
    key?: string;
}

export interface INotifierProps {
    notifications: INotification[];
}

export interface INotifierCallProps extends WithSnackbarProps {
    dispatch: Dispatch<AppAction>;
    removeSnackbar: (key: string) => void;
}
