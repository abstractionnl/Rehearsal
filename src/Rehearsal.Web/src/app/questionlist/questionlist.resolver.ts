import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve} from "@angular/router";

import {Observable} from "rxjs/Observable";
import "rxjs/add/operator/do";

import {QuestionListService} from "./questionlist.service";

import {AlertService} from "../alert/alert.service";

import QuestionListModel = QuestionList.QuestionListModel;

@Injectable()
export class QuestionListResolver implements Resolve<QuestionListModel> {
    constructor(private questionListService: QuestionListService, private alertService: AlertService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<QuestionListModel> | QuestionListModel {
        if (!route.params.id)
            return null;

        if (route.params.id == 'new') {
            return this.questionListService.new();
        }

        return this.questionListService.get(route.params.id).catch(err => {
            this.alertService.fail('Fout bij het laden van de woordenlijst', err);
            return null;
        });
    }
}
