import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { RouteTo } from '../interfaces/local-router';
import { ButtonProperties } from '../ResourceModels/ButtonProperties';
import { RegisterService } from "app/shared/services/register.service";
import { RegisterResponse, UserRegister } from "app/shared/ResourceModels/registerModel";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  UserRegister: UserRegister = new UserRegister();
  registerResponse: RegisterResponse;
  registerForm: FormGroup;
  detailButttons: ButtonProperties[] = new Array();
  showFCError: boolean = false;
  hide = true;
  passwordType = 'password';
  matcher = new MyErrorStateMatcher();

  companyFC: FormControl = new FormControl('');
  firstnameFC: FormControl = new FormControl('', [Validators.required]);
  middlenameFC: FormControl = new FormControl('');
  lastnameFC: FormControl = new FormControl('', [Validators.required]);
  addressFC: FormControl = new FormControl('');
  address2FC: FormControl = new FormControl('');
  cityFC: FormControl = new FormControl('');
  stateFC: FormControl = new FormControl('');
  zipcodeFC: FormControl = new FormControl('');
  userEmailFC: FormControl = new FormControl('', [Validators.required, Validators.email]);
  companyEmailFC: FormControl = new FormControl('', [Validators.required, Validators.email]);
  userCountryCodeFC: FormControl = new FormControl('', [Validators.pattern(/^\d{3}$/), Validators.required]);
  userPhonenumberFC: FormControl = new FormControl('', [Validators.pattern(/^\(\d{3}\)\s\d{3}-\d{4}$/), Validators.required]);
  companyCountryCodeFC: FormControl = new FormControl('', [Validators.pattern(/^\d{3}$/), Validators.required]);
  companyPhonenumberFC: FormControl = new FormControl('', [Validators.pattern(/^\(\d{3}\)\s\d{3}-\d{4}$/), Validators.required]);
  usernameFC: FormControl = new FormControl('', [Validators.required]);
  passwordFC: FormControl = new FormControl('', [Validators.required]);
  SellerAccountFC: FormControl = new FormControl('');
  showOrHideUserPhonenumberFC: FormControl = new FormControl('');
  showOrHideCompanyPhonenumberFC: FormControl = new FormControl('');
  isCompanyRegisteredFC: FormControl = new FormControl('');

  companyFCError: string;
  firstnameFCError: string;
  lastnameFCError: string;
  addressFCError: string;
  address2FCError: string;
  cityFCError: string;
  stateFCError: string;
  zipcodeFCError: string;
  userEmailFCError: string;
  companyEmailFCError: string;
  userPhonenumberFCError: string;
  companyPhonenumberFCError: string;
  usernameFCError: string;
  passwordFCError: string;
  SellerAccountFCError: string;
  selected = -1;
  isASeller = false;
  hideUserPhonenumber = true;
  hideCompanyPhonenumber = true;

  isUserASellerCheckbox = [
    { id: 0, label: 'Yes', isChecked: false },
    { id: 1, label: 'No', isChecked: true },
    { id: 2, label: 'May be later', isChecked: false }
  ]

  userChoiceOnPhonenumber = [
    { id: 0, label: 'Show', isChecked: false, note: `you have choosen to show your phone number, this will allow other to see your number when ever you post something or on your profile. ` },
    { id: 1, label: 'Hide', isChecked: true, note: `you have choosen to hide your phone number, we will not show your number on your future post or on your profile unless you change it through profile.` }
  ]
  companyChoiceOnPhonenumber = [
    { id: 0, label: 'Show', isChecked: false, note: `you have choosen to show your phone number, this will allow other to see your number when ever you post something or on your profile. ` },
    { id: 1, label: 'Hide', isChecked: true, note: `you have choosen to hide your phone number, we will not show your number on your future post or on your profile unless you change it through profile.` }
  ]


  constructor(private fb: FormBuilder,
    private routeLink: RouteTo,
    private registerService: RegisterService) {
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
      companyname: this.companyFC,
      firstname: this.firstnameFC,
      middlename: this.middlenameFC,
      lastname: this.lastnameFC,
      address: this.addressFC,
      address2: this.address2FC,
      city: this.cityFC,
      state: this.stateFC,
      zipcode: this.zipcodeFC,
      useremail: this.userEmailFC,
      companyemail: this.companyEmailFC,
      usercountryCode: this.userCountryCodeFC,
      userphonenumber: this.userPhonenumberFC,
      companycountryCode: this.companyCountryCodeFC,
      companyphonenumber: this.companyPhonenumberFC,
      isCompanyRegistered: this.isCompanyRegisteredFC,
      username: this.usernameFC,
      password: this.passwordFC,
      userIsSeller: this.SellerAccountFC,
      showOrHideUserPhonenumber: this.showOrHideUserPhonenumberFC,
      showOrHideCompanyPhonenumber: this.showOrHideCompanyPhonenumberFC
    })
  }

  onchangeSellerCheckbox(e: { target; value: string }, id) {
    const abc = e;
    console.log(this.selected);
    this.selected = id;
    //fisrtly uncheck all
    const allCheckbox = this.isUserASellerCheckbox.find(x => x.isChecked == true);
    if (allCheckbox.id !== id) {
      allCheckbox.isChecked = false;
      //check only which was selected
      this.isUserASellerCheckbox[id].isChecked = true;
    }
    const getCheckboxAnswer = this.isUserASellerCheckbox[id];
    this.SellerAccountFC.setValue(getCheckboxAnswer.label);
    this.isASeller = (getCheckboxAnswer.label.toLowerCase() == 'yes') ? true : false;
    this.registerForm.get('userIsSeller').patchValue(this.isASeller);
  }

  onchangeUserPhoneNumberCheckbox(e: { target; value: string }, id) {
    const abc = e;
    console.log(this.selected);
    this.selected = id;
    //fisrtly uncheck all
    const allCheckbox = this.userChoiceOnPhonenumber.find(x => x.isChecked == true);
    if (allCheckbox.id !== id) {
      allCheckbox.isChecked = false;
      //check only which was selected
      this.userChoiceOnPhonenumber[id].isChecked = true;
    }
    const getCheckboxAnswer = this.userChoiceOnPhonenumber[id];
    this.showOrHideUserPhonenumberFC.setValue(getCheckboxAnswer.label);
    this.hideUserPhonenumber = (getCheckboxAnswer.label.toLowerCase() == 'show') ? true : false;
    this.registerForm.get('showOrHideUserPhonenumber').patchValue(this.hideUserPhonenumber);
  }

  onchangeCompanyPhoneNumberCheckbox(e: { target; value: string }, id) {
    const abc = e;
    console.log(this.selected);
    this.selected = id;
    //fisrtly uncheck all
    const allCheckbox = this.companyChoiceOnPhonenumber.find(x => x.isChecked == true);
    if (allCheckbox.id !== id) {
      allCheckbox.isChecked = false;
      //check only which was selected
      this.companyChoiceOnPhonenumber[id].isChecked = true;
    }
    const getCheckboxAnswer = this.userChoiceOnPhonenumber[id];
    this.showOrHideCompanyPhonenumberFC.setValue(getCheckboxAnswer.label);
    this.hideCompanyPhonenumber = (getCheckboxAnswer.label.toLowerCase() == 'show') ? true : false;
    this.registerForm.get('showOrHideCompanyPhonenumber').patchValue(this.hideCompanyPhonenumber);
  }
  onchangeform(
    event: { target; value: string },
    formControlName: string
  ) {

    const val = event.target.value;
    this.registerForm.get(formControlName).patchValue(val);
    this.validateForm();
  }

  validateForm() {
    this.showFCError = true;
    if (this.registerForm.touched) {
      if (this.isASeller &&
        this.companyFC.invalid &&
        this.companyFC.hasError('required')
      ) {
        this.companyFCError = 'You must enter your company name';
        return;
      } else {
        this.companyFCError = '';
      }
      if (this.isASeller && this.companyEmailFC.invalid && this.companyEmailFC.hasError('email')) {
        this.companyEmailFCError = 'Not a valid email';
        return;
      } else {
        this.companyEmailFCError = '';
      }

      if (this.userEmailFC.invalid && this.userEmailFC.hasError('email')) {
        this.userEmailFCError = 'Not a valid email';
        return;
      } else {
        this.userEmailFCError = '';
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
      this.userEmailFCError = '';
      this.companyEmailFCError = '';
      this.firstnameFCError = '';
      this.lastnameFCError = '';
      this.companyFCError = '';
      this.addressFCError = '';
      this.address2FCError = '';
      this.userPhonenumberFCError = '';

    }
  }

  RouteTo(routeTo: string, routingEnabled = true): void {
    console.log('now routing: ', routeTo);
    this.routeLink.RouteTo(routeTo, routingEnabled);
  }

  registerNow() {
    this.validateForm();
    if (this.registerForm.valid) {
      console.log('FORM IS VALID');
      this.UserRegister.UserDetail.Email = this.registerForm.get('useremail').value;
      this.UserRegister.UserDetail.Firstname = this.registerForm.get('firstname').value;
      this.UserRegister.UserDetail.Lastname = this.registerForm.get('lastname').value;
      this.UserRegister.UserDetail.Middlename = this.registerForm.get('middlename').value;
      this.UserRegister.UserDetail.Password = this.registerForm.get('password').value;
      this.UserRegister.UserDetail.Username = this.registerForm.get('username').value;
      this.UserRegister.UserDetail.PhoneNumber = this.registerForm.get('userphonenumber').value;
      this.UserRegister.UserDetail.ShowPhonenumber = this.registerForm.get('showOrHideUserPhonenumber').value;
      this.UserRegister.UserDetail.IsUserSeller = this.registerForm.get('userIsSeller').value;
      this.UserRegister.CompanyDetails.CompanyEmailID = this.registerForm.get('companyemail').value;
      this.UserRegister.CompanyDetails.CompanyName = this.registerForm.get('companyname').value;
      this.UserRegister.CompanyDetails.Address = this.registerForm.get('address').value;
      this.UserRegister.CompanyDetails.PhoneNumber = this.registerForm.get('companyphonenumber').value;
      this.UserRegister.CompanyDetails.PhoneCountryCode = this.registerForm.get('companycountryCode').value;
      this.UserRegister.CompanyDetails.IsGOVRegisteredCompany = this.registerForm.get('showOrHideUserPhonenumber').value;
      this.UserRegister.CompanyDetails.ShowPhonenumber = this.registerForm.get('showOrHideCompanyPhonenumber').value;
      this.registerService.register(this.UserRegister).subscribe((item: RegisterResponse) => {
        alert(item.status);
      });
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
