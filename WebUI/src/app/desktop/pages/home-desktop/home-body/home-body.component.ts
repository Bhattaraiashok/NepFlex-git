import { Component, OnInit } from '@angular/core';
import { RouteTo } from 'app/shared/interfaces/local-router';
import { ContainerType } from 'app/shared/ResourceModels/enumerator';

@Component({
  selector: 'app-home-body',
  templateUrl: './home-body.component.html',
  styleUrls: ['./home-body.component.scss']
})
export class HomeBodyComponent implements OnInit {
  containerType_News = ContainerType.NewsItem;
  constructor(private routeLink: RouteTo) { }

  ngOnInit() {
  }

  RouteTo(routeTo: string, routingEnabled = true): void {
    console.log('now routing: ', routeTo);
    this.routeLink.RouteTo(routeTo, routingEnabled);
  }
}
