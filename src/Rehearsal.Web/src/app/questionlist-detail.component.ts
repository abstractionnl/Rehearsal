/// <reference path="types.ts" />

import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { QuestionListService } from './questionlist.service';
import {ActivatedRoute, Router} from "@angular/router";
import {AlertService} from "./error/alert.service";

@Component({
    templateUrl: './questionlist-detail.component.html'
})
export class QuestionlistDetailComponent implements OnInit {
    private questionList: Rehearsal.QuestionList;

    constructor(
        private questionListService: QuestionListService,
        private alertService: AlertService,
        private route: ActivatedRoute
    ) {

    }

    ngOnInit() {
        this.route.data
            .subscribe((data: { questionList: Rehearsal.QuestionList }) => {
                this.questionList = data.questionList;
            });
    }

    save() {
        this.questionListService.update(this.questionList)
            .then(
                x => this.questionList = x,
                err => {
                    this.alertService.fail('Fout bij het opslaan van de woordenlijst', err);
                }
            );
    }
}
