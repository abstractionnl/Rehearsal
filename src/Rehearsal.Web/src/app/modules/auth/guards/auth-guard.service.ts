import {Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';

import {AuthModuleState, selectIsLoggedIn} from "../store/auth.state";
import {Store} from "@ngrx/store";
import {filter, tap} from "rxjs/internal/operators";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private store: Store<AuthModuleState>, private router: Router) {}

    canActivate() {
        return this.store.select(selectIsLoggedIn)
            .pipe(
                filter(loggedIn => loggedIn != undefined),
                tap(loggedIn => {
                    if (!loggedIn)
                        this.router.navigate(['/login']);
                })
            );

        /*
        if(this.auth.loggedIn()) {
            return true;
        } else {
            this.router.navigate(['/login']);
            return false;
        }*/
    }
}
