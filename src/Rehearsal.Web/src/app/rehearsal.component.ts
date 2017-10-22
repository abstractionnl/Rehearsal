import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Route, Router, RouterState, RouterStateSnapshot} from "@angular/router";
import { Observable } from "rxjs/Observable";

import { QuestionListService } from "./questionlist/questionlist.service";
import {RehearsalService} from "./rehearsal.service";

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;
import OpenRehearsalQuestionModel = Rehearsal.OpenRehearsalQuestionModel;
import RehearsalSessionModel = Rehearsal.RehearsalSessionModel;
import AnswerResultModel = Rehearsal.AnswerResultModel;

@Component({
    template: `
    <div class="row">
        <div class="col-lg-offset-3 col-lg-6" style="margin-top: 50px;">
            <div class="well well-lg">
                <rehearsal-question [question]="currentQuestion" [answerResult]="answerResult" (onSubmit)="submitAnswer($event)" (onNext)="gotoNext()" *ngIf="currentQuestion"></rehearsal-question>
                <p class="text-info text-center" *ngIf="isDone()">Dat wassum</p>
            </div>
        </div>
    </div>`
})
export class RehearsalComponent implements OnInit {
    rehearsal: RehearsalSessionModel;
    answerResult: AnswerResultModel;

    private _questionIndex: number;

    constructor(
        private rehearsalService : RehearsalService,
        private route: ActivatedRoute) {

    }

    ngOnInit() {
        this.route
            .paramMap
            .map(x => x.get('id'))
            .switchMap(id => this.rehearsalService.get(id))
            .subscribe(rehearsal => this.startRehearsal(rehearsal));
    }

    get currentQuestion() {
        if (this.rehearsal && this.rehearsal.questions.length > this._questionIndex)
            return this.rehearsal.questions[this._questionIndex];

        return null;
    }

    submitAnswer(answer: string) {
        this.rehearsalService.giveAnswer(this.rehearsal.id, this.currentQuestion.id, answer)
            .toPromise()
            .then(answerResult => {
                this.answerResult = answerResult;
            });
    }

    gotoNext() {
        this._questionIndex++;
        this.answerResult = null;
    }

    isDone(): boolean {
        return this.rehearsal && this.rehearsal.questions.length <= this._questionIndex;
    }

    private startRehearsal(rehearsal: RehearsalSessionModel) {
        this.rehearsal = rehearsal;
        this._questionIndex = 0;
    }
}
