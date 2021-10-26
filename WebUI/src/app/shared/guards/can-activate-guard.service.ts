import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class CanActivateGuardService implements CanActivate {
  constructor(private _router: Router) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.cacheChecker()) {
      return true;
    }
    this._router.navigate(['error']);
  }

  cacheChecker(): boolean {
    const isStillSessionActive = localStorage.getItem("_authSessionToken");

    const isStillUserLoggedIn = localStorage.getItem("isLoggedIn");

    if (isStillSessionActive !== null && isStillUserLoggedIn !== null && isStillUserLoggedIn !== undefined) {
      return true;
    }

    return false;
  }
}
