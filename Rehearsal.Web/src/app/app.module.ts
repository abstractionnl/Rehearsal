import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AlertModule } from 'ngx-bootstrap';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

// Imports for loading & configuring the in-memory web api
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard.component';
import { HeroesComponent } from './heroes.component';
import { HeroDetailComponent } from './hero-detail.component';
import { HeroSearchComponent } from './hero-search.component';

import { HeroService } from './hero.service';

import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
      AppComponent,
      DashboardComponent,
      HeroesComponent,
      HeroDetailComponent,
      HeroSearchComponent
  ],
  imports: [
      BrowserModule, FormsModule, HttpModule,
      AlertModule.forRoot(), NgbModule.forRoot(),
      AppRoutingModule
  ],
  providers: [
      HeroService
  ],
  bootstrap: [
      AppComponent
  ]
})
export class AppModule { }
