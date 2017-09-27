import {Injectable} from "@angular/core";
import { Http } from "@angular/http";

import { tokenNotExpired } from 'angular2-jwt';

@Injectable()
export class Auth  {
    private tokenUrl = 'api/jwt/token';

    constructor(private http: Http) {

    }

    loggedIn() {
        try {
            return tokenNotExpired();
        } catch (e) {
            console.log(e, localStorage.getItem('token'));
            return false;
        }
    }

    login(): Promise<void> {
        return this.http.post(this.tokenUrl, {})
            .toPromise()
            .then(response => localStorage.setItem('token', response.json().access_token));
    }
}
