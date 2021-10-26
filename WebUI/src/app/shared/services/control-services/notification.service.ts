import { Injectable } from '@angular/core';
import {
  MatSnackBar,
  MatSnackBarHorizontalPosition,
  MatSnackBarVerticalPosition,
} from '@angular/material/snack-bar';
import { NotifierComponent } from "app/shared/notifier/notifier.component";
import { AlertMessageProperties } from "app/shared/ResourceModels/AlertMessages";

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  // These are the option horizontalPosition = 'start' | 'center' | 'end' | 'left' | 'right';
  //verticalPosition = 'top' | 'bottom';
  horizontalPosition: MatSnackBarHorizontalPosition = 'end';
  verticalPosition: MatSnackBarVerticalPosition = 'top';

  durationInSeconds = 5;

  constructor(private _snackBar: MatSnackBar, private msgAlert: AlertMessageProperties) { }
  //format of alert as of now
  // msgAlert.alertType = alertType;
  // msgAlert.alertMsg = alertMsg;
  // msgAlert.alertBtnLabel = 'OK';
  // msgAlert.showCloseButton = true;
  showNotification(msgAlert) {
    this._snackBar.openFromComponent(NotifierComponent, {
      data: msgAlert,
      //duration: this.durationInSeconds * 1000, //5 sec
      horizontalPosition: this.horizontalPosition,
      verticalPosition: this.verticalPosition,
      panelClass: msgAlert.alertType
    });
  }
}
