import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";

import {AuthModule} from "../auth/auth.module";

import {CanDeactivateGuard} from "../../shared/guards/can-deactivate-guard.service";
import {AuthGuard} from "../auth/guards/auth-guard.service";

import {StartRehearsalPage} from "./pages/start-rehearsal/start-rehearsal.page";
import {RehearsalPage} from "./pages/rehearsal/rehearsal.page";

const routes: Routes = [
    {
        path: 'rehearsal',
        component: StartRehearsalPage,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'rehearsal/:id',
        component: RehearsalPage,
        canActivate: [ AuthGuard ]
    }
];

@NgModule({
    imports: [ RouterModule.forChild(routes), AuthModule ],
    exports: [ RouterModule ],
    providers: [ CanDeactivateGuard ]
})
export class RehearsalRoutingModule { }
