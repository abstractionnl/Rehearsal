import {Injectable} from "@angular/core";
import {ActivatedRouteSnapshot, Resolve} from "@angular/router";

import {Observable} from "rxjs/Observable";

import {QuestionListService} from "./questionlist.service";
import QuestionList = Rehearsal.QuestionList;

import "rxjs/add/operator/do";
import { AlertService } from "./error/alert.service";

@Injectable()
export class QuestionListResolver implements Resolve<QuestionList> {
  constructor(private questionListService: QuestionListService, private alertService: AlertService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<QuestionList> {
    return this.questionListService.get(route.params.id).catch(err => {
      this.alertService.fail('Fout bij het laden van de woordenlijst', err);
      return null;
    });
  }
}
