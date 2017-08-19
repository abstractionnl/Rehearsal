import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AlertModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard.component';
import { QuestionlistOverviewComponent } from './questionlist-overview.component';
import { QuestionlistDetailComponent } from './questionlist-detail.component';
import { QuestionlistFormComponent } from './questionlist-form.component';
import { NoQuestionlistSelectedComponent } from './no-questionlist-selected.component';

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
      QuestionlistFormComponent,
      NoQuestionlistSelectedComponent
  ],
  imports: [
      BrowserModule, FormsModule, HttpModule,
      AlertModule.forRoot(),
      AppRoutingModule
  ],
  providers: [
    QuestionListService,
    AlertService
  ],
  bootstrap: [
      AppComponent
  ]
})
export class AppModule { }
