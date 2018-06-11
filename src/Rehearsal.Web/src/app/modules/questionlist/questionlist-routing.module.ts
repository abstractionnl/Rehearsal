import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AuthGuard} from "../auth/guards/auth-guard.service";
import {AuthModule} from "../auth/auth.module";

import {QuestionlistEditorPage} from "./pages/questionlist-editor/questionlist-editor.page";

import {QuestionListGuard} from "./guards/questionlist.guard";
import {QuestionlistOverviewGuard} from "./guards/questionlist-overview.guard";
import {QuestionListDeactivateGuard} from "./guards/questionlist.deactivate.guard";

const routes: Routes = [
    {
        path: 'questionlists',
        component: QuestionlistEditorPage,
        canActivate: [
            AuthGuard,
            QuestionlistOverviewGuard
        ],
    },
    {
        path: 'questionlists/:id',
        component: QuestionlistEditorPage,
        canActivate: [
            AuthGuard,
            QuestionlistOverviewGuard,
            QuestionListGuard
        ],
        canDeactivate: [ QuestionListDeactivateGuard ]
        /*children: [
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
        ]*/
    }
];

@NgModule({
    imports: [ RouterModule.forChild(routes), AuthModule ],
    exports: [ RouterModule ],
    providers: [ QuestionlistOverviewGuard, QuestionListGuard, QuestionListDeactivateGuard ]
})
export class QuestionlistRoutingModule { }
