import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { AngContext, FEDataContext } from "app/shared/ResourceModels/AngContext";

@Injectable({
  providedIn: 'root'
})
export class CanActivateGuardService implements CanActivate {
  componentData = new FEDataContext();
  constructor(private _router: Router, private angContext: AngContext) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

    console.log('componentData second:  ', this.componentData);

    return true; //for now in development ZONE

    // if (this.cacheChecker()) {
    //   return true;
    // }

    //this._router.navigate(['error']);
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
