import { Observable, BehaviorSubject } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class AngContext {
    _feDataContext = new BehaviorSubject<FEDataContext>(null);
    _data = new FEDataContext();

    constructor() { }

    getAngUserContext(): Observable<FEDataContext> {
        return this._feDataContext.asObservable();
    }

    feedCntxData(user: FEDataContext): boolean {
        this._feDataContext.next(user);
        return true;
    }
}

export class FEDataContext {
    userHasSellerAC: boolean;
    userIsLoggedIn: boolean;
    AngIsAuth: boolean;
    AngSessionID: string;
    AngAuthKey: string;
}

//toFeed
// const user = new FEDataContext();
// user.AngAuthKey = "";
// user.AngIsAuth = true;
// user.AngSessionID = "jdfhskajdghkjshgkj";
// user.userHasSellerAC = false;
// user.userIsLoggedIn = true;
// this.angContext.feedCntxData();


// simply, call like below where ever needed to check
// this.angContext.getAngUserContext().subscribe((data) => {
//     this.componentdata = data;
//     console.log('Spinner spinner_lg :  ', this.componentdata);
// })