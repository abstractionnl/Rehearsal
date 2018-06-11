import {Injectable} from "@angular/core";
import {Subject, Observable} from "rxjs";

export interface Notification {
  message: string,
  type: string,
  err: any
}

@Injectable()
export class NotificationsService {
  private notifications$: Subject<Notification>;

  constructor() {
    this.notifications$ = new Subject<Notification>();
  }

  getAlerts(): Observable<Notification> {
    return this.notifications$.asObservable();
  }

  success(message: string) {
    this.notifications$.next({
      type: 'success',
      message: message,
      err: null
    });
  }

  info(message: string) {
    this.notifications$.next({
      type: 'info',
      message: message,
      err: null
    });
  }

  warning(message: string, err?: any) {
    this.notifications$.next({
      type: 'warning',
      message: message,
      err: err
    });
  }

  fail(message: string, err?: any) {
    this.notifications$.next({
      type: 'danger',
      message: message,
      err: err
    });
  }
}
