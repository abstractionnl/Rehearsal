import {EventEmitter, Injectable} from "@angular/core";
import {CanDeactivate} from "@angular/router";
import {Observable} from "rxjs";
import {map,flatMap,switchMap} from "rxjs/operators";
import {Store} from "@ngrx/store";
import {AppState, selectIsPristine} from "../store/questionlist.state";
import {ConfirmSaveQuestionlistComponent, ResultAction} from "../components/confirm-save-questionlist/confirm-save-questionlist.component";
import {SAVE_LIST_FAILED, SAVE_LIST_SUCCESS, SaveQuestionList} from "../store/questionlist.actions";
import {Actions} from "@ngrx/effects";
import {BsModalService} from "ngx-bootstrap";

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
            .select(selectIsPristine)
            .pipe(
                switchMap(pristine => {
                    if (pristine)
                        return Promise.resolve(true);

                    let ref = this.modalService.show(ConfirmSaveQuestionlistComponent);
                    return (<EventEmitter<ResultAction>>(ref.content.selected)).pipe(
                        flatMap(x => {
                            switch (x.action) {
                                case 'continue':
                                    return Promise.resolve(true);
                                case 'save':
                                    this.save();

                                    return this.updates$
                                        .ofType(SAVE_LIST_SUCCESS, SAVE_LIST_FAILED)
                                        .pipe(map(action => action.type == SAVE_LIST_SUCCESS));
                                default:
                                    return Promise.resolve(false);
                            }
                        })
                    );
                })
            );
    }

    private save() {
        this.store.dispatch(new SaveQuestionList());
    }
}
