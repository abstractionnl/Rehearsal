/// <reference path="../types.ts" />

import {Component, EventEmitter, OnInit} from "@angular/core";
import {Router} from "@angular/router";
import {Observable} from "rxjs/Observable";

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import Guid = System.Guid;
import QuestionListModel = QuestionList.QuestionListModel;

import {ICanComponentDeactivate} from "../can-deactivate-guard.service";
import {ConfirmSaveQuestionListComponent, ResultAction} from "./confirm-save-question.component";
import {BsModalService} from "ngx-bootstrap";

import {Store} from "@ngrx/store";
import {
    AppState, selectIsPristine, selectIsValid, selectQuestionListOverview,
    selectSelectedQuestionList
} from "./store/questionlist.state";
import {QuestionListEdited, RemoveQuestionList, SaveQuestionList} from "./store/questionlist.actions";

@Component({
    templateUrl: 'questionlist-editor.component.html'
})
export class QuestionlistEditorComponent implements OnInit, ICanComponentDeactivate {

    questionLists$: Observable<QuestionListOverviewModel[]>;
    selectedList$: Observable<QuestionListModel>;

    listIsValid$: Observable<boolean>;
    listIsPristine$: Observable<boolean>;

    constructor(
        private store: Store<AppState>,
        private router: Router,
        private modalService: BsModalService)
    {
        this.questionLists$ = this.store.select(selectQuestionListOverview);
        this.selectedList$ = this.store.select(selectSelectedQuestionList);
        this.listIsValid$ = this.store.select(selectIsValid);
        this.listIsPristine$ = this.store.select(selectIsPristine);
    }

    ngOnInit(): void {
    }

    selectList(id: Guid) {
        this.router.navigate(['/questionlists', id])
    }

    new() {
        this.router.navigate(['/questionlists', 'new']);
    }

    save(questionList: QuestionListModel) {
        /*if (!this.form.valid) {
            this.alertService.warning('Er zitten nog fouten in de woordenlijst', null);
            return Promise.resolve(false);
        }*/

        this.store.dispatch(new SaveQuestionList(questionList));
    }

    changed(questionList: QuestionListModel) {
        console.log(questionList);
        this.store.dispatch(new QuestionListEdited(questionList));
    }

    delete(questionList: QuestionListModel) {
        this.store.dispatch(new RemoveQuestionList(questionList));
    }

    canDeactivate(): Observable<boolean> {
        return this.selectedList$
            .switchMap(list => {
                //if (!list || this.listIsPristine) {
                //    return Promise.resolve(true);
                //}

                return Promise.resolve(true);

                /*
                let ref = this.modalService.show(ConfirmSaveQuestionListComponent);
                let event = (<EventEmitter<ResultAction>>(ref.content.selected)).flatMap(x => {
                    switch (x.action) {
                        case 'continue':
                            return Promise.resolve(true);
                        case 'save':
                            if (!this.listIsValid) {
                                //this.alertService.warning('Er zitten nog fouten in de woordenlijst', null);
                                return Promise.resolve(false);
                            }

                            //TODO: We must block until saved, but this is done async so we must wait until success
                            //return this.save(list);
                        default:
                            return Promise.resolve(false);
                    }
                });

                return event;*/
            });
    }
}
