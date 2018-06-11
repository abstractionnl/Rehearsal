import {NgModule} from '@angular/core';
import {HttpClientModule} from "@angular/common/http";
import {JwtModule} from '@auth0/angular-jwt';

import {AuthRoutingModule} from "./auth-routing.module";

import {AuthService} from "./services/auth.service";

import {AuthGuard} from "./guards/auth-guard.service";

import {LoginPage} from "./pages/login/login.page";

export function getToken() {
    return localStorage.getItem('token');
}

@NgModule({
    declarations: [
        LoginPage
    ],
    imports: [
        HttpClientModule,
        AuthRoutingModule,
        JwtModule.forRoot({
            config: {
                tokenGetter: getToken,
                whitelistedDomains: ['localhost:5000', 'localhost:4200']
            }
        })
    ],
    providers: [
        AuthService,
        AuthGuard
    ]
})
export class AuthModule {}
