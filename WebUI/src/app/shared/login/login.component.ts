import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, FormGroupDirective, NgForm } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { RouteTo } from '../interfaces/local-router';
import { LoginService } from "app/shared/services/login.service";
import { LoginResponse, LoginRequest } from "app/shared/ResourceModels/LoginModel";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginRequest: LoginRequest;
  loginResponse: LoginResponse;
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  matcher = new MyErrorStateMatcher();
  hide = true;
  passwordType = 'password';
  form = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
    isRememberMe: ['']
  })
  constructor(private fb: FormBuilder,
    private routeLink: RouteTo,
    private loginService: LoginService,
    private modalService: NgbActiveModal) { }

  ngOnInit(): void {
  }

  validateForm(e: Event) {
    const formValue1 = this.form.get('username').value;
    const formValue2 = this.form.get('password').value;
    const _isRememberMe = this.form.get('isRememberMe').value;
    console.log('username', formValue1 + ' and ' + formValue2);
    if (formValue1 !== null && formValue2 !== null) {
      this.loginRequest = new LoginRequest();
      this.loginRequest.UserID = formValue1;
      this.loginRequest.UserPSWD = formValue2;
      this.loginRequest.IsRememberMe = _isRememberMe;
      this.loginService.login(this.loginRequest).subscribe((item: LoginResponse) => {
        this.manageLocalStorage(item);
        this.RouteTo('home');
        this.modalService.close('close');
      });
    }
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

}
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
