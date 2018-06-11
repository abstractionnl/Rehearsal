import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {DashboardComponent} from './dashboard.component';
import {CanDeactivateGuard } from './shared/guards/can-deactivate-guard.service';
import {AuthModule} from "./modules/auth/auth.module";
import {AuthGuard} from "./modules/auth/guards/auth-guard.service";

const routes: Routes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [ AuthGuard ]
    }
];

@NgModule({
    imports: [ RouterModule.forRoot(routes), AuthModule ],
    exports: [ RouterModule ],
    providers: [ CanDeactivateGuard ]
})
export class AppRoutingModule { }
