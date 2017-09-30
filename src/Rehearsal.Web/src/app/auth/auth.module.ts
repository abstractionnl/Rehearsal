import { NgModule } from '@angular/core';
import { Http, RequestOptions } from '@angular/http';
import { AuthHttp, AuthConfig } from 'angular2-jwt';
import { Auth } from "./auth.service";
import { AuthGuard } from "./auth-guard.service";
import { LoginComponent } from "./login.component";
import { AuthRoutingModule } from "./auth-routing.module";

export function authHttpServiceFactory(http: Http, options: RequestOptions) {
    return new AuthHttp(
        new AuthConfig(), http, options);
}

@NgModule({
    declarations: [
        LoginComponent
    ],
    imports: [
        AuthRoutingModule
    ],
    providers: [
        {
            provide: AuthHttp,
            useFactory: authHttpServiceFactory,
            deps: [ Http, RequestOptions ]
        },
        Auth,
        AuthGuard
    ]
})
export class AuthModule {}
