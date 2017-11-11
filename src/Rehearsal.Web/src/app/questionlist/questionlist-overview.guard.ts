import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";
import {Store} from "@ngrx/store";

import {Observable} from "rxjs/Observable";
import "rxjs/add/operator/take";

import {AppState, selectQuestionListOverview} from "./store/questionlist.state";
import {LoadQuestionListOverview} from "./store/questionlist.actions";
import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;

@Injectable()
export class QuestionlistOverviewGuard implements CanActivate  {
    constructor(private store: Store<AppState>) {
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        const hasLists = (lists: QuestionListOverviewModel[]) => lists && lists.length > 0;

        return this.store
            .select(selectQuestionListOverview)
            .do(
                lists => {
                    if (!hasLists(lists))
                        this.store.dispatch(new LoadQuestionListOverview());
                }
            )
            .filter(hasLists)
            .take(1)
            .map(_ => true)
            .timeoutWith(5000, Observable.of(false));
    }
}
