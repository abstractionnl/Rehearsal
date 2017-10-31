import {Injectable} from "@angular/core";
import {AlertService} from "../alert.service";
import {Actions, Effect} from "@ngrx/effects";
import {Observable} from "rxjs/Observable";
import {Action} from "@ngrx/store";

import * as QuestionListActions from "../../questionlist/store/questionlist.actions";
import {
    LoadQuestionListFailed,
    RemoveQuestionListFailed,
    RemoveQuestionListSuccess, SaveQuestionListFailed,
    SaveQuestionListSuccess
} from "../../questionlist/store/questionlist.actions";
import {empty} from "rxjs/observable/empty";

import QuestionListModel = QuestionList.QuestionListModel;

@Injectable()
export class AlertEffects {
    constructor(
        private alertService: AlertService,
        private actions$: Actions
    ) {}

    @Effect({ dispatch: false }) LoadQuestionListFailed: Observable<Action> = this.actions$
        .ofType<LoadQuestionListFailed>(QuestionListActions.LOAD_LIST_FAILED)
        .do(this.showFailed('Fout bij het laden van de woordenlijst'));

    @Effect({ dispatch: false }) SaveQuestionListSuccess: Observable<Action> = this.actions$
        .ofType<SaveQuestionListSuccess>(QuestionListActions.SAVE_LIST_SUCCESS)
        .do(this.showSuccess<QuestionListModel>(questionList => `Woordenlijst ${questionList.title} opgeslagen`));

    @Effect({ dispatch: false }) SaveQuestionListFailed: Observable<Action> = this.actions$
        .ofType<SaveQuestionListFailed>(QuestionListActions.SAVE_LIST_FAILED)
        .do(this.showFailed('Fout bij het opslaan van de woordenlijst'));

    @Effect({ dispatch: false }) RemoveQuestionListSuccess: Observable<Action> = this.actions$
        .ofType<RemoveQuestionListSuccess>(QuestionListActions.REMOVE_LIST_SUCCESS)
        .do(this.showSuccessT('Woordenlijst verwijderd'));

    @Effect({ dispatch: false }) RemoveQuestionListFailed: Observable<Action> = this.actions$
        .ofType<RemoveQuestionListFailed>(QuestionListActions.REMOVE_LIST_FAILED)
        .do(this.showFailed('Fout bij het verwijderen van de woordenlijst'));

    private showSuccess<T>(createMessage: (payload: T) => string): (action: { type: string, payload: T }) => void {
        return action => {
            this.alertService.success(createMessage(action.payload));
        }
    }

    private showSuccessT(message: string): (action: any) => void {
        return action => {
            this.alertService.success(message);
        }
    }

    private showFailed(message: string): (action: any) => void {
        return action => {
            this.alertService.fail(message, action.payload);
        }
    }
}
