﻿/// <reference path="../types.ts" />

import {AfterViewInit, Component, EventEmitter, Input, OnDestroy, Output } from '@angular/core';
import {FormArray, FormBuilder, FormGroup, NgForm} from "@angular/forms";

import "rxjs/add/observable/of";
import "rxjs/add/operator/delay";
import {Subscription} from "rxjs/Subscription";

import _ from "lodash";

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
    @Output() public onSave: EventEmitter<QuestionList.QuestionListModel> = new EventEmitter<QuestionList.QuestionListModel>();
    @Output() public onDelete: EventEmitter<QuestionList.QuestionListModel> = new EventEmitter<QuestionList.QuestionListModel>();
    @Output() public onChange: EventEmitter<QuestionList.QuestionListModel> = new EventEmitter<QuestionList.QuestionListModel>();

    @Input() public isValid;
    @Input() public isPristine;

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

        /*this.form.setControl('questions', this.fb.array(value.questions.map(_ => ))));*/
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
            .filter(_ => !this.isReloading)
            .debounceTime(250)
            .map(_ => this.form.value)
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
        if (this.canSave()) {
            this.onSave.emit(this.questionList);
            this.form.markAsPristine();
        }
    }

    canSave(): boolean {
        return !this.isPristine && this.isValid;
    }

    delete() {
        if (this.canDelete()) {
            this.onDelete.emit(this.questionList);
        }
    }

    canDelete(): boolean {
        return this.questionList && !!this.questionList.id;
    }

    swap() {
        let current = this.form.value;

        let turned = {
            ...current,
            questionTitle: current.answerTitle,
            answerTitle: current.questionTitle,
            questions: current.questions.map(q => ({
                ...q,
                question: q.answer,
                answer: q.question
            }))
        };

        this.onChange.emit(turned);
    }
}