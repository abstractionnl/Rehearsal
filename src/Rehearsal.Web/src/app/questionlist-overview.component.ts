/// <reference path="types.ts" />

import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { QuestionListService } from './questionlist.service';

@Component({
    templateUrl: './questionlist-overview.component.html'
})
export class QuestionlistOverviewComponent implements OnInit {
  questionLists: Observable<Rehearsal.QuestionList[]>;

  constructor(private questionListService: QuestionListService) {

  }

  ngOnInit(): void {
    this.questionLists = this.questionListService.getAll()
  }

}
