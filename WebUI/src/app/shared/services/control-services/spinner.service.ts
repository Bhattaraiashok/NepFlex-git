import { Injectable } from '@angular/core';
import { SpinnerComponent } from "app/shared/spinner/spinner.component";
import { BehaviorSubject, Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {
  //This spinner service when on call it has three spinner catergories but in different method.
  spin_lg = new BehaviorSubject<string>('');
  spin_sm = new BehaviorSubject<string>('');
  spin_disable_sm = new BehaviorSubject<string>('');
  private count_lg = 0;
  private count_sm = 0;
  private count_disable_sm = 0;

  constructor() { }

  getSpinner_lg_Observer(): Observable<string> {
    return this.spin_lg.asObservable();
  }

  getSpinner_sm_Observer(): Observable<string> {
    return this.spin_sm.asObservable();
  }

  getSpinner_disable_sm_Observer(): Observable<string> {
    return this.spin_disable_sm.asObservable();
  }

  // small disabled spinner
  showSpinner_Disabled_sm(): boolean {
    this.count_disable_sm++;
    if (this.count_disable_sm == 1) {
      this.spin_disable_sm.next('start');
    }
    const result = (this.count_disable_sm > 0) ? true : false;
    return result;
  }
  disableSpinner_Disabled_sm(): boolean {
    if (this.count_disable_sm === 0 || --this.count_disable_sm === 0) {
      this.spin_disable_sm.next('stop');
    }
    const result = (this.count_disable_sm > 0) ? true : false;
    return result;
  }
  resetSpinner_Disabled_sm(): boolean {
    this.count_disable_sm = 0;
    this.spin_disable_sm.next('stop');
    const result = (this.count_disable_sm > 0) ? true : false;
    return result;
  }

  // Large scale spinner
  showSpinner_lg(): boolean {
    this.count_lg++;
    if (this.count_lg == 1) {
      this.spin_lg.next('start');
    }
    const result = (this.count_lg > 0) ? true : false;
    return result;
  }
  disableSpinner_lg(): boolean {
    if (this.count_lg === 0 || --this.count_lg === 0) {
      this.spin_lg.next('stop');
    }
    const result = (this.count_lg > 0) ? true : false;
    return result;
  }
  resetSpinner_lg(): boolean {
    this.count_lg = 0;
    this.spin_lg.next('stop');
    const result = (this.count_lg > 0) ? true : false;
    return result;
  }

  //small spinner
  showSpinner_sm(): boolean {
    this.count_sm++;
    if (this.count_sm == 1) {
      this.spin_sm.next('start');
    }
    const result = (this.count_sm > 0) ? true : false;
    return result;
  }
  disableSpinner_sm(): boolean {
    if (this.count_sm === 0 || --this.count_sm === 0) {
      this.spin_sm.next('stop');
    }
    const result = (this.count_sm > 0) ? true : false;
    return result;
  }
  resetSpinner_sm(): boolean {
    this.count_sm = 0;
    this.spin_sm.next('stop');
    const result = (this.count_sm > 0) ? true : false;
    return result;
  }
}
