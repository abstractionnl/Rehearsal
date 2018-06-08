import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";

import {map, filter} from "rxjs/operators";

export abstract class AbstractActivateGuard<TState> implements CanActivate {
    canActivate(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        return this.selectState()
            .pipe(
                map( (state, i) => {
                    if (this.hasLoaded(route, routerState, state))
                        return true;

                    if (i === 0) {
                        this.triggerLoad(route, routerState, state);
                        return null;
                    } else {
                        return false;
                    }
                }),
                filter(x => x !== null)
            );
    }

    abstract selectState(): Observable<TState>;
    abstract triggerLoad(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot, state: TState): void;
    abstract hasLoaded(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot, state: TState): boolean;
}
