import {Injectable} from "@angular/core";
import {Observable} from "rxjs/Observable";
import {Action} from "@ngrx/store";
import {Actions, Effect} from "@ngrx/effects";

import {QuestionListService} from "../questionlist.service";
import * as QuestionListActions from "./questionlist.actions";
import {
    LoadQuestionListOverview,  LoadQuestionListOverviewSuccess, LoadQuestionListOverviewFailed,
    LoadQuestionList, LoadQuestionListFailed, LoadQuestionListSuccess,
    RemoveQuestionList, RemoveQuestionListFailed, RemoveQuestionListSuccess,
    SaveQuestionList, SaveQuestionListFailed, SaveQuestionListSuccess
} from "./questionlist.actions";

import {Router} from "@angular/router";
import {stripEmptyQuestions, validateQuestionList} from "./questionlist.state";

import 'rxjs/add/operator/do';

@Injectable()
export class QuestionlistEffects {
    constructor(
        private questionListService: QuestionListService,
        private router: Router,
        private actions$: Actions
    ) {}

    @Effect() loadQuestionListOverview$: Observable<Action> = this.actions$
        .ofType<LoadQuestionListOverview>(QuestionListActions.LOAD_OVERVIEW)
        .throttleTime(1000)
        .switchMap(action => {
            return this.questionListService.getAll()
                .map(lists => new LoadQuestionListOverviewSuccess(lists))
                .catch(err => Observable.of(new LoadQuestionListOverviewFailed(err)));
        });

    @Effect() loadQuestionList$: Observable<Action> = this.actions$
        .ofType<LoadQuestionList>(QuestionListActions.LOAD_LIST)
        .switchMap(action => {
            return this.questionListService.get(action.payload.id)
                .map(list => new LoadQuestionListSuccess(list))
                .catch(err => Observable.of(new LoadQuestionListFailed(err)));
        });

    @Effect() saveQuestionList$: Observable<Action> = this.actions$
        .ofType<SaveQuestionList>(QuestionListActions.SAVE_LIST)
        .switchMap(action => {
            let list = stripEmptyQuestions(action.payload);
            let valid = validateQuestionList(list);

            if (!valid) {
                return Observable.of(new SaveQuestionListFailed("Not valid"));
            }

            let save = list.id !== null
                ? this.questionListService.update(list).map(_ => list.id)
                : this.questionListService.create(list);

            return save
                .map(id => new SaveQuestionListSuccess({...list, id: id }))
                .catch(err => Observable.of(new SaveQuestionListFailed(err)));
        });

    @Effect() removeQuestionList$:  Observable<Action> = this.actions$
        .ofType<RemoveQuestionList>(QuestionListActions.REMOVE_LIST)
        .switchMap(action => {
            return this.questionListService.remove(action.payload.id)
                .map(_ => new RemoveQuestionListSuccess({...action.payload}))
                .catch(err => Observable.of(new RemoveQuestionListFailed(err)));
        });

    @Effect() reloadQuestionListOverview$: Observable<Action> = this.actions$
        .ofType(QuestionListActions.SAVE_LIST_SUCCESS, QuestionListActions.REMOVE_LIST_SUCCESS)
        .map(_ => new LoadQuestionListOverview());

    @Effect({ dispatch: false }) navigateToListAfterSave$: Observable<Action> = this.actions$
        .ofType<SaveQuestionListSuccess>(QuestionListActions.SAVE_LIST_SUCCESS)
        .do(action => {
            this.router.navigate(['/questionlists', action.payload.id]);
        });

    @Effect({ dispatch: false }) navigateToOverviewAfterRemove$: Observable<Action> = this.actions$
        .ofType<SaveQuestionListSuccess>(QuestionListActions.REMOVE_LIST_SUCCESS)
        .do(action => {
            this.router.navigate(['/questionlists']);
        });
}
