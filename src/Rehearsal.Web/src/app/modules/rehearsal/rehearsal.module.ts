import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {HttpClientModule} from "@angular/common/http";
import {FormsModule} from "@angular/forms";

import {ProgressbarModule} from "ngx-bootstrap";

import {StartRehearsalPage} from "./pages/start-rehearsal/start-rehearsal.page";
import {RehearsalPage} from "./pages/rehearsal/rehearsal.page";

import {RehearsalProgressComponent} from "./components/rehearsal-progress/rehearsal-progress.component";
import {RehearsalService} from "./services/rehearsal.service";
import {RehearsalRoutingModule} from "./rehearsal-routing.module";
import {FocusDirective} from "../../shared/directives/focus.directive";
import {EffectsModule} from "@ngrx/effects";
import {RehearsalEffects} from "./store/rehearsal.effects";
import {StoreModule} from "@ngrx/store";
import {rehearsalReducer} from "./store/rehearsal.reducer";
import {OpenRehearsalQuestionComponent} from "./components/open-rehearsal-question/open-rehearsal-question.component";
import {MultipleChoiceQuestionComponent} from "./components/multiplechoice-question/multiplechoice-question.component";
import {RehearsalStatisticsComponent} from "./components/rehearsal-statistics/rehearsal-statistics.component";

@NgModule({
    declarations: [
        StartRehearsalPage,
        RehearsalPage,
        OpenRehearsalQuestionComponent,
        MultipleChoiceQuestionComponent,
        RehearsalProgressComponent,
        RehearsalStatisticsComponent,
        FocusDirective
    ],
    imports: [
        CommonModule, HttpClientModule, RehearsalRoutingModule, FormsModule,
        ProgressbarModule.forRoot(),
        StoreModule.forFeature('rehearsal', rehearsalReducer),
        EffectsModule.forFeature([RehearsalEffects])
    ],
    providers: [
        RehearsalService
    ]
})
export class RehearsalModule { }

