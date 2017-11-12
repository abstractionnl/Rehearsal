/// <reference path="../types.ts" />

import {Component, OnInit} from "@angular/core";
import {Router} from "@angular/router";
import {Store} from "@ngrx/store";
import {Observable} from "rxjs/Observable";

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import Guid = System.Guid;
import QuestionListModel = QuestionList.QuestionListModel;

import {AppState, selectCanDelete, selectCanSave, selectCanSwap, selectQuestionListOverview, selectSelectedQuestionList} from "./store/questionlist.state";
import {QuestionListEdited, RemoveQuestionList, SaveQuestionList, SwapQuestionList} from "./store/questionlist.actions";

import 'rxjs/add/operator/first';

@Component({
    templateUrl: 'questionlist-editor.component.html'
})
export class QuestionlistEditorComponent implements OnInit {

    public questionLists$: Observable<QuestionListOverviewModel[]>;
    public selectedList$: Observable<QuestionListModel>;

    public canSave$: Observable<boolean>;
    public canDelete$: Observable<boolean>;
    public canSwap$: Observable<boolean>;

    constructor(
        private store: Store<AppState>,
        private router: Router)
    {
        this.questionLists$ = this.store.select(selectQuestionListOverview);
        this.selectedList$ = this.store.select(selectSelectedQuestionList);
        this.canSave$ = this.store.select(selectCanSave);
        this.canDelete$ = this.store.select(selectCanDelete);
        this.canSwap$ = this.store.select(selectCanSwap);
    }

    ngOnInit(): void {
    }

    selectList(id: Guid) {
        this.router.navigate(['/questionlists', id])
    }

    new() {
        this.router.navigate(['/questionlists', 'new']);
    }

    save() {
        this.store.dispatch(new SaveQuestionList());
    }

    changed(questionList: QuestionListModel) {
        this.store.dispatch(new QuestionListEdited(questionList));
    }

    delete() {
        this.store.dispatch(new RemoveQuestionList());
    }

    swap() {
        this.store.dispatch(new SwapQuestionList())
    }
}
