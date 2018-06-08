/// <reference path="types.ts" />

import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";

import { Observable } from 'rxjs';

import StartRehearsalRequest = Rehearsal.StartRehearsalRequest;
import Guid = System.Guid;
import RehearsalSessionModel = Rehearsal.RehearsalSessionModel;
import AnswerResultModel = Rehearsal.AnswerResultModel;
import GiveAnswerRequest = Rehearsal.GiveAnswerRequest;

import {map} from "rxjs/operators";

@Injectable()
export class RehearsalService {
    private apiUrl = 'api/rehearsal';

    private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    constructor(private http: HttpClient) {

    }

    public start(request: StartRehearsalRequest): Promise<Guid> {
        return this.http
            .post(this.apiUrl, JSON.stringify(request), { headers: this.headers })
            .pipe(
                map(response => response as Guid)
            )
            .toPromise();
    }

    public get(id: Guid): Observable<RehearsalSessionModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http
            .get(url)
            .pipe(
                map(response => response as RehearsalSessionModel)
            );
    }

    public giveAnswer(rehearsalId: Guid, questionId: Guid, answer: string): Observable<AnswerResultModel> {
        const url = `${this.apiUrl}/${rehearsalId}`;
        const model: GiveAnswerRequest = {
            questionId: questionId,
            answer: answer
        };

        return this.http
            .put(url, JSON.stringify(model), { headers: this.headers })
            .pipe(
                map(response => response as AnswerResultModel)
            );
    }
}
