import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { Observable } from "rxjs";

import {QuestionListService} from "../../../questionlist/services/questionlist.service";
import {RehearsalService} from "../../services/rehearsal.service";

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;

@Component({
    templateUrl: './start-rehearsal.page.html'
})
export class StartRehearsalPage implements OnInit {
    questionLists: Observable<QuestionListOverviewModel[]>;
    questionList: QuestionListModel;

    constructor(
        private questionListService: QuestionListService,
        private rehearsalService : RehearsalService,
        private router: Router) {

    }

    ngOnInit(): void {
        this.questionLists = this.questionListService.getAll();         // TODO: Remove dependency to questionlist service
    }

    start(): void {
        this.rehearsalService.start({
            questionListId: this.questionList.id
        }).then(id => {
            this.router.navigate(['/rehearsal', id]);
        });
    }

    canStart(): boolean {
        return this.questionList != null;
    }
}
