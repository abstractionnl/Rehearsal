import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';

import {AuthGuard} from "../auth/auth-guard.service";
import {AuthModule} from "../auth/auth.module";
import {QuestionlistEditorComponent} from "./questionlist-editor.component";
import {QuestionListGuard} from "./questionlist.guard";
import {QuestionlistOverviewGuard} from "./questionlist-overview.guard";
import {QuestionListDeactivateGuard} from "./questionlist.deactivate.guard";

const routes: Routes = [
    {
        path: 'questionlists',
        component: QuestionlistEditorComponent,
        canActivate: [
            AuthGuard,
            QuestionlistOverviewGuard
        ],
    },
    {
        path: 'questionlists/:id',
        component: QuestionlistEditorComponent,
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
