import {Injectable} from "@angular/core";
import {Router} from "@angular/router";
import {Actions, Effect} from "@ngrx/effects";
import {Observable, of} from "rxjs/index";
import {Action, Store} from "@ngrx/store";
import {
    CreateRehearsal,
    CreateRehearsalFailed,
    CreateRehearsalSuccess, GiveAnswer, GiveAnswerFailed, GiveAnswerSuccess,
    LoadRehearsal,
    LoadRehearsalSuccess
} from "./rehearsal.actions";

import * as RehearsalActions from "./rehearsal.actions";
import {switchMap, withLatestFrom} from "rxjs/internal/operators";
import {RehearsalService} from "../services/rehearsal.service";
import {catchError, map, tap} from "rxjs/operators";
import {RehearsalState, selectCurrentQuestion, selectCurrentSession} from "./rehearsal.state";

@Injectable()
export class RehearsalEffects {
    constructor(
        private rehearsalService: RehearsalService,
        private router: Router,
        private actions$: Actions,
        private state$: Store<RehearsalState>
    ) {

    }

    @Effect() createRehearsal$: Observable<Action> = this.actions$
        .ofType<CreateRehearsal>(RehearsalActions.CREATE_REHEARSAL)
        .pipe(
            switchMap(action =>
                this.rehearsalService
                    .start({ questionListId: action.payload.questionListId })
                    .pipe(
                        map(id => new CreateRehearsalSuccess({rehearsalId: id})),
                        catchError(err => of(new CreateRehearsalFailed(err)))
                    )
            )
        );

    @Effect() loadRehearsal$: Observable<Action> = this.actions$
        .ofType<LoadRehearsal>(RehearsalActions.LOAD_REHEARSAL)
        .pipe(
            switchMap(action =>
                this.rehearsalService
                    .get(action.payload.id)
                    .pipe(
                        map(session => new LoadRehearsalSuccess(session)),
                        catchError(err => of(new CreateRehearsalFailed(err)))
                    )
            )
        );

    @Effect() giveAnswer$: Observable<Action> = this.actions$
        .ofType<GiveAnswer>(RehearsalActions.GIVE_ANSWER)
        .pipe(
            withLatestFrom(
                this.state$.select(selectCurrentSession),
                this.state$.select(selectCurrentQuestion),
                (action, session, question) => ({ answer: action.payload, rehearsalId: session.id, currentQuestionId: question.id })
            ),
            switchMap(x =>
                this.rehearsalService
                    .giveAnswer(x.rehearsalId, x.currentQuestionId, x.answer)
                    .pipe(
                        map(result => new GiveAnswerSuccess(result)),
                        catchError(err => of(new GiveAnswerFailed(err)))
                    )
            )
        );

    @Effect({ dispatch: false }) navigateToRehearsalAfterCreate$: Observable<Action> = this.actions$
        .ofType<CreateRehearsalSuccess>(RehearsalActions.CREATE_REHEARSAL_SUCCESS)
        .pipe(
            tap((action: CreateRehearsalSuccess) => this.router.navigate(['/rehearsal', action.payload.rehearsalId]))
        );
}
