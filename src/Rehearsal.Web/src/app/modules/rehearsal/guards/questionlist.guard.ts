import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, RouterStateSnapshot} from "@angular/router";

import {Observable} from "rxjs";

import {Store} from "@ngrx/store";

import {AbstractActivateGuard} from "../../../shared/guards/abstract-activate-guard";

import {RehearsalSessionState, RehearsalState, selectCurrentSession} from "../store/rehearsal.state";
import {LoadRehearsal} from "../store/rehearsal.actions";

@Injectable()
export class RehearsalSessionGuard extends AbstractActivateGuard<RehearsalSessionState> {
    constructor(private store: Store<RehearsalState>) {
        super();
    }

    selectState(): Observable<RehearsalSessionState> {
        return this.store.select(selectCurrentSession);
    }

    triggerLoad(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot, session: RehearsalSessionState): void {
        let id = route.params['id'];
        this.store.dispatch(new LoadRehearsal({ id: id }));
    }

    hasLoaded(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot, session: RehearsalSessionState): boolean {
        let id = route.params['id'];
        return session && session.id == id;
    }
}
