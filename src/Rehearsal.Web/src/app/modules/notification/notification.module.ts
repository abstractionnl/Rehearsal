import {NgModule} from "@angular/core";
import {CommonModule} from '@angular/common';
import {EffectsModule} from "@ngrx/effects";

import {AlertModule} from 'ngx-bootstrap';

import {NotificationsService} from './services/notifications.service';

import {NotificationsComponent} from './components/notifications/notifications.component';

import {NotificationsEffects} from "./store/notifications.effects";

@NgModule({
    declarations: [
        NotificationsComponent
    ],
    imports: [
        CommonModule,
        AlertModule,
        EffectsModule.forFeature([NotificationsEffects])
    ],
    exports: [
        NotificationsComponent
    ],
    providers: [
        NotificationsService
    ]
})
export class NotificationModule { }
