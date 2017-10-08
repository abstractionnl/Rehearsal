/// <reference path="../types.ts" />

import { Component, EventEmitter, OnInit, ViewChild } from '@angular/core';
import { NgForm } from "@angular/forms";

import { QuestionListService } from './questionlist.service';
import { ActivatedRoute, Router} from "@angular/router";
import { AlertService } from "../alert/alert.service";
import { ICanComponentDeactivate } from "../can-deactivate-guard.service";
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmSaveQuestionListComponent as ConfirmSaveQuestionComponent, ResultAction } from "./confirm-save-question.component";

import "rxjs/add/observable/of";
import "rxjs/add/operator/delay";

import QuestionListModel = QuestionList.QuestionListModel;
import QuestionModel = QuestionList.QuestionModel;

@Component({
    templateUrl: './questionlist-detail.component.html',
    styleUrls: [ './questionlist-detail.component.css' ],
})
export class QuestionlistDetailComponent implements OnInit, ICanComponentDeactivate {
    questionList: QuestionListModel;

    @ViewChild('form') public form: NgForm;

    constructor(
        private questionListService: QuestionListService,
        private alertService: AlertService,
        private route: ActivatedRoute,
        private modalService: BsModalService,
        private router: Router
    ) {

    }

    ngOnInit() {
        this.route.data
            .subscribe((data: { questionList: QuestionListModel }) => {
                this.questionList = data.questionList;
                this.form.form.markAsPristine();
                this.form.form.markAsUntouched();
            });
    }

    addQuestion(): void {
        this.questionList.questions.push({ question: '', answer: '' });
    }

    removeQuestion(item: QuestionModel) {
        this.questionList.questions = this.questionList.questions.filter(x => x != item);
    }

    save() {
        this.saveInternal(true);
    }

    delete() {
        this.questionListService.delete(this.questionList.id)
            .then(
                _ => {
                    this.alertService.success(`Woordenlijst ${this.questionList.title} verwijderd`);
                    this.form.form.markAsPristine();
                    this.router.navigate(['/questionlists']);
                },
                err => {
                    this.alertService.fail('Fout bij het verwijderen van de woordenlijst', err);
                    return false;
                }
            );
    }

    private saveInternal(navigateToNewList: boolean): Promise<boolean> {
        if (!this.form.valid) {
            this.alertService.warning('Er zitten nog fouten in de woordenlijst', null);
            return Promise.resolve(false);
        }

        if (this.questionList.id) {
            // update
            return this.questionListService.update(this.questionList)
                .then(
                    _ => {
                        this.alertService.success(`Woordenlijst ${this.questionList.title} opgeslagen`);
                        this.form.form.markAsPristine();
                        return true; },
                    err => {
                        this.alertService.fail('Fout bij het opslaan van de woordenlijst', err);
                        return false;
                    }
                );
        } else {
            return this.questionListService.create(this.questionList)
                .then(
                    id => {
                        this.alertService.success(`Woordenlijst ${this.questionList.title} opgeslagen`);
                        this.form.form.markAsPristine();
                        if (navigateToNewList) this.router.navigate(['/questionlists', id]);
                        return true;
                    },
                    err => {
                        this.alertService.fail('Fout bij het opslaan van de woordenlijst', err);
                        return false;
                    }
                );
        }
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
                    return this.saveInternal(false);
                default:
                    return Promise.resolve(false);
            }
        });

        return event;
    }
}
