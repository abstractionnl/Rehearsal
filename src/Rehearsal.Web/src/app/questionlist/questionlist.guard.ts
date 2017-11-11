import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";

import {Observable} from "rxjs/Observable";
import "rxjs/add/operator/do";
import "rxjs/add/operator/timeoutWith";

import QuestionListModel = QuestionList.QuestionListModel;
import {Store} from "@ngrx/store";
import {AppState, selectSelectedQuestionList} from "./store/questionlist.state";
import {LoadQuestionList, NewQuestionList} from "./store/questionlist.actions";

@Injectable()
export class QuestionListGuard implements CanActivate {
    constructor(private store: Store<AppState>) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
        let id = route.params['id'];
        const listIsLoaded = (list: QuestionListModel) => list && (list.id == id || (id == 'new' && list.id === null));

        return this.store
            .select(selectSelectedQuestionList)
            .do(
                list => {
                    if (!listIsLoaded(list)) {
                        if (id == 'new') {
                            this.store.dispatch(new NewQuestionList());
                        } else {
                            this.store.dispatch(new LoadQuestionList({id: id}));
                        }
                    }

                }
            )
            .filter(listIsLoaded)
            .take(1)
            .map(_ => true)
            .timeoutWith(5000, Observable.of(false));
    }
}
