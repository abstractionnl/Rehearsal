import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve} from "@angular/router";

import {Observable} from "rxjs/Observable";

import {QuestionListService} from "./questionlist.service";
import QuestionList = Rehearsal.QuestionList;

import { AlertService } from "../alert/alert.service";
import QuestionListOverview = Rehearsal.QuestionListOverview;
import "rxjs/add/operator/take";

@Injectable()
export class QuestionListsResolver implements Resolve<QuestionListOverview[]> {
    constructor(private questionListService: QuestionListService, private alertService: AlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<QuestionListOverview[]> {
        return this.questionListService.getAll().catch(err => {
            this.alertService.fail('Fout bij het laden van de woordenlijsten', err);
            return null;
        }).take(1);
    }
}
