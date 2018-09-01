import {APP_INITIALIZER, NgModule} from '@angular/core';
import {HttpClientModule} from "@angular/common/http";
import {JWT_OPTIONS, JwtModule, JwtModuleOptions} from '@auth0/angular-jwt';

import {AuthRoutingModule} from "./auth-routing.module";

import {AuthService} from "./services/auth.service";

import {AuthGuard} from "./guards/auth-guard.service";

import {LoginPage} from "./pages/login/login.page";
import {Store, StoreModule} from "@ngrx/store";
import {authmoduleStateReducer} from "./store/auth.reducer";
import {EffectsModule} from "@ngrx/effects";
import {AuthEffects} from "./store/auth.effects";
import {AuthModuleState, selectToken} from "./store/auth.state";
import {RestoreToken} from "./store/auth.actions";
import {IfLoggedInDirective} from "./directives/if-logged-in.directive";

export function loadTokenFromLocalStorage(store: Store<AuthModuleState>) {
    return () => {
        let token = localStorage.getItem('token');
        if (token) {
            store.dispatch(new RestoreToken(token));
        }
    }
}

export function jwtOptionsFactory(store: Store<AuthModuleState>) {
    let token: string = null;

    store.select(selectToken).subscribe(t => token = t);

    return {
        tokenGetter: () => token,
        whitelistedDomains: ['localhost:5000', 'localhost:4200']
    };
}

@NgModule({
    declarations: [
        LoginPage,
        IfLoggedInDirective
    ],
    imports: [
        HttpClientModule,
        AuthRoutingModule,
        JwtModule.forRoot({
            jwtOptionsProvider: { provide: JWT_OPTIONS, useFactory: jwtOptionsFactory, deps: [Store] }
        }),
        StoreModule.forFeature('auth', authmoduleStateReducer),
        EffectsModule.forFeature([AuthEffects])
    ],
    providers: [
        AuthService,
        AuthGuard,
        { provide: APP_INITIALIZER, useFactory: loadTokenFromLocalStorage, deps: [Store], multi: true }
    ],
    exports: [
        IfLoggedInDirective
    ]
})
export class AuthModule {}
