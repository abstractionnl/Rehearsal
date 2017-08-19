﻿/// <reference path="types.ts" />

import {Injectable, OnInit} from '@angular/core';
import { Headers, Http } from '@angular/http';

import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/switchMap';
import "rxjs/add/operator/catch";
import 'rxjs/add/observable/combinelatest';

import { BehaviorSubject } from "rxjs/BehaviorSubject";
import "rxjs/add/operator/publish";

@Injectable()
export class QuestionListService {

    private apiUrl = 'api/questionlist';
    private headers = new Headers({ 'Content-Type': 'application/json' });

    private fetchTrigger = new BehaviorSubject(null);
    private all: Observable<Rehearsal.QuestionList[]>;

    constructor(private http: Http) {
        this.all = this.fetchTrigger
            .switchMap(() => this.fetchAll())
            .publish().refCount();
    }

    private fetchAll(): Observable<Rehearsal.QuestionList[]> {
        return this.http.get(this.apiUrl)
            .map(response => response.json() as Rehearsal.QuestionList[]);
    }

    getAll(): Observable<Rehearsal.QuestionList[]> {
        this.fetchTrigger.next(null);
        return this.all;
    }

    get(id: System.Guid): Observable<Rehearsal.QuestionList> {
        const url = `${this.apiUrl}/${id}`;
        return this.http.get(url)
            .map(response => response.json() as Rehearsal.QuestionList);
    }

    create(questionList: Rehearsal.QuestionList): Promise<Rehearsal.QuestionList> {
        return this.http
            .post(this.apiUrl, JSON.stringify(questionList), { headers: this.headers })
            .toPromise()
            .then(response => response.json() as Rehearsal.QuestionList)
            .catch(this.handleError);
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
            .then(() => null)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}