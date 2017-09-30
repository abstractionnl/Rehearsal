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
import "rxjs/add/operator/publish";
import "rxjs/add/operator/throttleTime";
import Guid = System.Guid;

@Injectable()
export class QuestionListService {

    private apiUrl = 'api/questionlist';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    private fetchTrigger = new BehaviorSubject(null);
    private all: Observable<Rehearsal.QuestionListOverview[]>;

    constructor(private http: AuthHttp) {
        this.all = this.fetchTrigger
            .throttleTime(500)
            .switchMap(() => this.fetchAll())
            .publish().refCount();
    }

    private fetchAll(): Observable<Rehearsal.QuestionListOverview[]> {
        return this.http.get(this.apiUrl)
            .map(response => response.json() as Rehearsal.QuestionListOverview[]);
    }

    getAll(): Observable<Rehearsal.QuestionListOverview[]> {
        this.fetchTrigger.next(null);
        return this.all;
    }

    get(id: System.Guid): Observable<Rehearsal.QuestionList> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get(url)
            .map(response => response.json() as Rehearsal.QuestionList);
    }

    create(questionList: Rehearsal.QuestionList): Promise<Guid> {
        return this.http
            .post(this.apiUrl, JSON.stringify(questionList), { headers: this.headers })
            .toPromise()
            .then(response => {
                this.fetchTrigger.next(null);
                return response.json() as Guid;
            });
    }

    update(questionList: Rehearsal.QuestionList): Promise<void> {
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

    new(): Rehearsal.QuestionList {
        return {
            id: null,
            title: 'Nieuwe lijst',
            questionTitle: '',
            answerTitle: '',
            questions: [ {
                question: '',
                answer: ''
            }]
        }
    }
}
