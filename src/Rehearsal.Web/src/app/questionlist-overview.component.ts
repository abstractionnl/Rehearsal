/// <reference path="types.ts" />

import {Component, OnDestroy, OnInit} from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { QuestionListService } from './questionlist.service';
import Guid = System.Guid;

@Component({
    templateUrl: './questionlist-overview.component.html'
})
export class QuestionlistOverviewComponent implements OnInit  {
    questionLists: Observable<Rehearsal.QuestionListOverview[]>;

    constructor(private questionListService: QuestionListService) {

    }

    ngOnInit(): void {
        this.questionLists = this.questionListService.getAll();
    }

}
