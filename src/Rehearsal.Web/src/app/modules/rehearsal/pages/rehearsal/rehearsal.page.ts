import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, ParamMap} from "@angular/router";
import {RehearsalService} from "../../services/rehearsal.service";
import {map, switchMap} from "rxjs/operators";

import RehearsalSessionModel = Rehearsal.RehearsalSessionModel;
import AnswerResultModel = Rehearsal.AnswerResultModel;

@Component({
    templateUrl: './rehearsal.page.html'
})
export class RehearsalPage implements OnInit {
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
            .pipe(
                map((x: ParamMap) => x.get('id')),
                switchMap(id => this.rehearsalService.get(id))
            )
            .subscribe(rehearsal => this.startRehearsal(rehearsal));
    }

    get currentQuestion() {
        if (this.rehearsal && this.rehearsal.questions.length > this._questionIndex)
            return this.rehearsal.questions[this._questionIndex];

        return null;
    }

    get questionIndex() {
        return this._questionIndex;
    }

    submitAnswer(answer: string) {
        this.rehearsalService.giveAnswer(this.rehearsal.id, this.currentQuestion.id, answer)
            .subscribe(answerResult => {
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
