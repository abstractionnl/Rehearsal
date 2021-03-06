import {Component, HostListener, OnInit} from '@angular/core';
import {RehearsalService} from "../../services/rehearsal.service";
import {Store} from "@ngrx/store";

import {Rehearsal} from "../../../../types";
import RehearsalSessionModel = Rehearsal.RehearsalSessionModel;
import AnswerResultModel = Rehearsal.AnswerResultModel;
import RehearsalQuestionModel = Rehearsal.RehearsalQuestionModel;

import {
    RehearsalState, selectAnsweredQuestionCount,
    selectCurrentQuestion, selectIncorrectAnsweredQuestions as selectIncorrectAnswers, selectIsFinished,
    selectQuestionCount
} from "../../store/rehearsal.state";
import {Observable} from "rxjs/index";
import {GiveAnswer, NextQuestion, PreviousQuestion, Repeat} from "../../store/rehearsal.actions";

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
    incorrectQuestions: Observable<RehearsalQuestionModel[]>;

    constructor(
        private rehearsalService : RehearsalService,
        private store: Store<RehearsalState>) {
        this.currentQuestion = store.select(selectCurrentQuestion);
        this.totalQuestions = store.select(selectQuestionCount);
        this.answeredQuestions = store.select(selectAnsweredQuestionCount);
        this.isFinished = store.select(selectIsFinished);
        this.incorrectQuestions = store.select(selectIncorrectAnswers);
    }

    submitAnswer(answer: string) {
        this.store.dispatch(new GiveAnswer(answer));
    }

    gotoNext() {
        this.store.dispatch(new NextQuestion());
    }

    repeat() {
        this.store.dispatch(new Repeat());
    }

    @HostListener('document:keydown', ['$event']) onKeydownHandler(event: KeyboardEvent) {
        if (event.key === 'ArrowLeft') {
            this.store.dispatch(new PreviousQuestion());
        }
        if (event.key === 'ArrowRight') {
            this.store.dispatch(new NextQuestion());
        }
    }
}
