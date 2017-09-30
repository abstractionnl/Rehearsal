import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AlertModule, ModalModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard.component';
import { FormValidationStyleDirective } from "./form-validation-style.directive";

import { AlertService } from './alert/alert.service';

import { AppRoutingModule } from './app-routing.module';
import { AlertComponent } from './alert/alert.component';
import { QuestionlistModule } from "./questionlist/questionlist.module";
import { AuthModule } from "./auth/auth.module";

@NgModule({
    declarations: [
        AppComponent,
        AlertComponent,
        DashboardComponent,
        FormValidationStyleDirective
    ],
    imports: [
        BrowserModule, FormsModule, HttpModule,
        AlertModule.forRoot(), ModalModule.forRoot(),
        QuestionlistModule,
        AuthModule, AppRoutingModule
    ],
    providers: [
        AlertService
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
