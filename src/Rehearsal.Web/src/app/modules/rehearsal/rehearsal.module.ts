import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";

import {ProgressbarModule} from "ngx-bootstrap";

import {StartRehearsalPage} from "./pages/start-rehearsal/start-rehearsal.page";
import {RehearsalPage} from "./pages/rehearsal/rehearsal.page";

import {RehearsalQuestionComponent} from "./components/rehearsal-question/rehearsal-question.component";
import {RehearsalProgressComponent} from "./components/rehearsal-progress/rehearsal-progress.component";
import {RehearsalService} from "./services/rehearsal.service";
import {RehearsalRoutingModule} from "./rehearsal-routing.module";
import {FocusDirective} from "../../shared/directives/focus.directive";

@NgModule({
    declarations: [
        StartRehearsalPage,
        RehearsalPage,
        RehearsalQuestionComponent,
        RehearsalProgressComponent,
        FocusDirective
    ],
    imports: [
        CommonModule, HttpClientModule, RehearsalRoutingModule, FormsModule,
        ProgressbarModule.forRoot()
    ],
    providers: [
        RehearsalService
    ]
})
export class RehearsalModule { }

