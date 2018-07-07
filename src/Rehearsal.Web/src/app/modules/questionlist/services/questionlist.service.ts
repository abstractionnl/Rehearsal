import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";

import {Observable} from 'rxjs';
import {map} from "rxjs/operators";

import {QuestionList, System} from "../../../types";
import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import QuestionListModel = QuestionList.QuestionListModel;

@Injectable()
export class QuestionListService {
    private apiUrl = 'api/questionlist';
    private headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    constructor(private http: HttpClient) {
    }

    private fetchAll(): Observable<QuestionListOverviewModel[]> {
        return this.http
            .get(this.apiUrl, { headers: this.headers })
            .pipe(
                map(response => response as QuestionListOverviewModel[])
            );
    }

    getAll(): Observable<QuestionListOverviewModel[]> {
        return this.fetchAll();
    }

    get(id: System.Guid): Observable<QuestionListModel> {
        const url = `${this.apiUrl}/${id}`;
        return this.http
            .get(url)
            .pipe(
                map(response => response as QuestionListModel)
            );
    }

    create(questionList: QuestionListModel): Observable<System.Guid> {
        return this.http
            .post(this.apiUrl, JSON.stringify(questionList), { headers: this.headers })
            .pipe(
                map(response => response as System.Guid)
            );
    }

    update(questionList: QuestionListModel): Observable<void> {
        const url = `${this.apiUrl}/${questionList.id}`;
        return this.http
            .put<void>(url, JSON.stringify(questionList), { headers: this.headers });
    }

    remove(id: System.Guid): Observable<void> {
        const url = `${this.apiUrl}/${id}`;
        return this.http
            .delete<void>(url, { headers: this.headers });
    }
}
