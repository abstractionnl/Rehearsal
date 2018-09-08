import {Action} from "@ngrx/store";

export enum NotificationActions {
    SUCCESS = '[Notification] Success',
    INFO = '[Notification] Info',
    WARNING = '[Notification] Warning',
    ERROR = '[Notification] Error'
}

export class SuccessNotification implements Action {
    readonly type = NotificationActions.SUCCESS;
    constructor(public message) {}
}

export class InfoNotification implements Action {
    readonly type = NotificationActions.INFO;
    constructor(public message) {}
}

export class WarningNotification implements Action {
    readonly type = NotificationActions.WARNING;
    constructor(public message) {}
}

export class ErrorNotification implements Action {
    readonly type = NotificationActions.ERROR;
    constructor(public message) {}
}
