import {EventEmitter, Injectable} from "@angular/core";
import {CanDeactivate} from "@angular/router";
import {Observable} from "rxjs/Observable";
import {Store} from "@ngrx/store";
import {AppState} from "./store/questionlist.state";
import {ConfirmSaveQuestionListComponent, ResultAction} from "./confirm-save-question.component";
import { SAVE_LIST_FAILED, SAVE_LIST_SUCCESS, SaveQuestionList } from "./store/questionlist.actions";
import {Actions} from "@ngrx/effects";
import {BsModalService} from "ngx-bootstrap";
import QuestionListModel = QuestionList.QuestionListModel;

@Injectable()
export class QuestionListDeactivateGuard implements CanDeactivate<any> {
    constructor(
        private store: Store<AppState>,
        private updates$: Actions,
        private modalService: BsModalService
    ) {

    }

    canDeactivate(component: any) : Observable<boolean> {
        return this.store
            .select(x => x.questionListEditor)
            .switchMap(state => {
                if (state.isPristine)
                    return Promise.resolve(true);

                let ref = this.modalService.show(ConfirmSaveQuestionListComponent);
                let event = (<EventEmitter<ResultAction>>(ref.content.selected)).flatMap(x => {
                    switch (x.action) {
                        case 'continue':
                            return Promise.resolve(true);
                        case 'save':
                            this.save(state.list);

                            return this.updates$
                                .ofType(SAVE_LIST_SUCCESS, SAVE_LIST_FAILED)
                                .map(action => action.type == SAVE_LIST_SUCCESS);
                        default:
                            return Promise.resolve(false);
                    }
                });

                return event;
            });
    }

    private save(questionList: QuestionListModel) {
        this.store.dispatch(new SaveQuestionList(questionList));
    }
}
