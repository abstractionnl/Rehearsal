import {AuthModuleState, initialState} from "./auth.state";
import {AuthActions, All, LoginSuccess} from "./auth.actions";
import {JwtHelperService} from "@auth0/angular-jwt";

const helper = new JwtHelperService();

export function authmoduleStateReducer(state: AuthModuleState = initialState, action: All): AuthModuleState {
    switch (action.type) {
        case AuthActions.LOGIN_SUCCESS:
        case AuthActions.RESTORE_TOKEN:
            if (helper.isTokenExpired(action.token))
                return state;

            return {
                ...state,
                token: action.token,
                claims: helper.decodeToken(action.token)
            };
        case AuthActions.LOGOUT:
            return {
                ...state,
                token: null,
                claims: null
            }
    }

    return state;
}
