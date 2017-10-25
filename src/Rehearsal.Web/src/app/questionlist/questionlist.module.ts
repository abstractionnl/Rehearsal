import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from "@angular/router";

import { ModalModule } from 'ngx-bootstrap';

import { QuestionlistOverviewComponent } from './questionlist-overview.component';
import {QuestionlistFormComponent} from './questionlist-form.component';
import { NoQuestionlistSelectedComponent } from './no-questionlist-selected.component';
import { ConfirmSaveQuestionListComponent as ConfirmSaveQuestionComponent } from "./confirm-save-question.component";

import {QuestionlistEditorComponent} from "./questionlist-editor.component";
import {QuestionListService} from './questionlist.service';
import {QuestionlistRoutingModule} from "./questionlist-routing.module";

@NgModule({
    declarations: [
        QuestionlistEditorComponent,
        QuestionlistOverviewComponent,
        QuestionlistFormComponent,
        NoQuestionlistSelectedComponent,
        ConfirmSaveQuestionComponent
    ],
    imports: [
        CommonModule, FormsModule, HttpModule, RouterModule,
        ModalModule.forRoot(), QuestionlistRoutingModule
    ],
    entryComponents: [ ConfirmSaveQuestionComponent ],
    providers: [
        QuestionListService
    ]
})
export class QuestionlistModule { }
