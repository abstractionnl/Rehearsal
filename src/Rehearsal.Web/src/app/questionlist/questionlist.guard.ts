import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, RouterStateSnapshot} from "@angular/router";

import {Observable} from "rxjs/Observable";

import QuestionListModel = QuestionList.QuestionListModel;

import {Store} from "@ngrx/store";

import {AppState, selectSelectedQuestionList} from "./store/questionlist.state";
import {LoadQuestionList, NewQuestionList} from "./store/questionlist.actions";
import {AbstractActivateGuard} from "../abstract-activate-guard";

@Injectable()
export class QuestionListGuard extends AbstractActivateGuard<QuestionListModel> {
    constructor(private store: Store<AppState>) {
        super();
    }

    selectState(): Observable<QuestionList.QuestionListModel> {
        return this.store.select(selectSelectedQuestionList);
    }

    triggerLoad(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot, state: QuestionList.QuestionListModel): void {
        let id = route.params['id'];
        if (id == 'new') {
            this.store.dispatch(new NewQuestionList());
        } else {
            this.store.dispatch(new LoadQuestionList({id: id}));
        }
    }

    hasLoaded(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot, list: QuestionList.QuestionListModel): boolean {
        let id = route.params['id'];
        return list && (list.id == id || (id == 'new' && list.id === null));
    }
}
