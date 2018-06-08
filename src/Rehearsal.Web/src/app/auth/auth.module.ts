import {NgModule} from '@angular/core';
import {HttpClientModule} from "@angular/common/http";
import {JwtModule} from '@auth0/angular-jwt';
import {Auth} from "./auth.service";
import {AuthGuard} from "./auth-guard.service";
import {LoginComponent} from "./login.component";
import {AuthRoutingModule} from "./auth-routing.module";

export function getToken() {
    return localStorage.getItem('token');
}

@NgModule({
    declarations: [
        LoginComponent
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
        Auth,
        AuthGuard
    ]
})
export class AuthModule {}
