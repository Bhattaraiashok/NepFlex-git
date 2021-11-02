import { Component, OnInit } from '@angular/core';
import { IDeactivateComponent } from "app/shared/guards/can-deactivate-guard.service";

@Component({
  selector: 'app-seller-user-registration',
  templateUrl: './seller-user-registration.component.html',
  styleUrls: ['./seller-user-registration.component.scss']
})
export class SellerUserRegistrationComponent implements OnInit,IDeactivateComponent {
  canExit() {
    // if (this.userRegisterForm.dirty) {
    //   const res = window.confirm('You have not saved changes yet. Are you sure you want to cancel?');
    //   return res;
    // }
    return true;
  }
  constructor() { }

  ngOnInit(): void {
  }

}
