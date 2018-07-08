import {Component, EventEmitter, Input, Output} from "@angular/core";

import {Rehearsal} from "../../../../types";

@Component({
    selector: 'multiplechoice-question',
    templateUrl: './multiplechoice-question.component.html',
    styleUrls: [ './multiplechoice-question.component.css' ]
    //changeDetection: ChangeDetectionStrategy.OnPush
})
export class MultipleChoiceQuestionComponent {
    private _question: Rehearsal.MultipleChoiceQuestionModel;
    private _answerGiven: boolean;
    private _answerResult: Rehearsal.AnswerResultModel;

    @Output() onSubmit: EventEmitter<string> = new EventEmitter<string>();
    @Output() onNext: EventEmitter<void> = new EventEmitter<void>();

    public focusAnswerField = new EventEmitter<void>();
    public focusNextButton = new EventEmitter<void>();

    answer: string;

    @Input('question')
    set question(value: Rehearsal.MultipleChoiceQuestionModel) {
        this._question = value;
        this.answer = null;
        this._answerGiven = false;
        this.focusAnswerField.emit();
    }

    get question(): Rehearsal.MultipleChoiceQuestionModel {
        return this._question;
    }

    get answerGiven() {
        return this._answerGiven || this._answerResult;
    }

    @Input('answerResult')
    set answerResult(value: Rehearsal.AnswerResultModel) {
        this.answer = value !== null ? value.givenAnswer : null;
        this._answerResult = value;
        this.focusNextButton.emit();
    }

    get answerResult() {
        return this._answerResult;
    }

    submit(): void {
        if (this.canSubmit()) {
            this._answerGiven = true;
            this.onSubmit.emit(this.answer);
        }
        if (this.canGotoNext()) {
            this.onNext.emit();
        }
    }

    canSubmit(): boolean {
        return !this._answerGiven && this.answer !== null;
    }

    canGotoNext(): boolean {
        return !!this.answerResult;
    }

    isCorrect(answer: number): boolean {
        return this.answerResult && this.answerResult.correctAnswers.includes(String(answer));
    }

    isInCorrect(answer: number): boolean {
        return this.answerResult && !this.answerResult.correctAnswers.includes(String(answer));
    }

    isGivenAnswer(answer: number): boolean {
        return this.answer && this.answer === String(answer);
    }
}
