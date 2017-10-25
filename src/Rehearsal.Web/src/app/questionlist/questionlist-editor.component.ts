/// <reference path="../types.ts" />

import {Component, EventEmitter, OnInit} from "@angular/core";
import {ActivatedRoute, Router} from "@angular/router";
import {Observable} from "rxjs/Observable";


import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import Guid = System.Guid;
import QuestionListModel = QuestionList.QuestionListModel;

import {QuestionListService} from "./questionlist.service";
import {AlertService} from "../alert/alert.service";
import {ICanComponentDeactivate} from "../can-deactivate-guard.service";
import {ConfirmSaveQuestionListComponent, ResultAction} from "./confirm-save-question.component";
import {BsModalService} from "ngx-bootstrap";

import _ from "lodash";

@Component({
    templateUrl: 'questionlist-editor.component.html'
})
export class QuestionlistEditorComponent implements OnInit, ICanComponentDeactivate {

    questionLists$: Observable<QuestionListOverviewModel[]>;
    selectedList: QuestionListModel;

    listIsValid: boolean;
    listIsPristine: boolean;

    constructor(
        private questionListService: QuestionListService,
        private alertService: AlertService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private modalService: BsModalService) {
        this.questionLists$ = this.questionListService.getAll();

        this.activatedRoute.data
            .map((data: { questionList: QuestionListModel }) => data.questionList)
            .subscribe(list => this.selectedList = _.cloneDeep(list));
            //.publishReplay(1).refCount();
    }

    ngOnInit(): void {

    }

    selectList(id: Guid) {
        this.router.navigate(['/questionlists', id])
    }

    new() {
        this.router.navigate(['/questionlists', 'new']);
    }

    save(questionList: QuestionListModel, navigateToNewList: boolean): Promise<boolean> {
        /*if (!this.form.valid) {
            this.alertService.warning('Er zitten nog fouten in de woordenlijst', null);
            return Promise.resolve(false);
        }*/

        if (questionList.id) {
            // update
            return this.questionListService.update(questionList)
                .then(
                    __ => {
                        this.alertService.success(`Woordenlijst ${questionList.title} opgeslagen`);
                        this.selectedList = _.cloneDeep(questionList);
                        return true; },
                    err => {
                        this.alertService.fail('Fout bij het opslaan van de woordenlijst', err);
                        return false;
                    }
                );
        } else {
            return this.questionListService.create(questionList)
                .then(
                    id => {
                        this.alertService.success(`Woordenlijst ${questionList.title} opgeslagen`);
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

    delete(questionList: QuestionListModel) {
        this.questionListService.delete(questionList.id)
            .then(
                _ => {
                    this.alertService.success(`Woordenlijst ${questionList.title} verwijderd`);
                    this.router.navigate(['/questionlists']);
                },
                err => {
                    this.alertService.fail('Fout bij het verwijderen van de woordenlijst', err);
                    return false;
                }
            );
    }

    canDeactivate() {
        if (!this.selectedList || this.listIsPristine) {
            return true;
        }

        let ref = this.modalService.show(ConfirmSaveQuestionListComponent);
        let event = (<EventEmitter<ResultAction>>(ref.content.selected)).flatMap(x => {
            switch (x.action) {
                case 'continue':
                    return Promise.resolve(true);
                case 'save':
                    if (!this.listIsValid) {
                        this.alertService.warning('Er zitten nog fouten in de woordenlijst', null);
                        return Promise.resolve(false);
                    }

                    return this.save(this.selectedList, false);
                default:
                    return Promise.resolve(false);
            }
        });

        return event;
    }
}
