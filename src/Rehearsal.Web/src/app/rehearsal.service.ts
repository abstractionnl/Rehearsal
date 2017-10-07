/// <reference path="types.ts" />

import { Injectable, OnInit } from '@angular/core';
import { Headers } from '@angular/http';
import { AuthHttp } from "angular2-jwt";

import { Observable } from 'rxjs/Observable';

import StartRehearsalRequest = Rehearsal.StartRehearsalRequest;
import Guid = System.Guid;

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
}
