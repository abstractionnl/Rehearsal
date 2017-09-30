import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { QuestionlistOverviewComponent } from './questionlist-overview.component';
import { QuestionlistDetailComponent } from './questionlist-detail.component';
import { NoQuestionlistSelectedComponent } from './no-questionlist-selected.component';
import { QuestionListResolver } from './questionlist.resolver';
import { QuestionListsResolver } from './questionlists.resolver';
import { CanDeactivateGuard } from '../can-deactivate-guard.service';
import { AuthGuard } from "../auth/auth-guard.service";
import { AuthModule } from "../auth/auth.module";

const routes: Routes = [
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
    imports: [ RouterModule.forChild(routes), AuthModule ],
    exports: [ RouterModule ],
    providers: [ QuestionListResolver, QuestionListsResolver ]
})
export class QuestionlistRoutingModule { }
