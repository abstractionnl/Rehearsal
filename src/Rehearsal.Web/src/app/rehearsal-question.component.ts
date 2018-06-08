/// <reference path="types.ts" />

import {Component, EventEmitter, Input, Output} from "@angular/core";
import * as Joi from "joi-browser";
import {IValidationResult} from "./validation";

@Component({
    selector: 'rehearsal-question',
    templateUrl: './rehearsal-question.component.html',
    //changeDetection: ChangeDetectionStrategy.OnPush
})
export class RehearsalQuestionComponent {
    private _question: Rehearsal.OpenRehearsalQuestionModel;
    private _answerGiven: boolean;
    private _answerResult: Rehearsal.AnswerResultModel;

    @Output() onSubmit: EventEmitter<string> = new EventEmitter<string>();
    @Output() onNext: EventEmitter<void> = new EventEmitter<void>();

    public focusAnswerField = new EventEmitter<void>();
    public focusNextButton = new EventEmitter<void>();

    answer: string = '';

    @Input('question')
    set question(value: Rehearsal.OpenRehearsalQuestionModel) {
        this._question = value;
        this.answer = '';
        this._answerGiven = false;
        this.focusAnswerField.emit();
    }

    get question() {
        return this._question;
    }

    get answerGiven() {
        return this._answerGiven;
    }

    @Input('answerResult')
    set answerResult(value: Rehearsal.AnswerResultModel) {
        this._answerResult = value;
        this.focusNextButton.emit();
    }

    get answerResult() {
        return this._answerResult;
    }

    submit(): void {
        if (this.canSubmit()) {
            this._answerGiven = true;
            RehearsalQuestionComponent.validateAnswer(this.answer)
                .then(a => this.onSubmit.emit(a));
        }
        if (this.canGotoNext()) {
            this.onNext.emit();
        }
    }

    canSubmit(): boolean {
        return !this._answerGiven && RehearsalQuestionComponent.validateAnswer(this.answer).error == null;
    }

    canGotoNext(): boolean {
        return !!this.answerResult;
    }

    isCorrect(): boolean {
        return this.answerResult && this.answerResult.isCorrect;
    }

    isInCorrect(): boolean {
        return this.answerResult && !this.answerResult.isCorrect;
    }

    static readonly validationSchema = Joi.string().trim().required();

    static validateAnswer(answer: string): IValidationResult<string> {
        return Joi.validate(answer, RehearsalQuestionComponent.validationSchema);
    }
}
