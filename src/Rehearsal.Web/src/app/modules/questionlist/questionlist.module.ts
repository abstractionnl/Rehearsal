import {NgModule} from '@angular/core';
import {CommonModule} from "@angular/common";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule} from "@angular/router";

import {StoreModule} from "@ngrx/store";
import {EffectsModule} from "@ngrx/effects";

import {BsDropdownModule, ModalModule} from 'ngx-bootstrap';

import {QuestionlistRoutingModule} from "./questionlist-routing.module";

import {QuestionListService} from './services/questionlist.service';

import {questionListReducer} from "./store/questionlist.reducer";
import {QuestionlistEffects} from "./store/questionlist.effects";

import {QuestionlistEditorPage} from "./pages/questionlist-editor/questionlist-editor.page";

import {QuestionlistOverviewComponent} from './components/questionlist-overview/questionlist-overview.component';
import {QuestionlistFormComponent} from './components/questionlist-form/questionlist-form.component';
import {NoQuestionlistSelectedComponent} from './components/no-questionlist-selected/no-questionlist-selected.component';
import {ConfirmSaveQuestionlistComponent} from "./components/confirm-save-questionlist/confirm-save-questionlist.component";
import {QuestionlistButtonsComponent} from "./components/questionlist-buttons/questionlist-buttons.component";
import {ConfirmCopyQuestionlistComponent} from "./components/confirm-copy-questionlist/confirm-copy-questionlist.component";
import {ConfirmRemoveQuestionlistComponent} from "./components/confirm-remove-questionlist/confirm-remove-questionlist.component";

@NgModule({
    declarations: [
        QuestionlistEditorPage,
        QuestionlistOverviewComponent,
        QuestionlistFormComponent,
        NoQuestionlistSelectedComponent,
        ConfirmSaveQuestionlistComponent,
        ConfirmCopyQuestionlistComponent,
        ConfirmRemoveQuestionlistComponent,
        QuestionlistButtonsComponent
    ],
    imports: [
        CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule, RouterModule,
        ModalModule.forRoot(), QuestionlistRoutingModule,
        StoreModule.forFeature('questionlist', {
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
