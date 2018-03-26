import {Injectable} from "@angular/core";
import {Observable} from "rxjs/Observable";
import {ArrayObservable} from "rxjs/observable/ArrayObservable";
import {catchError, map, switchMap, tap, throttleTime, withLatestFrom} from "rxjs/operators";
import {Action, Store} from "@ngrx/store";
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
import {AppState, selectSelectedQuestionList, stripEmptyQuestions, validateQuestionList} from "./questionlist.state";
import QuestionListModel = QuestionList.QuestionListModel;

@Injectable()
export class QuestionlistEffects {
    constructor(
        private questionListService: QuestionListService,
        private router: Router,
        private actions$: Actions,
        private store: Store<AppState>
    ) {}

    @Effect() loadQuestionListOverview$: Observable<Action> = this.actions$
        .ofType<LoadQuestionListOverview>(QuestionListActions.LOAD_OVERVIEW)
        .pipe(
            throttleTime(1000),
            switchMap(action => {
                return this.questionListService.getAll().pipe(
                    map(lists => new LoadQuestionListOverviewSuccess(lists)),
                    catchError(err => ArrayObservable.of<Action>(new LoadQuestionListOverviewFailed(err)))
                );
            })
        );

    @Effect() loadQuestionList$: Observable<Action> = this.actions$
        .ofType<LoadQuestionList>(QuestionListActions.LOAD_LIST)
        .pipe(
            switchMap(action => {
                return this.questionListService.get(action.payload.id).pipe(
                    map(list => new LoadQuestionListSuccess(list)),
                    catchError(err => ArrayObservable.of(new LoadQuestionListFailed(err)))
                )
            })
        );

    @Effect() saveQuestionList$: Observable<Action> = this.actions$
        .ofType<SaveQuestionList>(QuestionListActions.SAVE_LIST)
        .pipe(
            withLatestFrom(this.store.select(selectSelectedQuestionList), (a, l) => l),
            switchMap((list: QuestionListModel) => {
                list = stripEmptyQuestions(list);
                let valid = validateQuestionList(list);

                if (!valid) {
                    return ArrayObservable.of(new SaveQuestionListFailed("Not valid"));
                }

                let save = list.id !== null
                    ? this.questionListService.update(list).pipe(map(_ => list.id))
                    : this.questionListService.create(list);

                return save.pipe(
                    map(id => new SaveQuestionListSuccess({...list, id: id })),
                    catchError(err => ArrayObservable.of(new SaveQuestionListFailed(err)))
                );
            })
        );

    @Effect() removeQuestionList$:  Observable<Action> = this.actions$
        .ofType<RemoveQuestionList>(QuestionListActions.REMOVE_LIST)
        .pipe(
            withLatestFrom(this.store.select(selectSelectedQuestionList), (a, l) => l),
            switchMap((list: QuestionListModel) => {
                return this.questionListService.remove(list.id).pipe(
                    map(_ => new RemoveQuestionListSuccess({...list})),
                    catchError(err => ArrayObservable.of(new RemoveQuestionListFailed(err)))
                );
            })
        );

    @Effect() reloadQuestionListOverview$: Observable<Action> = this.actions$
        .ofType(QuestionListActions.SAVE_LIST_SUCCESS, QuestionListActions.REMOVE_LIST_SUCCESS)
        .pipe(
            map(_ => new LoadQuestionListOverview())
        );

    @Effect({ dispatch: false }) navigateToListAfterSave$: Observable<Action> = this.actions$
        .ofType<SaveQuestionListSuccess>(QuestionListActions.SAVE_LIST_SUCCESS)
        .pipe(
            tap((action: SaveQuestionListSuccess) => this.router.navigate(['/questionlists', action.payload.id]))
        );

    @Effect({ dispatch: false }) navigateToOverviewAfterRemove$: Observable<Action> = this.actions$
        .ofType<SaveQuestionListSuccess>(QuestionListActions.REMOVE_LIST_SUCCESS)
        .pipe(
            tap(action => this.router.navigate(['/questionlists']))
        );
}
