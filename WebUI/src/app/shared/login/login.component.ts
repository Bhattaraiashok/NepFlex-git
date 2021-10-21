import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { RouteTo } from '../interfaces/local-router';
import { LoginService } from "app/shared/services/login.service";
import { LoginResponse, LoginRequest } from "app/shared/ResourceModels/LoginModel";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
import { AlertMessageProperties, CONSTList } from "app/shared/ResourceModels/AlertMessages";
import { ButtonProperties } from "app/shared/ResourceModels/ButtonProperties";
import { NotificationService } from "app/shared/services/control-services/notification.service";
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginRequest: LoginRequest = new LoginRequest();
  loginResponse: LoginResponse = new LoginResponse();
  messageAlerts: AlertMessageProperties = new AlertMessageProperties();
  CONSTList: CONSTList=new CONSTList();
  loginComponentButttons: ButtonProperties[] = new Array();
  showAlertMessages: boolean = false;
  showFCError: boolean = false;
  disableLoginButton: boolean = true;

  userIsInProcessOfLogin: boolean = false;
  spinnerActivated: boolean = false;

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  matcher = new MyErrorStateMatcher();
  hide = true;
  passwordType = 'password';
  loginForm: FormGroup;

  usernameFC: FormControl = new FormControl('', [Validators.required]);
  passwordFC: FormControl = new FormControl('', [Validators.required]);
  isRememberMeFC: FormControl = new FormControl('');

  usernameFCError: string;
  passwordFCError: string;

  constructor(private fb: FormBuilder,
    private routeLink: RouteTo,
    private loginService: LoginService,
    private modalService: NgbActiveModal,
    private notificationService: NotificationService) {
    this.passwordFC.disable(true)
    this.createForm();
  }

  ngOnInit(): void {
    this.allButtons();
  }

  allButtons() {
    this.loginComponentButttons = [
      {
        buttonId: 1,
        buttonLabel: 'Login',
        isDisable: (!this.loginForm.valid && this.disableLoginButton),
        tooltip: (!this.loginForm.valid && this.disableLoginButton) ? "Please, provide login info." : " Click here to login.",
        hasPopUp: false,
        buttonRoute: '',
        canRoute: false,
        HasDropDown: false,
        parentEmit: true
      }
    ];
  }

  createForm() {
    this.loginForm = this.fb.group({
      username: this.usernameFC,
      password: this.passwordFC,
      isRememberMe: this.isRememberMeFC
    });
  }

  smallSpinner() {
    this.spinnerActivated = !this.spinnerActivated;
    console.log('turnOnSmallLoader :  ', this.spinnerActivated);
  }

  validateForm(e: Event) {
    this.smallSpinner();
    this.mapping();
    const firstLayerValidation = this.userValidation();
    if (firstLayerValidation === true && this.loginForm.valid) {
      this.loginService.login(this.loginRequest).subscribe((item: LoginResponse) => {
        if (item.isSuccess == true) {
          this.call_MessageAlertComponent(this.CONSTList.success, item.strMessage[0]);
          this.manageLocalStorage(item);
          this.RouteTo('home');
          this.modalService.close('close');
        } else {
          if (item.strMessage[0] !== null && item.strMessage[0] !== undefined) {
            this.call_MessageAlertComponent(this.CONSTList.error, item.strMessage[0]);
          } else {
            this.call_MessageAlertComponent(this.CONSTList.error, '!! ERROR !!');
          }
          this.smallSpinner();
        }
      });
    } else {
      this.validationErrorMapping();
      this.smallSpinner();
    }

  }
  validationErrorMapping() {
    this.showFCError = true;
    if (this.loginForm.touched || this.loginForm.dirty) {
      if (
        this.usernameFC.invalid &&
        this.usernameFC.hasError('required')
      ) {
        this.usernameFCError = "You must enter Username.";
        this.call_MessageAlertComponent(this.CONSTList.info, this.usernameFCError);
        return;
      } else if (
        this.passwordFC.invalid && this.passwordFC.enabled==true &&
        this.passwordFC.hasError('required')
      ) {
        this.passwordFCError = "You must enter Password.";
        this.call_MessageAlertComponent(this.CONSTList.info, this.passwordFCError);
        return;
      } else {
        this.passwordFCError = '';
        this.passwordFC.enable(true);
      }
    } else {
      this.showFCError = false;
      this.passwordFCError = '';
      this.usernameFCError = '';
      this.passwordFC.enable(true);
      return;
    }
  }

  onchangeform(
    event: { target; value: string },
    formControlName: string
  ) {

    const val = event.target.value;
    this.loginForm.get(formControlName).patchValue(val);
    this.validationErrorMapping();
    this.disableLoginButton == this.showFCError;
    this.allButtons();
  }

  userValidation(): boolean {
    const valResult = ((this.loginRequest.UserID != null && this.loginRequest.UserID != undefined && this.loginRequest.UserID != '')
      && (this.loginRequest.UserPSWD != null && this.loginRequest.UserPSWD != undefined && this.loginRequest.UserPSWD != ''));
    this.allButtons();
    return valResult;
  }

  mapping() {
    this.loginRequest.UserID = this.loginForm.get('username').value;
    this.loginRequest.UserPSWD = this.loginForm.get('password').value;
    this.loginRequest.IsRememberMe = this.loginForm.get('isRememberMe').value;
  }

  manageLocalStorage(item: LoginResponse) {
    // if it comes here firstly lets clear localstorage
    localStorage.clear();
    //now start assigning
    console.log("LOGGEDDDDDDD", item);
    localStorage.setItem('isLoggedIn', "true");
    localStorage.setItem('_authSessionToken', item.sessionID);
  }

  passwordShowField(e: Event) {
    if (this.hide) {
      this.passwordType = 'text';
      this.hide = false;
    } else {
      this.passwordType = 'password';
      this.hide = true;
    }
  }

  RouteTo(routeTo: string, routingEnabled = true): void {
    console.log('now routing: ', routeTo);
    this.routeLink.RouteTo(routeTo, routingEnabled);
  }

  call_MessageAlertComponent(alertType, alertMsg) {
    this.showAlertMessages = true;
    this.messageAlerts.alertType = alertType;
    this.messageAlerts.alertMsg = alertMsg;
    this.messageAlerts.alertBtnLabel = 'OK';
    this.messageAlerts.showButton = true;
    this.notificationService.showNotification(this.messageAlerts);
  }

}
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
