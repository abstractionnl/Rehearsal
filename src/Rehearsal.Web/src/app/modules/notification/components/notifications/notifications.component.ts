import {Component, OnDestroy, OnInit} from '@angular/core';
import {Notification, NotificationsService} from '../../services/notifications.service'
import {Subscription} from "rxjs";

@Component({
  selector: 'notifications',
  templateUrl: './notifications.component.html',
  styleUrls: [ './notifications.component.css' ]
})
export class NotificationsComponent implements OnInit, OnDestroy {
  private subscription: Subscription;

  ngOnInit(): void {
    this.alerts = [];
    this.subscription = this.alertService
      .getAlerts()
      .subscribe(x => this.alerts.push(x));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  alerts: Notification[];

  constructor(private alertService: NotificationsService) { }
}
