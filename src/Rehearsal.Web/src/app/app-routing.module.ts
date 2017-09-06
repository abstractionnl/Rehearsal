import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard.component';
import { QuestionlistOverviewComponent } from './questionlist-overview.component';
import { QuestionlistDetailComponent } from './questionlist-detail.component';
import { NoQuestionlistSelectedComponent } from './no-questionlist-selected.component';
import { QuestionListResolver } from './questionlist.resolver';
import { QuestionListsResolver } from './questionlists.resolver';
import { CanDeactivateGuard } from './can-deactivate-guard.service';

const routes: Routes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent },
    {
        path: 'questionlists',
        component: QuestionlistOverviewComponent,
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
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [QuestionListResolver, QuestionListsResolver, CanDeactivateGuard ]
})
export class AppRoutingModule { }
