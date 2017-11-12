/// <reference path="../types.ts" />

import { Injectable, OnInit } from '@angular/core';
import { Headers } from '@angular/http';
import { AuthHttp } from "angular2-jwt";

import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/switchMap';
import "rxjs/add/operator/catch";
import 'rxjs/add/observable/combinelatest';

import { BehaviorSubject } from "rxjs/BehaviorSubject";
import "rxjs/add/operator/publishReplay";
import "rxjs/add/operator/throttleTime";
import Guid = System.Guid;

import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;

@Injectable()
export class QuestionListService {
    private apiUrl = 'api/questionlist';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: AuthHttp) {
    }

    private fetchAll(): Observable<QuestionListOverviewModel[]> {
        return this.http.get(this.apiUrl)
            .map(response => response.json() as QuestionListOverviewModel[]);
    }

    getAll(): Observable<QuestionListOverviewModel[]> {
        return this.fetchAll();
    }

    get(id: System.Guid): Observable<QuestionListModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get(url)
            .map(response => response.json() as QuestionListModel);
    }

    create(questionList: QuestionListModel): Observable<Guid> {
        return this.http
            .post(this.apiUrl, JSON.stringify(questionList), { headers: this.headers })
            .map(response => response.json() as Guid);
    }

    update(questionList: QuestionListModel): Observable<void> {
        const url = `${this.apiUrl}/${questionList.id}`;
        return this.http
            .put(url, JSON.stringify(questionList), { headers: this.headers })
            .map(_ => null);
    }

    remove(id: System.Guid): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete(url, { headers: this.headers })
            .map(_ => null);
    }
}
