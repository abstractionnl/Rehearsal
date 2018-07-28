import {AfterViewInit, Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import {FormArray, FormBuilder, FormGroup} from "@angular/forms";

import {Subscription} from "rxjs";
import {debounceTime, filter, map} from "rxjs/operators";

import {QuestionList} from "../../../../types";
import {FormGroupState} from "ngrx-forms";
import QuestionListModel = QuestionList.QuestionListModel;

@Component({
    selector: 'questionlist-form',
    templateUrl: './questionlist-form.component.html',
    styleUrls: [ './questionlist-form.component.css' ],
})
export class QuestionlistFormComponent {
    @Input() public formState: FormGroupState<QuestionListModel>;
    @Output() public onNewLine: EventEmitter<void> = new EventEmitter<void>();
    @Output() public onRemoveLine: EventEmitter<number> = new EventEmitter<number>();

    addQuestion(): void {
        this.onNewLine.emit();
    }

    removeQuestion(index: number): void {
        this.onRemoveLine.emit(index);
    }

    trackById(index, group): string {
        return group.id;
    }

    canSave() {
        return !this.formState.isPristine && this.formState.isValid && !this.formState.isValidationPending;
    }
}
