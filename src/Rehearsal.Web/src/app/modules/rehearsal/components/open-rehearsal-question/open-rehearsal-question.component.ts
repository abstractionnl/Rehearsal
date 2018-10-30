import {Component, EventEmitter, Input, Output} from "@angular/core";
import * as Joi from "joi-browser";
import {IValidationResult} from "../../../../validation";

import {Rehearsal} from "../../../../types";

@Component({
    selector: 'open-rehearsal-question',
    templateUrl: './open-rehearsal-question.component.html',
    //changeDetection: ChangeDetectionStrategy.OnPush
})
export class OpenRehearsalQuestionComponent {
    private _question: Rehearsal.OpenRehearsalQuestionModel;
    private _answerGiven: boolean;

    @Output() onSubmit: EventEmitter<string> = new EventEmitter<string>();
    @Output() onNext: EventEmitter<void> = new EventEmitter<void>();

    public focusAnswerField = new EventEmitter<void>();
    public focusNextButton = new EventEmitter<void>();

    answer: string = '';

    @Input('question')
    set question(value: Rehearsal.OpenRehearsalQuestionModel) {
        this._question = value;
        this.answer = value.givenAnswer;
        this._answerGiven = value.givenAnswer !== null;
        this.focusAnswerField.emit();
    }

    get question(): Rehearsal.OpenRehearsalQuestionModel {
        return this._question;
    }

    get answerGiven() {
        return this._answerGiven;
    }

    submit(): void {
        if (this.canSubmit()) {
            this._answerGiven = true;
            OpenRehearsalQuestionComponent.validateAnswer(this.answer)
                .then(a => this.onSubmit.emit(a));
        }
        if (this.canGotoNext()) {
            this.onNext.emit();
        }
    }

    canSubmit(): boolean {
        return !this._answerGiven && OpenRehearsalQuestionComponent.validateAnswer(this.answer).error == null;
    }

    canGotoNext(): boolean {
        return this._answerGiven && this._question.givenAnswer != null;
    }

    isCorrect(): boolean {
        return this._question.givenAnswer != null && this._question.answeredCorrectly;
    }

    isInCorrect(): boolean {
        return this._question.givenAnswer != null && !this._question.answeredCorrectly;
    }

    static readonly validationSchema = Joi.string().trim().required();

    static validateAnswer(answer: string): IValidationResult<string> {
        return Joi.validate(answer, OpenRehearsalQuestionComponent.validationSchema);
    }
}
