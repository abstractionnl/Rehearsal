import { Component, OnInit } from '@angular/core';
import { Observable } from "rxjs";

import {Rehearsal, QuestionList} from "../../../../types";
import RehearsalQuestionType = Rehearsal.RehearsalQuestionType;
import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;

import {QuestionlistState, selectQuestionListOverview} from "../../../questionlist/store/questionlist.state";
import {Store} from "@ngrx/store";
import {RehearsalState} from "../../store/rehearsal.state";
import {CreateRehearsal} from "../../store/rehearsal.actions";

interface RehearsalQuestionTypeChoice {
    questionType: RehearsalQuestionType;
    label: string;
}

@Component({
    templateUrl: './start-rehearsal.page.html'
})
export class StartRehearsalPage implements OnInit {
    questionLists: Observable<QuestionListOverviewModel[]>;
    questionList: QuestionListModel;
    questionTypes: RehearsalQuestionTypeChoice[];
    questionType: RehearsalQuestionType;

    constructor(
        private questionListStore: Store<QuestionlistState>,
        private store: Store<RehearsalState>) {
        this.questionType = Rehearsal.RehearsalQuestionType.Open;
        this.questionTypes = [
            { questionType: RehearsalQuestionType.Open, label: "Open vragen" },
            { questionType: RehearsalQuestionType.MultipleChoice, label: "Meerkeuze" }
        ];
    }

    ngOnInit(): void {
        this.questionLists = this.questionListStore.select(selectQuestionListOverview); // TODO: Remove dependecy to other module?
    }

    start(): void {
        this.store.dispatch(new CreateRehearsal({ questionListId: this.questionList.id, questionType: this.questionType }));
    }

    canStart(): boolean {
        return this.questionList != null;
    }
}
