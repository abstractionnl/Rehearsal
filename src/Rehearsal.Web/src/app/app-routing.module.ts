import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard.component';
import { QuestionlistOverviewComponent } from './questionlist-overview.component';
import { QuestionlistDetailComponent } from './questionlist-detail.component';
import { QuestionListResolver } from './questionlist.resolver';
//import { HeroDetailComponent } from './hero-detail.component';

const routes: Routes = [
    { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent },
    //{ path: 'detail/:id', component: HeroDetailComponent },
    {
        path: 'questionlists',
        component: QuestionlistOverviewComponent,
        children: [
            {
                path: ':id',
                component: QuestionlistDetailComponent,
                resolve: {
                  questionList: QuestionListResolver
                }
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    providers: [QuestionListResolver]
})
export class AppRoutingModule { }
