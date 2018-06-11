import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";

import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable()
export class AuthService  {
    private tokenUrl = 'api/jwt/token';

    constructor(private http: HttpClient, private jwtHelperService: JwtHelperService ) {

    }

    loggedIn() {
        const token: string = this.jwtHelperService.tokenGetter();

        if (!token) {
            return false
        }

        const tokenExpired: boolean = this.jwtHelperService.isTokenExpired(token);

        return !tokenExpired
    }

    login(): Promise<void> {
        return this.http.post(this.tokenUrl, { userName: 'default' })
            .toPromise()
            .then(response => localStorage.setItem('token', (<any>(response)).access_token));
    }
}
