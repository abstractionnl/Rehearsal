/// <reference path="../types.ts" />

import {AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild} from '@angular/core';
import { NgForm } from "@angular/forms";

import "rxjs/add/observable/of";
import "rxjs/add/operator/delay";
import {Subscription} from "rxjs/Subscription";

@Component({
    selector: 'questionlist-form',
    templateUrl: './questionlist-form.component.html',
    styleUrls: [ './questionlist-form.component.css' ],
})
export class QuestionlistFormComponent implements AfterViewInit, OnDestroy {

    private validChangeSubscription: Subscription;
    private pristineChangeSubscription: Subscription;

    @ViewChild('form') public form: NgForm;
    private _questionList: QuestionList.QuestionListModel;
    @Output() public onSave: EventEmitter<QuestionList.QuestionListModel> = new EventEmitter<QuestionList.QuestionListModel>();
    @Output() public onDelete: EventEmitter<QuestionList.QuestionListModel> = new EventEmitter<QuestionList.QuestionListModel>();
    @Output() public validChange: EventEmitter<boolean> = new EventEmitter<boolean>();
    @Output() public pristineChange: EventEmitter<boolean> = new EventEmitter<boolean>();

    @Input()
    set questionList(value: QuestionList.QuestionListModel) {
        this._questionList = value;

        if (this.form)
            this.form.form.markAsPristine();
    }

    get questionList(): QuestionList.QuestionListModel {
        return this._questionList;
    }

    ngAfterViewInit() {
        this.validChangeSubscription = this.form.form.valueChanges
            .map(_ => this.form.form.valid)
            .distinctUntilChanged()
            .subscribe(valid => {
                console.log('valid', valid);
                this.validChange.emit(valid);
            });

        this.pristineChangeSubscription = this.form.form.valueChanges
            .map(_ => this.form.form.pristine)
            .distinctUntilChanged()
            .subscribe(valid => {
                console.log('pristine', valid);
                this.pristineChange.emit(valid);
            });
    }

    ngOnDestroy() {
        this.validChangeSubscription
            .unsubscribe();
        this.pristineChangeSubscription
            .unsubscribe();
    }

    addQuestion(): void {
        this.questionList.questions.push({ question: '', answer: '' });
    }

    removeQuestion(item: QuestionList.QuestionModel) {
        this.questionList.questions = this.questionList.questions.filter(x => x != item);
    }

    save() {
        if (this.canSave()) {
            this.onSave.emit(this.questionList);
            this.form.form.markAsPristine();
        }
    }

    canSave(): boolean {
        return this.form && this.form.valid && !this.form.pristine;
    }

    delete() {
        if (this.canDelete()) {
            this.onDelete.emit(this.questionList);
        }
    }

    canDelete(): boolean {
        return this.questionList && !!this.questionList.id;
    }


}
