/// <reference path="../../../../types.ts" />

import {AfterViewInit, Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import {FormArray, FormBuilder, FormGroup, NgForm} from "@angular/forms";

import {Subscription} from "rxjs";
import {debounceTime, filter, map} from "rxjs/operators";

@Component({
    selector: 'questionlist-form',
    templateUrl: './questionlist-form.component.html',
    styleUrls: [ './questionlist-form.component.css' ],
})
export class QuestionlistFormComponent implements AfterViewInit, OnDestroy {

    constructor(private fb: FormBuilder) {
        this.form = this.fb.group({
            'title': '',
            'questionTitle': '',
            'answerTitle': '',
            'questions': this.fb.array([])
        });
    }

    private valueChangeSubscription: Subscription;

    public form: FormGroup;
    private _questionList: QuestionList.QuestionListModel;
    @Output() public onSave: EventEmitter<any> = new EventEmitter<any>();
    @Output() public onChange: EventEmitter<QuestionList.QuestionListModel> = new EventEmitter<QuestionList.QuestionListModel>();

    @Input() public canSave: boolean;

    private isReloading: boolean = false;

    @Input()
    set questionList(value: QuestionList.QuestionListModel) {
        this.isReloading = true;
        this._questionList = value;

        while (this.questions.length < value.questions.length) {
            this.questions.push(this.fb.group({
                'question': '',
                'answer': ''
            }));
        }

        while (this.questions.length > value.questions.length) {
            this.questions.removeAt(this.questions.controls.length-1);
        }

        this.form.patchValue(value, { emitEvent: false });
        this.isReloading = false;
    }

    get questionList(): QuestionList.QuestionListModel {
        return this._questionList;
    }

    get questions(): FormArray {
        return this.form.get('questions') as FormArray;
    };

    ngAfterViewInit() {
        this.valueChangeSubscription = this.form.valueChanges
            .pipe(
                filter(_ => !this.isReloading),
                debounceTime(250),
                map(_ => this.form.value)
            )
            .subscribe(x => this.onChange.emit(x));
    }

    ngOnDestroy() {
        this.valueChangeSubscription
            .unsubscribe();
    }

    addQuestion(): void {
        this.questions.push(this.fb.group({ question: '', answer: '' }));
    }

    removeQuestion(index: number) {
        this.questions.removeAt(index);
    }

    save() {
        if (this.canSave) {
            this.onSave.emit();
        }
    }
}
