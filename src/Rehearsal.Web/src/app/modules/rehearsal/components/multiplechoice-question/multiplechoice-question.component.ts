import {Component, EventEmitter, HostListener, Input, Output} from "@angular/core";

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

    @Output() onSubmit: EventEmitter<string> = new EventEmitter<string>();
    @Output() onNext: EventEmitter<void> = new EventEmitter<void>();

    public focusCheckButton = new EventEmitter<void>();
    public focusNextButton = new EventEmitter<void>();

    answer: string;

    @Input('question')
    set question(value: Rehearsal.MultipleChoiceQuestionModel) {
        this._question = value;
        this.answer = value.givenAnswer;
        this._answerGiven = value.givenAnswer !== null;

        if (!this._answerGiven)
            this.focusCheckButton.emit();
        else
            this.focusNextButton.emit();
    }

    get question(): Rehearsal.MultipleChoiceQuestionModel {
        return this._question;
    }

    answerGiven(): boolean {
        return this._answerGiven;
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
        return this._answerGiven && this._question.givenAnswer !== null;
    }

    isCorrect(answer: number): boolean {
        return this._answerGiven && this._question.correctAnswer == answer;
    }

    isInCorrect(answer: number): boolean {
        return this._answerGiven && this._question.correctAnswer != answer;
    }

    isGivenAnswer(answer: number): boolean {
        return this.answer && this._question.givenAnswer === this._question.availableAnswers[answer];
    }

    @HostListener('document:keydown', ['$event']) onKeydownHandler(event: KeyboardEvent) {
        if (this.answerGiven()) return;

        let keyAsNumber = Number(event.key);

        if (!Number.isNaN(keyAsNumber)) {
            keyAsNumber = keyAsNumber-1;        // Numbers should be one indexed
            if (keyAsNumber in this._question.availableAnswers) {
                this.answer = this._question.availableAnswers[keyAsNumber];
                this.focusCheckButton.emit();
            }
        }
    }
}
