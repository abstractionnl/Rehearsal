/// <reference path="types.ts" />

import {Component, EventEmitter, OnInit, ViewChild} from '@angular/core';
import { NgForm } from "@angular/forms";
import { Observable } from 'rxjs/Observable';

import { QuestionListService } from './questionlist.service';
import { ActivatedRoute, Router} from "@angular/router";
import { AlertService } from "./error/alert.service";
import { ICanComponentDeactivate } from "./can-deactivate-guard.service";
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmSaveQuestionComponent, ResultAction } from "./confirm-save-question.component";

import "rxjs/add/observable/of";
import "rxjs/add/operator/delay";

@Component({
    templateUrl: './questionlist-detail.component.html',
    styleUrls: [ './questionlist-detail.component.css' ],
})
export class QuestionlistDetailComponent implements OnInit, ICanComponentDeactivate {
    questionList: Rehearsal.QuestionList;

    @ViewChild('form') public form: NgForm;

    constructor(
        private questionListService: QuestionListService,
        private alertService: AlertService,
        private route: ActivatedRoute,
        private modalService: BsModalService
    ) {

    }

    ngOnInit() {
        this.route.data
            .subscribe((data: { questionList: Rehearsal.QuestionList }) => {
                this.questionList = data.questionList;
                this.form.form.markAsPristine();
            });
    }

    addQuestion(): void {
        this.questionList.questions.push({ question: '', answer: '' });
    }

    removeQuestion(item: Rehearsal.QuestionList.Item) {
        this.questionList.questions = this.questionList.questions.filter(x => x != item);
    }

    save() {
        this.saveInternal();
    }

    private saveInternal(): Promise<boolean> {
        if (!this.form.valid) {
            this.alertService.warning('Er zitten nog fouten in de woordenlijst', null);
            return Promise.resolve(false);
        }

        return this.questionListService.update(this.questionList)
            .then(
                _ => { this.alertService.success(`Woordenlijst ${this.questionList.title} opgeslagen`); return true; },
                err => {
                    this.alertService.fail('Fout bij het opslaan van de woordenlijst', err);
                    return false;
                }
            );
    }

    canDeactivate() {
        if (this.form.pristine) {
            return true;
        }

        let ref = this.modalService.show(ConfirmSaveQuestionComponent);
        let event = (<EventEmitter<ResultAction>>(ref.content.selected)).flatMap(x => {
            switch (x.action) {
                case 'continue':
                    return Promise.resolve(true);
                case 'save':
                    return this.saveInternal();
                default:
                    return Promise.resolve(false);
            }
        });

        return event;
    }
}
