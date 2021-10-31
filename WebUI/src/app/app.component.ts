import { Component } from '@angular/core';
import { Router, NavigationEnd, NavigationStart, Event, NavigationCancel, NavigationError } from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  showloadingIndicator = true;
  constructor(private route: Router) {
    this.route.events.subscribe((routeEvent: Event) => {
      if (routeEvent instanceof NavigationStart) {
        this.showloadingIndicator = true;
      }
      if (routeEvent instanceof NavigationEnd
        || routeEvent instanceof NavigationCancel
        || routeEvent instanceof NavigationError) {
        this.showloadingIndicator = false;
      }
      console.log(' this.showloadingIndicator::::::::: ', this.showloadingIndicator);
    })
  }
}
