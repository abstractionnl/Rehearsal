import {Component, EventEmitter, Input, Output} from "@angular/core";
import * as Joi from "joi-browser";
import {IValidationResult} from "../../../../validation";

import {Rehearsal} from "../../../../types";

@Component({
    selector: 'open-question',
    templateUrl: './open-question.component.html',
    //changeDetection: ChangeDetectionStrategy.OnPush
})
export class OpenQuestionComponent {
    private _question: Rehearsal.OpenQuestionModel;
    private _answerGiven: boolean;
    private _answerResult: Rehearsal.AnswerResultModel;

    @Output() onSubmit: EventEmitter<string> = new EventEmitter<string>();
    @Output() onNext: EventEmitter<void> = new EventEmitter<void>();

    public focusAnswerField = new EventEmitter<void>();
    public focusNextButton = new EventEmitter<void>();

    answer: string = '';

    @Input('question')
    set question(value: Rehearsal.OpenQuestionModel) {
        this._question = value;
        this.answer = '';
        this._answerGiven = false;
        this.focusAnswerField.emit();
    }

    get question(): Rehearsal.OpenQuestionModel {
        return this._question;
    }

    get answerGiven() {
        return this._answerGiven || this._answerResult;
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
            OpenQuestionComponent.validateAnswer(this.answer)
                .then(a => this.onSubmit.emit(a));
        }
        if (this.canGotoNext()) {
            this.onNext.emit();
        }
    }

    canSubmit(): boolean {
        return !this._answerGiven && OpenQuestionComponent.validateAnswer(this.answer).error == null;
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
        return Joi.validate(answer, OpenQuestionComponent.validationSchema);
    }
}
