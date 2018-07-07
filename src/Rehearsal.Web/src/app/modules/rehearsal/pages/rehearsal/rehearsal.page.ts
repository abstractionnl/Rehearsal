import { Component, OnInit } from '@angular/core';
import {RehearsalService} from "../../services/rehearsal.service";
import {Store} from "@ngrx/store";

import {Rehearsal} from "../../../../types";
import RehearsalSessionModel = Rehearsal.RehearsalSessionModel;
import AnswerResultModel = Rehearsal.AnswerResultModel;
import RehearsalQuestionModel = Rehearsal.RehearsalQuestionModel;

import {
    RehearsalState, selectAnsweredQuestionCount,
    selectCurrentQuestion, selectCurrentResult, selectIsFinished,
    selectQuestionCount
} from "../../store/rehearsal.state";
import {Observable} from "rxjs/index";
import {GiveAnswer, NextQuestion} from "../../store/rehearsal.actions";

@Component({
    templateUrl: './rehearsal.page.html'
})
export class RehearsalPage {
    rehearsal: RehearsalSessionModel;
    answerResult: AnswerResultModel;

    private _questionIndex: number;

    currentQuestion: Observable<RehearsalQuestionModel>;
    currentResult: Observable<AnswerResultModel>;
    totalQuestions: Observable<number>;
    answeredQuestions: Observable<number>;
    isFinished: Observable<boolean>;

    constructor(
        private rehearsalService : RehearsalService,
        private store: Store<RehearsalState>) {
        this.currentQuestion = store.select(selectCurrentQuestion);
        this.currentResult = store.select(selectCurrentResult);
        this.totalQuestions = store.select(selectQuestionCount);
        this.answeredQuestions = store.select(selectAnsweredQuestionCount);
        this.isFinished = store.select(selectIsFinished);
    }

    submitAnswer(answer: string) {
        this.store.dispatch(new GiveAnswer(answer));

        /*this.rehearsalService.giveAnswer(this.rehearsal.id, this.currentQuestion.id, answer)
            .subscribe(answerResult => {
                this.answerResult = answerResult;
            });*/
    }

    gotoNext() {
        this.store.dispatch(new NextQuestion());
    }

    isDone(): boolean {
        return this.rehearsal && this.rehearsal.questions.length <= this._questionIndex;
    }
}
