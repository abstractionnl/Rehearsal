import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard.component';
import { QuestionlistOverviewComponent } from './questionlist-overview.component';
import { QuestionlistDetailComponent } from './questionlist-detail.component';
import { NoQuestionlistSelectedComponent } from './no-questionlist-selected.component';
import { QuestionListResolver } from './questionlist.resolver';
import { QuestionListsResolver } from './questionlists.resolver';
import { CanDeactivateGuard } from './can-deactivate-guard.service';
import { AuthModule } from "./auth/auth.module";
import { AuthGuard } from "./auth/auth-guard.service";
import { LoginComponent } from "./login.component";

const routes: Routes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [ AuthGuard ]
    },
    {
        path: 'questionlists',
        component: QuestionlistOverviewComponent,
        canActivate: [ AuthGuard ],
        resolve: {
            questionLists: QuestionListsResolver
        },
        children: [
            {
                path: ':id',
                component: QuestionlistDetailComponent,
                resolve: {
                  questionList: QuestionListResolver
                },
                canDeactivate: [ CanDeactivateGuard ]
            },
            {
                path: '',
                component: NoQuestionlistSelectedComponent
            }
        ]
    }
];

@NgModule({
    imports: [ RouterModule.forRoot(routes), AuthModule ],
    exports: [ RouterModule ],
    providers: [ QuestionListResolver, QuestionListsResolver, CanDeactivateGuard ]
})
export class AppRoutingModule { }
