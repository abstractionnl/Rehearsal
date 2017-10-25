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

    private fetchTrigger = new BehaviorSubject(null);
    private all: Observable<QuestionListOverviewModel[]>;
    private hasLoaded: boolean;

    constructor(private http: AuthHttp) {
        this.all = this.fetchTrigger
            .throttleTime(500)
            .do(() => this.hasLoaded = true)
            .switchMap(() => this.fetchAll())
            .publishReplay(1).refCount();
    }

    private fetchAll(): Observable<QuestionListOverviewModel[]> {
        return this.http.get(this.apiUrl)
            .map(response => response.json() as QuestionListOverviewModel[]);
    }

    getAll(): Observable<QuestionListOverviewModel[]> {
        if (!this.hasLoaded)
            this.fetchTrigger.next(null);

        return this.all;
    }

    get(id: System.Guid): Observable<QuestionListModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get(url)
            .map(response => response.json() as QuestionListModel);
    }

    create(questionList: QuestionListModel): Promise<Guid> {
        return this.http
            .post(this.apiUrl, JSON.stringify(questionList), { headers: this.headers })
            .toPromise()
            .then(response => {
                this.fetchTrigger.next(null);
                return response.json() as Guid;
            });
    }

    update(questionList: QuestionListModel): Promise<void> {
        const url = `${this.apiUrl}/${questionList.id}`;
        return this.http
            .put(url, JSON.stringify(questionList), { headers: this.headers })
            .toPromise()
            .then(() => this.fetchTrigger.next(null));
    }

    delete(id: System.Guid): Promise<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.delete(url, { headers: this.headers })
            .toPromise()
            .then(() => this.fetchTrigger.next(null));
    }

    new(): QuestionListModel {
        return {
            id: null,
            title: 'Nieuwe lijst',
            questionTitle: '',
            answerTitle: '',
            questions: [ {
                question: '',
                answer: ''
            }],
            version: 0
        }
    }
}
