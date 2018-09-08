import {Component, OnDestroy, OnInit} from '@angular/core';
import {Notification} from '../../services/notifications.service'
import {Subscription} from "rxjs";
import {Actions} from "@ngrx/effects";
import {NotificationActions, SuccessNotification} from "../../store/notifications.actions";

@Component({
  selector: 'notifications',
  templateUrl: './notifications.component.html',
  styleUrls: [ './notifications.component.css' ]
})
export class NotificationsComponent implements OnInit, OnDestroy {
  private subscription: Subscription[];

  ngOnInit(): void {
      this.alerts = [];
      this.subscription = [];
      this.subscription.push(this.actions$
        .ofType<SuccessNotification>(NotificationActions.SUCCESS)
        .subscribe(a => this.alerts.push({ type: 'success', message: a.message, err: null })));

      this.subscription.push(this.actions$
          .ofType<SuccessNotification>(NotificationActions.INFO)
          .subscribe(a => this.alerts.push({ type: 'info', message: a.message, err: null })));

      this.subscription.push(this.actions$
          .ofType<SuccessNotification>(NotificationActions.WARNING)
          .subscribe(a => this.alerts.push({ type: 'warning', message: a.message, err: null })));

      this.subscription.push(this.actions$
          .ofType<SuccessNotification>(NotificationActions.ERROR)
          .subscribe(a => this.alerts.push({ type: 'error', message: a.message, err: null })));
  }

  ngOnDestroy(): void {
    this.subscription.forEach(s => s.unsubscribe());
    this.subscription = [];
  }

  alerts: Notification[];

  constructor(private actions$: Actions) { }
}
