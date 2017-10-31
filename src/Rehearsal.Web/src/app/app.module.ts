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
import { StartRehearsalComponent } from "./start-rehearsal.component";
import { RehearsalService } from "./rehearsal.service";
import { RehearsalComponent } from "./rehearsal.component";
import { RehearsalQuestionComponent } from "./rehearsal-question.component";
import {FocusDirective} from "./focus.directive";
import {StoreDevtoolsModule} from "@ngrx/store-devtools";
import {EffectsModule} from "@ngrx/effects";
import {AlertEffects} from "./alert/store/alert.effects";

@NgModule({
    declarations: [
        AppComponent,
        AlertComponent,
        DashboardComponent,
        FormValidationStyleDirective,
        StartRehearsalComponent,
        RehearsalComponent,
        RehearsalQuestionComponent,
        FocusDirective
    ],
    imports: [
        BrowserModule, FormsModule, HttpModule,
        AlertModule.forRoot(), ModalModule.forRoot(),
        QuestionlistModule,
        AuthModule, AppRoutingModule,
        EffectsModule.forFeature([AlertEffects]),
        StoreDevtoolsModule.instrument({
            maxAge: 25 //  Retains last 25 states
        })
    ],
    providers: [
        AlertService, RehearsalService
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
