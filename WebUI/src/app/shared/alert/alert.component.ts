import { Component, OnInit, Input } from '@angular/core';
import { AlertMessageProperties } from "app/shared/ResourceModels/AlertMessages";

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent implements OnInit {
  @Input() messageAlert: AlertMessageProperties;
  constructor() {
  }

  ngOnInit(): void {
    console.log('messageAlert', this.messageAlert);
  }

  autoFadeAlert(e: Event) {

  }

}
