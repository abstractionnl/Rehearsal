import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AlertModule, ModalModule, ProgressbarModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard.component';
import { FormValidationStyleDirective } from "./shared/directives/form-validation-style.directive";

import {AppRoutingModule} from './app-routing.module';

import {AuthModule} from "./modules/auth/auth.module";
import {NotificationModule} from "./modules/notification/notification.module";
import {QuestionlistModule} from "./modules/questionlist/questionlist.module";
import {RehearsalModule} from "./modules/rehearsal/rehearsal.module";

import {FocusDirective} from "./shared/directives/focus.directive";
import {StoreDevtoolsModule} from "@ngrx/store-devtools";

@NgModule({
    declarations: [
        AppComponent,
        DashboardComponent,
        FormValidationStyleDirective,
        FocusDirective
    ],
    imports: [
        BrowserModule, FormsModule, HttpClientModule,
        AlertModule.forRoot(), ModalModule.forRoot(), ProgressbarModule.forRoot(),
        QuestionlistModule, NotificationModule, RehearsalModule,
        AuthModule, AppRoutingModule,
        StoreDevtoolsModule.instrument({
            maxAge: 25 //  Retains last 25 states
        })
    ],
    providers: [

    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
