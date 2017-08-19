/// <reference path="types.ts" />

import {Component, Input} from '@angular/core';

@Component({
    selector: 'questionlist-form',
    templateUrl: './questionlist-form.component.html',
    styleUrls: [ './questionlist-form.component.css' ],
})
export class QuestionlistFormComponent {
    private _questionList: Rehearsal.QuestionList;

    @Input() set questionList(questionList: Rehearsal.QuestionList) {
        this._questionList = questionList;
    }

    get questionList(): Rehearsal.QuestionList { return this._questionList; }

    addQuestion(): void {
        this._questionList.questions.push({ question: '', answer: '' });
    }

    removeQuestion(item: Rehearsal.QuestionList.Item) {
        this._questionList.questions = this._questionList.questions.filter(x => x != item);
    }
}
