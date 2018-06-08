import {Component, OnDestroy, OnInit} from '@angular/core';
import {Alert, AlertService} from './alert.service'
import {Subscription} from "rxjs";

@Component({
  selector: 'alerts',
  templateUrl: './alert.component.html',
  styleUrls: [ './alert.component.css' ]
})
export class AlertComponent implements OnInit, OnDestroy {
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

  alerts: Alert[];

  constructor(private alertService: AlertService) { }
}
