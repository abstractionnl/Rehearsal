import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, RouterStateSnapshot} from "@angular/router";
import {Store} from "@ngrx/store";

import {Observable} from "rxjs";

import {QuestionlistState, selectQuestionListOverview} from "../store/questionlist.state";
import {LoadQuestionListOverview} from "../store/questionlist.actions";

import {QuestionList} from "../../../types";
import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;

import {AbstractActivateGuard} from "../../../shared/guards/abstract-activate-guard";

@Injectable()
export class QuestionlistOverviewGuard extends AbstractActivateGuard<QuestionListOverviewModel[]> {
    constructor(protected store: Store<QuestionlistState>) {
        super();
    }

    selectState(): Observable<QuestionList.QuestionListOverviewModel[]> {
        return this.store.select(selectQuestionListOverview);
    }

    triggerLoad(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot, state: QuestionList.QuestionListOverviewModel[]): void {
        this.store.dispatch(new LoadQuestionListOverview());
    }

    hasLoaded(route: ActivatedRouteSnapshot, routerState: RouterStateSnapshot, lists: QuestionList.QuestionListOverviewModel[]): boolean {
        return lists && lists.length > 0;
    }
}
