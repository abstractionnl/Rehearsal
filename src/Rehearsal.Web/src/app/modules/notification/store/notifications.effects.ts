import {Injectable} from "@angular/core";
import {NotificationsService} from "../services/notifications.service";
import {Actions, Effect} from "@ngrx/effects";
import {Observable} from "rxjs";
import {Action} from "@ngrx/store";

import * as QuestionListActions from "../../questionlist/store/questionlist.actions";
import {
    LoadQuestionListFailed,
    RemoveQuestionListFailed,
    RemoveQuestionListSuccess, SaveQuestionListFailed,
    SaveQuestionListSuccess
} from "../../questionlist/store/questionlist.actions";

import {ErrorNotification, SuccessNotification} from "./notifications.actions";
import {map} from "rxjs/internal/operators";

@Injectable()
export class NotificationsEffects {
    constructor(
        private alertService: NotificationsService,
        private actions$: Actions
    ) {}

    @Effect() LoadQuestionListFailed: Observable<Action> = this.actions$
        .ofType<LoadQuestionListFailed>(QuestionListActions.LOAD_LIST_FAILED)
        .pipe(
            map(_ => new ErrorNotification('Fout bij het laden van de woordenlijst'))
        );

    @Effect() SaveQuestionListSuccess: Observable<Action> = this.actions$
        .ofType<SaveQuestionListSuccess>(QuestionListActions.SAVE_LIST_SUCCESS)
        .pipe(
            map(a => a.payload),
            map(questionList => new SuccessNotification(`Woordenlijst ${questionList.title} opgeslagen`))
        );

    @Effect() SaveQuestionListFailed: Observable<Action> = this.actions$
        .ofType<SaveQuestionListFailed>(QuestionListActions.SAVE_LIST_FAILED)
        .pipe(
            map(_ => new ErrorNotification('Fout bij het opslaan van de woordenlijst'))
        );

    @Effect() RemoveQuestionListSuccess: Observable<Action> = this.actions$
        .ofType<RemoveQuestionListSuccess>(QuestionListActions.REMOVE_LIST_SUCCESS)
        .pipe(
            map(_ => new SuccessNotification('Woordenlijst verwijderd'))
        );

    @Effect() RemoveQuestionListFailed: Observable<Action> = this.actions$
        .ofType<RemoveQuestionListFailed>(QuestionListActions.REMOVE_LIST_FAILED)
        .pipe(
            map(_ => new ErrorNotification('Fout bij het verwijderen van de woordenlijst'))
        );
}
