import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { RouteTo } from '../interfaces/local-router';
import { ButtonProperties } from '../ResourceModels/ButtonProperties';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  detailButttons: ButtonProperties[] = new Array();
  showFCError: boolean = false;
  hide = true;
  passwordType = 'password';
  matcher = new MyErrorStateMatcher();

  companyFC: FormControl = new FormControl('', [Validators.required]);
  firstnameFC: FormControl = new FormControl('', [Validators.required]);
  lastnameFC: FormControl = new FormControl('', [Validators.required]);
  addressFC: FormControl = new FormControl('', [Validators.required]);
  address2FC: FormControl = new FormControl('', [Validators.required]);
  cityFC: FormControl = new FormControl('', [Validators.required]);
  stateFC: FormControl = new FormControl('', [Validators.required]);
  zipcodeFC: FormControl = new FormControl('', [Validators.required]);
  emailFC: FormControl = new FormControl('', [Validators.required, Validators.email]);
  phonenumberFC: FormControl = new FormControl('', [Validators.required]);
  usernameFC: FormControl = new FormControl('', [Validators.required]);
  passwordFC: FormControl = new FormControl('', [Validators.required]);

  companyFCError: string;
  firstnameFCError: string;
  lastnameFCError: string;
  addressFCError: string;
  address2FCError: string;
  cityFCError: string;
  stateFCError: string;
  zipcodeFCError: string;
  emailFCError: string;
  phonenumberFCError: string;
  usernameFCError: string;
  passwordFCError: string;


  constructor(private fb: FormBuilder, private routeLink: RouteTo) {
    this.createForm();
  }

  ngOnInit(): void {
    this.allButtons();
  }
  allButtons() {
    this.detailButttons = [
      {
        buttonId: 1,
        buttonLabel: 'Register Now',
        hasPopUp: false,
        buttonRoute: '',
        canRoute: false,
        HasDropDown: false,
        DropDownList: [{ id: 1, label: '', url: '' }],
        popUpName: 'clickSendEmailButton'
      }
    ];
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

  createForm() {
    this.registerForm = this.fb.group({
      company: this.companyFC,
      firstname: this.firstnameFC,
      lastname: this.lastnameFC,
      address: this.addressFC,
      address2: this.address2FC,
      city: this.cityFC,
      state: this.stateFC,
      zipcode: this.zipcodeFC,
      email: this.emailFC,
      phonenumber: this.phonenumberFC,
      username: this.usernameFC,
      password: this.passwordFC
    })
  }

  onchangeform(
    event: { target; value: string },
    formControlName: string
  ) {
    const val = event.target.value;
    this.registerForm.get(formControlName).patchValue(val);
    this.validateForrm();
    // console.log(
    //   'senderEmailVal: ',
    //   val,
    //   'formControlName: ',
    //   formControlName,
    //   'senderEmailForm: ',

    //   this.sendEmailForm.get(formControlName).value
    // );
  }

  validateForrm() {
    this.showFCError = true;
    if (this.registerForm.invalid) {
      if (
        this.companyFC.invalid &&
        this.companyFC.hasError('required')
      ) {
        this.companyFCError = 'You must enter your company name';
        return;
      } else {
        this.companyFCError = '';
      }

      if (this.emailFC.invalid && this.emailFC.hasError('email')) {
        this.emailFCError = 'Not a valid email';
        return;
      } else {
        this.emailFCError = '';
      }

      if (
        this.firstnameFC.invalid &&
        this.firstnameFC.hasError('required')
      ) {
        this.firstnameFCError = 'You must enter your firstname';
        return;
      } else {
        this.firstnameFCError = '';
      }

      if (this.lastnameFC.invalid && this.lastnameFC.hasError('required')) {
        this.lastnameFCError = 'You must enter your lastname';
        return;
      } else {
        this.lastnameFCError = '';
      }

      if (
        this.usernameFC.invalid &&
        this.usernameFC.hasError('required')
      ) {
        this.usernameFCError = 'You must create a username';
        return;
      } else {
        this.usernameFCError = '';
      }

      if (this.passwordFC.invalid && this.passwordFC.hasError('required')) {
        this.passwordFCError = 'You must create a new password';
        return;
      } else {
        this.passwordFCError = '';
      }
    } else {
      this.showFCError = false;
      this.passwordFCError = '';
      this.usernameFCError = '';
      this.emailFCError = '';
      this.firstnameFCError = '';
      this.lastnameFCError = '';
      this.companyFCError = '';
      this.addressFCError = '';
      this.address2FCError = '';
      this.phonenumberFCError = '';

    }
  }

  RouteTo(routeTo: string, routingEnabled = true): void {
    console.log('now routing: ', routeTo);
    this.routeLink.RouteTo(routeTo, routingEnabled);
  }

  registerNow() {
    this.validateForrm();
    if (this.registerForm.valid) {
      console.log('FORM IS VALID');
      // const val = Object.assign(
      //   new registerFormProperties(),
      //   this.registerForm.value
      // );
      // this.registerService.register(val).subscribe(a => {
      //   alert('registered');
      // });
    }
  }
}
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
