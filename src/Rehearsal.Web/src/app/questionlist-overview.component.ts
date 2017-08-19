/// <reference path="types.ts" />

import {Component, OnDestroy, OnInit} from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { QuestionListService } from './questionlist.service';
import { ActivatedRoute } from "@angular/router";
import Guid = System.Guid;

@Component({
    templateUrl: './questionlist-overview.component.html'
})
export class QuestionlistOverviewComponent implements OnInit  {
    questionLists: Observable<Rehearsal.QuestionList[]>;
    selectedQuestionList: Observable<Guid>;

    constructor(private questionListService: QuestionListService, private route: ActivatedRoute) {

    }

    ngOnInit(): void {
        this.questionLists = this.questionListService.getAll();
        //this.route.firstChild.paramMap.subscribe(p => console.log(p.get('id')));
        this.selectedQuestionList = this.route.firstChild.paramMap.map(x => x.get('id'));
    }

}
