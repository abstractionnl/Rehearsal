import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve} from "@angular/router";

import {Observable} from "rxjs/Observable";
import "rxjs/add/operator/take";

import {QuestionListService} from "./questionlist.service";

import { AlertService } from "../alert/alert.service";

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;

@Injectable()
export class QuestionListsResolver implements Resolve<QuestionListOverviewModel[]> {
    constructor(private questionListService: QuestionListService, private alertService: AlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<QuestionListOverviewModel[]> {
        return this.questionListService.getAll().catch(err => {
            this.alertService.fail('Fout bij het laden van de woordenlijsten', err);
            return null;
        }).take(1);
    }
}
