import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AlertModule, ModalModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard.component';
import { QuestionlistOverviewComponent } from './questionlist-overview.component';
import { QuestionlistDetailComponent } from './questionlist-detail.component';
import { NoQuestionlistSelectedComponent } from './no-questionlist-selected.component';
import { ConfirmSaveQuestionListComponent as ConfirmSaveQuestionComponent } from "./confirm-save-question.component";
import { FormValidationStyleDirective } from "./form-validation-style.directive";

import { QuestionListService } from './questionlist.service';
import { AlertService } from './error/alert.service';

import { AppRoutingModule } from './app-routing.module';
import { AlertComponent } from './error/alert.component';

@NgModule({
    declarations: [
        AppComponent,
        AlertComponent,
        DashboardComponent,
        QuestionlistOverviewComponent,
        QuestionlistDetailComponent,
        NoQuestionlistSelectedComponent,
        ConfirmSaveQuestionComponent,
        FormValidationStyleDirective
    ],
    imports: [
        BrowserModule, FormsModule, HttpModule,
        AlertModule.forRoot(), ModalModule.forRoot(),
        AppRoutingModule
    ],
    entryComponents: [ ConfirmSaveQuestionComponent ],
    providers: [
        QuestionListService,
        AlertService
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
