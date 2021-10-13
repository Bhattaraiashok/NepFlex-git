import { Component, OnInit, Input } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RouteTo } from 'app/shared/interfaces/local-router';
import { LoginComponent } from 'app/shared/login/login.component';
import { HeadersNavigation, ButtonProperties, DropDownList } from 'app/shared/ResourceModels/ButtonProperties';
import { RegisterComponent } from "app/shared/register/register.component";

@Component({
  selector: 'app-desktop-header',
  templateUrl: './desktop-header.component.html',
  styleUrls: ['./desktop-header.component.scss']
})
export class DesktopHeaderComponent implements OnInit {
  @Input()
  isThisComingFromHomePage: boolean = false;
  title: string = 'NepaliCraig';
  headersNavigation: HeadersNavigation[] = new Array();
  showLoginPopUpModal = false;
  showRegisterationPopUpModal=false;
  isLoggedIn = false;
  detailButttons: ButtonProperties[] = new Array();
  dropdownlist: DropDownList[] = new Array();

  constructor(private routeLink: RouteTo, private modalService: NgbModal) {
    this.headersNavigation = [
      {
        headerId: 1,
        header: 'About Us',
        headerRoute: 'aboutus',
        canRoute: true,
        hasDropDown: false,
        dropDownList: [{ id: 1, displayName: '' }]
      },
      {
        headerId: 2,
        header: 'Report Item',
        headerRoute: 'report',
        canRoute: true,
        hasDropDown: false,
        dropDownList: [{ id: 2, displayName: '' }]
      },
      {
        headerId: 3,
        header: 'NC Latest',
        headerRoute: 'home',
        canRoute: false,
        hasDropDown: true,
        dropDownList: [
          { id: 3, displayName: 'FAQ', route: 'faq' },
          { id: 4, displayName: 'Latest Release' }
        ]
      },
      {
        headerId: 4,
        header: 'My Account',
        headerRoute: 'account',
        canRoute: true,
        hasDropDown: true,
        dropDownList: [
          { id: 5, displayName: 'Post Items' },
          {
            id: 6,
            displayName: 'LogIn',
            children: [{ id: 5.1, displayName: 'Log Me In' }]
          }
        ]
      },
      {
        headerId: 5,
        header: '',
        headerRoute: 'Notification',
        canRoute: true,
        hasDropDown: true,
        dropDownList: [
          {
            id: 7,
            displayName: 'you have 0 notifications'
          }
        ]
      }
    ];
  }

  ngOnInit(): void { this.checkifUserIsLogedIn(); }

  // HeaderRoute(routeTo: string, routingEnabled: boolean): void {
  //   if (routingEnabled) {
  //     this.router.navigate([routeTo]);
  //   }
  // }

  RouteTo(routeTo: string, routingEnabled = true): void {
    console.log('now routing: ', routeTo);
    this.routeLink.RouteTo(routeTo, routingEnabled);
  }

  loginPopUp() {
    this.showLoginPopUpModal = true;
    // console.log('showPopUpModal: ', this.showPopUpModal);
    this.modalService.open(LoginComponent, { windowClass: 'dark-modal' });
  }

  registerPopUp() {
    this.showRegisterationPopUpModal = true;
    // console.log('showRegisterationPopUpModal: ', this.showPopUpModal);
    this.modalService.open(RegisterComponent, { windowClass: 'dark-modal' });
  }

  checkifUserIsLogedIn() {
    var userStatus = localStorage.getItem('isLoggedIn');
    if (userStatus == 'true') {
      this.isLoggedIn = true;
      this.allButtons();
    } else {
      this.isLoggedIn = false;
    }
  }
  allButtons() {
    this.dropdownlist = [
      { id: 1,parentLabel:'MyAccounthere', displayName: 'edit' },
      { id: 2,parentLabel:'MyAccounthere', displayName: 'Post' }
    ]

    this.detailButttons = [
      {
        buttonId: 1,
        buttonLabel: 'MY ACCOUNT',
        hasPopUp: false,
        buttonRoute: '',
        canRoute: false,
        HasDropDown: true,
        DropDownList: this.dropdownlist,
        popUpName: 'MyAccount2'
      }
    ];

  }
}
