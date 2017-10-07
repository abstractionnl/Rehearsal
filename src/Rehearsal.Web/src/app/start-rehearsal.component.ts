import { Component, OnInit } from '@angular/core';
import { QuestionListService } from "./questionlist/questionlist.service";
import { Observable } from "rxjs/Observable";

import QuestionListOverview = Rehearsal.QuestionListOverview;
import QuestionList = Rehearsal.QuestionList;
import {RehearsalService} from "./rehearsal.service";

@Component({
    templateUrl: './start-rehearsal.component.html'
})
export class StartRehearsalComponent implements OnInit {
    questionLists: Observable<QuestionListOverview[]>;
    questionList: QuestionList;

    constructor(private questionListService: QuestionListService, private rehearsalService : RehearsalService) {

    }

    ngOnInit(): void {
        this.questionLists = this.questionListService.getAll();
    }

    start(): void {
        this.rehearsalService.start({
            questionListId: this.questionList.id
        });
    }

    canStart(): boolean {
        return this.questionList != null;
    }
}
