import { Component, OnInit } from '@angular/core';
import { RouteTo } from 'app/shared/interfaces/local-router';

@Component({
  selector: 'app-desktop-footer',
  templateUrl: './desktop-footer.component.html',
  styleUrls: ['./desktop-footer.component.scss']
})
export class DesktopFooterComponent implements OnInit {
  title: string = 'UpdatedCloud';
  constructor(private routeLink: RouteTo) { }

  ngOnInit() {
  }

  RouteTo(routeTo: string, routingEnabled = true): void {
    console.log('now routing: ', routeTo);
    this.routeLink.RouteTo(routeTo, routingEnabled);
  }

}

