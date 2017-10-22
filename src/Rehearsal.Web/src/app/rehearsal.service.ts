/// <reference path="types.ts" />

import { Injectable, OnInit } from '@angular/core';
import { Headers } from '@angular/http';
import { AuthHttp } from "angular2-jwt";

import { Observable } from 'rxjs/Observable';

import StartRehearsalRequest = Rehearsal.StartRehearsalRequest;
import Guid = System.Guid;
import RehearsalSessionModel = Rehearsal.RehearsalSessionModel;
import AnswerResultModel = Rehearsal.AnswerResultModel;
import GiveAnswerRequest = Rehearsal.GiveAnswerRequest;

@Injectable()
export class RehearsalService {
    private apiUrl = 'api/rehearsal';

    private headers = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: AuthHttp) {

    }

    public start(request: StartRehearsalRequest): Promise<Guid> {
        return this.http
            .post(this.apiUrl, JSON.stringify(request), { headers: this.headers })
            .map(response => response.json() as Guid)
            .toPromise();
    }

    public get(id: Guid): Observable<RehearsalSessionModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http
            .get(url)
            .map(response => response.json() as RehearsalSessionModel);
    }

    public giveAnswer(rehearsalId: Guid, questionId: Guid, answer: string): Observable<AnswerResultModel> {
        const url = `${this.apiUrl}/${rehearsalId}`;
        const model: GiveAnswerRequest = {
            questionId: questionId,
            answer: answer
        };

        return this.http
            .put(url, JSON.stringify(model), { headers: this.headers })
            .map(response => response.json() as AnswerResultModel);
    }
}
