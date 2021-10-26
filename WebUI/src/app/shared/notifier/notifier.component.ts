import { Component, OnInit, Inject } from '@angular/core';
import { MAT_SNACK_BAR_DATA, MatSnackBarRef } from '@angular/material/snack-bar'

@Component({
  selector: 'app-notifier',
  templateUrl: './notifier.component.html',
  styleUrls: ['./notifier.component.scss']
})
export class NotifierComponent implements OnInit {

  countdownNumber: string;
  SetMinutesCountdown = 5;

  constructor( @Inject(MAT_SNACK_BAR_DATA) public data: any,
    public snackBarRef: MatSnackBarRef<NotifierComponent>) {
    this.startTimer(this.SetMinutesCountdown);
  }

  ngOnInit(): void {
    console.log(this.data);
  }

  startTimer(duration: number) {
    let timer = duration;
    let minutes = 0;
    let seconds = 0;
    setInterval(x => {
      minutes = duration / 60;
      seconds = duration % 60;

      minutes = minutes < 10 ? 0 + minutes : minutes;
      seconds = seconds < 10 ? 0 + seconds : seconds;

      this.countdownNumber = 0 + ":" + seconds;

      if (--timer < 0) {
        timer = duration;
      }

    }, 1000);
  }

}
