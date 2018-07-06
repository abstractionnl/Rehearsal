import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs";

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;
import {QuestionlistState, selectQuestionListOverview} from "../../../questionlist/store/questionlist.state";
import {Store} from "@ngrx/store";
import {RehearsalState} from "../../store/rehearsal.state";
import {CreateRehearsal} from "../../store/rehearsal.actions";

@Component({
    templateUrl: './start-rehearsal.page.html'
})
export class StartRehearsalPage implements OnInit {
    questionLists: Observable<QuestionListOverviewModel[]>;
    questionList: QuestionListModel;

    constructor(
        private questionListStore: Store<QuestionlistState>,
        private store: Store<RehearsalState>) {

    }

    ngOnInit(): void {
        this.questionLists = this.questionListStore.select(selectQuestionListOverview); // TODO: Remove dependecy to other module?
    }

    start(): void {
        this.store.dispatch(new CreateRehearsal({ questionListId: this.questionList.id }));
    }

    canStart(): boolean {
        return this.questionList != null;
    }
}
