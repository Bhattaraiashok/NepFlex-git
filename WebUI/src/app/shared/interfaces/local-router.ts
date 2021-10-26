import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable()
export class RouteTo {
  constructor(private router: Router) { }
    RouteTo(routeTo: string, routingEnabled: boolean): void {
      if (routingEnabled) {
        this.router.navigate([routeTo]);
      }
    }
  }
