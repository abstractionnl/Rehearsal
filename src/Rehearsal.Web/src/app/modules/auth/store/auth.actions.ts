import {Action} from "@ngrx/store";

export enum AuthActions {
    LOGIN = '[Auth] Login',
    LOGIN_SUCCESS = '[Auth] Login Success',
    RESTORE_TOKEN = '[Auth] Restore token',
    LOGIN_FAILED = '[Auth] Login Failed',
    LOGOUT = '[Auth] Logout'
};

export class Login implements Action {
    readonly type = AuthActions.LOGIN;
    constructor(public userName: string) { }
}

export class LoginSuccess implements Action {
    readonly type = AuthActions.LOGIN_SUCCESS;
    constructor(public token: string) { }
}

export class LoginFailed implements Action {
    readonly type = AuthActions.LOGIN_FAILED;
    constructor(public reason: string) { }
}

export class Logout implements Action {
    readonly type = AuthActions.LOGOUT;
    constructor() { }
}

export class RestoreToken implements Action {
    readonly type = AuthActions.RESTORE_TOKEN;
    constructor(public token: string) { }
}

export type All = Login | LoginSuccess | RestoreToken | LoginFailed | Logout;
