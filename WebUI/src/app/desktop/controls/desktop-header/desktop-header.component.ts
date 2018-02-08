import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-desktop-header',
  templateUrl: './desktop-header.component.html',
  styleUrls: ['./desktop-header.component.scss']
})
export class DesktopHeaderComponent implements OnInit {
  title: string = 'NepaliCraig';
  headersNavigation: [
    {
      headerId: number;
      header: string;
      headerRoute: string;
      canRoute: boolean;
      HasDropDown: boolean;
      DropDownList: [{ label: string }];
    }
  ];

  constructor(private router: Router) {
    this.headersNavigation = [
      {
        headerId: 1,
        header: 'home',
        headerRoute: 'home',
        canRoute: true,
        HasDropDown: false,
        DropDownList: [{ label: '' }]
      },
      {
        headerId: 2,
        header: 'About Us',
        headerRoute: 'home',
        canRoute: true,
        HasDropDown: false,
        DropDownList: [{ label: '' }]
      },
      {
        headerId: 3,
        header: 'Contact Us',
        headerRoute: 'home',
        canRoute: true,
        HasDropDown: false,
        DropDownList: [{ label: '' }]
      },
      {
        headerId: 4,
        header: 'Report Item',
        headerRoute: 'home',
        canRoute: true,
        HasDropDown: false,
        DropDownList: [{ label: '' }]
      },
      {
        headerId: 5,
        header: 'Terms Of Use',
        headerRoute: 'Terms Of Use',
        canRoute: true,
        HasDropDown: false,
        DropDownList: [{ label: '' }]
      },
      {
        headerId: 6,
        header: 'Notification',
        headerRoute: 'Notification',
        canRoute: true,
        HasDropDown: true,
        DropDownList: [{ label: 'you have 0 notifications' }]
      },
      {
        headerId: 7,
        header: 'NC Latest',
        headerRoute: 'home',
        canRoute: false,
        HasDropDown: true,
        DropDownList: [{ label: 'FAQ' }, { label: 'Latest Release' }]
      },
      {
        headerId: 8,
        header: 'My Account',
        headerRoute: 'account',
        canRoute: true,
        HasDropDown: true,
        DropDownList: [{ label: 'Post Items' }, { label: 'LogIn' }]
      }
    ];
  }

  ngOnInit(): void {}
  HeaderRoute(routeTo: string, routingEnabled: boolean): void {
    if (routingEnabled) {
      this.router.navigate([routeTo]);
    }
  }
}