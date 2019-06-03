import { INotification } from "../Notifier.interfaces";

export class NotifierStore {
    public notifications: INotification[];

    constructor() {
        this.notifications = [];
    }
}
