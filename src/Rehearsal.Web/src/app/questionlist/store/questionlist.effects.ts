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

import {of} from "rxjs/observable/of";
import {empty} from "rxjs/observable/empty";
import {Router} from "@angular/router";

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
                .catch(err => of(new LoadQuestionListOverviewFailed(err)));
        });

    @Effect() loadQuestionList$: Observable<Action> = this.actions$
        .ofType<LoadQuestionList>(QuestionListActions.LOAD_LIST)
        .switchMap(action => {
            return this.questionListService.get(action.payload.id)
                .map(list => new LoadQuestionListSuccess(list))
                .catch(err => of(new LoadQuestionListFailed(err)));
        });

    @Effect() saveQuestionList$: Observable<Action> = this.actions$
        .ofType<SaveQuestionList>(QuestionListActions.SAVE_LIST)
        .switchMap(action => {
            let save = action.payload.id !== null
                ? this.questionListService.update(action.payload).map(_ => action.payload.id)
                : this.questionListService.create(action.payload);

            return save
                .map(id => new SaveQuestionListSuccess({...action.payload, id: id }))
                .catch(err => of(new SaveQuestionListFailed(err)));
        });

    @Effect() removeQuestionList$:  Observable<Action> = this.actions$
        .ofType<RemoveQuestionList>(QuestionListActions.REMOVE_LIST)
        .switchMap(action => {
            return this.questionListService.remove(action.payload.id)
                .map(_ => new RemoveQuestionListSuccess({...action.payload}))
                .catch(err => of(new RemoveQuestionListFailed(err)));
        });

    @Effect() reloadQuestionListOverview$: Observable<Action> = this.actions$
        .ofType(QuestionListActions.SAVE_LIST_SUCCESS, QuestionListActions.REMOVE_LIST_SUCCESS)
        .map(_ => new LoadQuestionListOverview());

    @Effect() navigateToListAfterSave$: Observable<Action> = this.actions$
        .ofType<SaveQuestionListSuccess>(QuestionListActions.SAVE_LIST_SUCCESS)
        .do(action => {
            this.router.navigate(['/questionlists', action.payload.id]);
        })
        .switchMap(_ => empty());

    @Effect() navigateToOverviewAfterRemove$: Observable<Action> = this.actions$
        .ofType<SaveQuestionListSuccess>(QuestionListActions.REMOVE_LIST_SUCCESS)
        .do(action => {
            this.router.navigate(['/questionlists']);
        })
        .switchMap(_ => empty());
}
