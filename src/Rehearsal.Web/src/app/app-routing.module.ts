﻿import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard.component';
import { CanDeactivateGuard } from './can-deactivate-guard.service';
import { AuthModule } from "./auth/auth.module";
import { AuthGuard } from "./auth/auth-guard.service";
import { LoginComponent } from "./auth/login.component";
import { StartRehearsalComponent } from "./start-rehearsal.component";
import { RehearsalComponent } from "./rehearsal.component";

const routes: Routes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'rehearsal',
        component: StartRehearsalComponent,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'rehearsal/:id',
        component: RehearsalComponent,
        canActivate: [ AuthGuard ]
    }
];

@NgModule({
    imports: [ RouterModule.forRoot(routes), AuthModule ],
    exports: [ RouterModule ],
    providers: [ CanDeactivateGuard ]
})
export class AppRoutingModule { }
