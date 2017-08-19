﻿
import {Injectable} from "@angular/core";
import {Subject} from "rxjs/Subject";
import {Observable} from "rxjs/Observable";

export interface Alert {
  message: string,
  type: string,
  err: any
}

@Injectable()
export class AlertService {
  private alerts$: Subject<Alert>;

  constructor() {
    this.alerts$ = new Subject<Alert>();
  }

  getAlerts(): Observable<Alert> {
    return this.alerts$.asObservable();
  }

  success(message: string) {
    this.alerts$.next({
      type: 'success',
      message: message,
      err: null
    });
  }

  info(message: string) {
    this.alerts$.next({
      type: 'success',
      message: message,
      err: null
    });
  }

  warn(message: string, err: any) {
    this.alerts$.next({
      type: 'warning',
      message: message,
      err: err
    });
  }

  fail(message: string, err: any) {
    this.alerts$.next({
      type: 'danger',
      message: message,
      err: err
    });
  }
}