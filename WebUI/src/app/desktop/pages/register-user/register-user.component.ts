import { Component, OnInit } from '@angular/core';
import { RouteTo } from "app/shared/interfaces/local-router";

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.scss']
})
export class RegisterUserComponent implements OnInit {
  registerCategories = [
    { id: 0, CardTitle: 'Register As Standard', isChecked: false, routeto: 'StandardAccount', footnote: 'If you are only buyer or visitor, register standard account. This will allow you to perform normal operation and have standard profile.' },
    { id: 1, CardTitle: 'Register As Seller', isChecked: false, routeto: 'SellerAccount', footnote: 'If you intend to sell, register as the seller. Seller has the most powerful tool to be up to date on ther products and content-management.' }
  ]
  constructor(private routeLink: RouteTo) { }

  ngOnInit(): void {
  }

  cardOnClick(id: number) {
    if (id !== 0 && id !== 1) {
      return;
    }

    for (let i = 0; i < this.registerCategories.length; i++) {
      if (i == id) {
        this.registerCategories[i].isChecked = !this.registerCategories[i].isChecked;
      } else {
        this.registerCategories[i].isChecked = false;
      }
    }

    this.RouteTo(this.registerCategories[id].routeto);

  }

  RouteTo(routeTo: string, routingEnabled = true): void {
    console.log('now routing: ', routeTo);
    this.routeLink.RouteTo(routeTo, routingEnabled);
  }
}
