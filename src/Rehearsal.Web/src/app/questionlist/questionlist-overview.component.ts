/// <reference path="../types.ts" />

import {Component, OnInit} from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { QuestionListService } from './questionlist.service';
import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;

@Component({
    templateUrl: './questionlist-overview.component.html'
})
export class QuestionlistOverviewComponent implements OnInit  {
    questionLists: Observable<QuestionListOverviewModel[]>;

    constructor(private questionListService: QuestionListService) {

    }

    ngOnInit(): void {
        this.questionLists = this.questionListService.getAll();
    }
}
