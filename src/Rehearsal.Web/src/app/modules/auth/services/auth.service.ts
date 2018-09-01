import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";

import {JwtHelperService} from '@auth0/angular-jwt';
import {Observable} from "rxjs/index";
import {map} from "rxjs/internal/operators";

@Injectable()
export class AuthService  {
    private tokenUrl = 'api/jwt/token';

    constructor(private http: HttpClient, private jwtHelperService: JwtHelperService ) {

    }

    login(userName: string): Observable<string> {
        return this.http.post(this.tokenUrl, { userName: userName })
            .pipe(map(response => (<any>(response)).access_token));
    }
}
