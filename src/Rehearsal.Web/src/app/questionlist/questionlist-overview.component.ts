/// <reference path="../types.ts" />

import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { QuestionListService } from './questionlist.service';
import QuestionListOverviewModel = QuestionList.QuestionListOverviewModel;
import Guid = System.Guid;

@Component({
    templateUrl: './questionlist-overview.component.html',
    selector: 'questionlist-overview'
})
export class QuestionlistOverviewComponent  {
    @Input() questionLists: QuestionListOverviewModel[];
    @Input() selectedList: Guid;
    @Output() onSelect: EventEmitter<Guid> = new EventEmitter<Guid>();

    select(id: Guid) {
        this.onSelect.emit(id);
    }
}
