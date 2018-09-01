import {createFeatureSelector, createSelector} from "@ngrx/store";

export interface AuthModuleState {
    token: string;
    claims: any;
}

export const initialState: AuthModuleState = {
    token: null,
    claims: null
};

export const selectFeature = createFeatureSelector<AuthModuleState>('auth');

export const selectToken = createSelector(
    selectFeature,
    (state: AuthModuleState) => {
        return state.token;
    }
);

export const selectIsLoggedIn = createSelector(
    selectToken,
    (token: string) => {
        return token != null;
    }
);
