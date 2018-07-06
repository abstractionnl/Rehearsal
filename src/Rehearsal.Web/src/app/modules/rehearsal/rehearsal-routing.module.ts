import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";

import {AuthModule} from "../auth/auth.module";
import {AuthGuard} from "../auth/guards/auth-guard.service";

import {StartRehearsalPage} from "./pages/start-rehearsal/start-rehearsal.page";
import {RehearsalPage} from "./pages/rehearsal/rehearsal.page";
import {QuestionlistOverviewGuard} from "../questionlist/guards/questionlist-overview.guard";
import {RehearsalSessionGuard} from "./guards/questionlist.guard";

const routes: Routes = [
    {
        path: 'rehearsal',
        component: StartRehearsalPage,
        canActivate: [ AuthGuard, QuestionlistOverviewGuard ]
    },
    {
        path: 'rehearsal/:id',
        component: RehearsalPage,
        canActivate: [ AuthGuard, RehearsalSessionGuard ]
    }
];

@NgModule({
    imports: [ RouterModule.forChild(routes), AuthModule ],
    exports: [ RouterModule ],
    providers: [ RehearsalSessionGuard ]
})
export class RehearsalRoutingModule { }
