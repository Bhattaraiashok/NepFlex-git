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
  horizontalPosition: MatSnackBarHorizontalPosition = 'start';
  verticalPosition: MatSnackBarVerticalPosition = 'bottom';

  durationInSeconds = 5;

  constructor(private _snackBar: MatSnackBar, private msgAlert: AlertMessageProperties) { }

  // showNotification(displayMsg: string, buttonText: string) {
  //   this._snackBar.open(displayMsg, buttonText, {
  //     duration: this.durationInSeconds * 1000, //5 sec
  //     horizontalPosition: this.horizontalPosition,
  //     verticalPosition: this.verticalPosition,
  //   });
  // }

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
