import {NgModule} from '@angular/core';
import {CommonModule} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpModule} from '@angular/http';
import {RouterModule} from "@angular/router";

import {StoreModule} from "@ngrx/store";

import {BsDropdownModule, ModalModule} from 'ngx-bootstrap';

import {QuestionlistOverviewComponent} from './questionlist-overview.component';
import {QuestionlistFormComponent} from './questionlist-form.component';
import {NoQuestionlistSelectedComponent} from './no-questionlist-selected.component';
import {ConfirmSaveQuestionlistComponent} from "./confirm-save-questionlist.component";

import {QuestionlistEditorComponent} from "./questionlist-editor.component";
import {QuestionListService} from './questionlist.service';
import {QuestionlistRoutingModule} from "./questionlist-routing.module";
import {questionListReducer} from "./store/questionlist.reducer";
import {EffectsModule} from "@ngrx/effects";
import {QuestionlistEffects} from "./store/questionlist.effects";
import {QuestionlistButtonsComponent} from "./questionlist-buttons.component";
import {ConfirmCopyQuestionlistComponent} from "./confirm-copy-questionlist.component";
import {ConfirmRemoveQuestionlistComponent} from "./confirm-remove-questionlist.component";

@NgModule({
    declarations: [
        QuestionlistEditorComponent,
        QuestionlistOverviewComponent,
        QuestionlistFormComponent,
        NoQuestionlistSelectedComponent,
        ConfirmSaveQuestionlistComponent,
        ConfirmCopyQuestionlistComponent,
        ConfirmRemoveQuestionlistComponent,
        QuestionlistButtonsComponent
    ],
    imports: [
        CommonModule, FormsModule, ReactiveFormsModule, HttpModule, RouterModule,
        ModalModule.forRoot(), QuestionlistRoutingModule,
        StoreModule.forRoot({
            questionListEditor: questionListReducer
        }),
        BsDropdownModule.forRoot(),
        EffectsModule.forRoot([QuestionlistEffects])
    ],
    entryComponents: [ ConfirmSaveQuestionlistComponent, ConfirmCopyQuestionlistComponent, ConfirmRemoveQuestionlistComponent ],
    providers: [
        QuestionListService
    ]
})
export class QuestionlistModule { }
