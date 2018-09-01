import {Injectable} from "@angular/core";
import {Action, Store} from "@ngrx/store";
import {Router} from "@angular/router";
import {Actions, Effect, ofType} from "@ngrx/effects";
import {AuthService} from "../services/auth.service";
import {AuthModuleState} from "./auth.state";
import {Observable, of} from "rxjs/index";
import {AuthActions, Login, LoginFailed, LoginSuccess} from "./auth.actions";
import {catchError, map, switchMap, tap} from "rxjs/operators";

@Injectable()
export class AuthEffects {
    constructor(
        private authService: AuthService,
        private router: Router,
        private actions$: Actions,
        private store: Store<AuthModuleState>
    ) {}

    @Effect() loadQuestionListOverview$: Observable<Action> = this.actions$.pipe(
            ofType<Login>(AuthActions.LOGIN),
            switchMap(action => {
                return this.authService.login(action.userName).pipe(
                    map(token => new LoginSuccess(token)),
                    catchError(err => of<Action>(new LoginFailed(err)))
                );
            })
        );

    @Effect({ dispatch: false }) addTokenToLocalStorage$: Observable<Action> = this.actions$.pipe(
        ofType<LoginSuccess>(AuthActions.LOGIN_SUCCESS),
        tap((action: LoginSuccess) => {
            localStorage.setItem('token', action.token);
        }
    ));

    @Effect({ dispatch: false }) navigateToDashboardAfterLogin$: Observable<Action> = this.actions$.pipe(
            ofType<LoginSuccess>(AuthActions.LOGIN_SUCCESS),
            tap(action => {
                this.router.navigate(['/dashboard'])
            }
        ));
}
