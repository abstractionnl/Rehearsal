import {NgModule} from '@angular/core';
import {CommonModule} from "@angular/common";
import {ReactiveFormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {RouterModule} from "@angular/router";

import {StoreModule} from "@ngrx/store";

import {BsDropdownModule, ModalModule} from 'ngx-bootstrap';

import {QuestionlistOverviewComponent} from './questionlist-overview.component';
import {QuestionlistFormComponent} from './questionlist-form.component';
import {NoQuestionlistSelectedComponent} from './no-questionlist-selected.component';
import {ConfirmSaveQuestionListComponent} from "./confirm-save-question.component";

import {QuestionlistEditorComponent} from "./questionlist-editor.component";
import {QuestionListService} from './questionlist.service';
import {QuestionlistRoutingModule} from "./questionlist-routing.module";
import {questionListReducer} from "./store/questionlist.reducer";
import {EffectsModule} from "@ngrx/effects";
import {QuestionlistEffects} from "./store/questionlist.effects";
import {QuestionlistButtonsComponent} from "./questionlist-buttons.component";

@NgModule({
    declarations: [
        QuestionlistEditorComponent,
        QuestionlistOverviewComponent,
        QuestionlistFormComponent,
        NoQuestionlistSelectedComponent,
        ConfirmSaveQuestionListComponent,
        QuestionlistButtonsComponent
    ],
    imports: [
        CommonModule, ReactiveFormsModule, HttpModule, RouterModule,
        ModalModule.forRoot(), QuestionlistRoutingModule,
        StoreModule.forRoot({
            questionListEditor: questionListReducer
        }),
        BsDropdownModule.forRoot(),
        EffectsModule.forRoot([QuestionlistEffects])
    ],
    entryComponents: [ ConfirmSaveQuestionListComponent ],
    providers: [
        QuestionListService
    ]
})
export class QuestionlistModule { }
