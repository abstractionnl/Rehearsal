import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { QuestionListResolver } from './questionlist.resolver';
import { QuestionListsResolver } from './questionlists.resolver';
import { CanDeactivateGuard } from '../can-deactivate-guard.service';
import { AuthGuard } from "../auth/auth-guard.service";
import { AuthModule } from "../auth/auth.module";
import {QuestionlistEditorComponent} from "./questionlist-editor.component";

const routes: Routes = [
    {
        path: 'questionlists',
        component: QuestionlistEditorComponent,
        canActivate: [ AuthGuard ],
        resolve: {
            questionLists: QuestionListsResolver,
            questionList: QuestionListResolver
        },
    },
    {
        path: 'questionlists/:id',
        component: QuestionlistEditorComponent,
        canActivate: [ AuthGuard ],
        resolve: {
            questionLists: QuestionListsResolver,
            questionList: QuestionListResolver
        },
        canDeactivate: [ CanDeactivateGuard ]
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
    providers: [ QuestionListResolver, QuestionListsResolver ]
})
export class QuestionlistRoutingModule { }
