import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { RouteTo } from '../interfaces/local-router';
import { ButtonProperties } from '../ResourceModels/ButtonProperties';
import { RegisterService } from "app/shared/services/register.service";
import { UserRegister } from "app/shared/ResourceModels/registerModel";
import { AlertMessageProperties } from "app/shared/ResourceModels/AlertMessages";
import { ResponseObjects } from "app/shared/ResourceModels/ResponseStatus";
import { LoginComponent } from "app/shared/login/login.component";
import { DesktopHeaderComponent } from "app/desktop/controls/desktop-header/desktop-header.component";
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Output() PopUpName: string;

  UserRegister: UserRegister = new UserRegister();
  registerResponse: ResponseObjects;
  registerForm: FormGroup;
  detailButttons: ButtonProperties[] = new Array();
  showFCError: boolean = false;
  hide = true;
  passwordType = 'password';
  matcher = new MyErrorStateMatcher();
  showAlertMessages: boolean = false;
  messageAlerts: AlertMessageProperties = new AlertMessageProperties();
  userIsRegistered: boolean = false;
  spinnerActivated: boolean = false;


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
  companyEmailFC: FormControl = new FormControl('', [Validators.email]);
  userCountryCodeFC: FormControl = new FormControl('', [Validators.pattern(/\d{3}$/)]);
  userPhonenumberFC: FormControl = new FormControl('', [Validators.pattern(/^\(\d{3}\)\s\d{3}-\d{4}$/)]);
  companyCountryCodeFC: FormControl = new FormControl('', [Validators.pattern(/\d{3}$/)]);
  companyPhonenumberFC: FormControl = new FormControl('', [Validators.pattern(/^\(\d{3}\)\s\d{3}-\d{4}$/)]);
  usernameFC: FormControl = new FormControl('', [Validators.required]);
  passwordFC: FormControl = new FormControl('', [Validators.required]);
  SellerAccountFC: FormControl = new FormControl('');
  showOrHideUserPhonenumberFC: FormControl = new FormControl('');
  showOrHideCompanyPhonenumberFC: FormControl = new FormControl('');
  isCompanyRegisteredFC: FormControl = new FormControl('');
  //user agreement
  isUserAgreementCheckedFC: FormControl = new FormControl('');

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
  isUserAgreementCheckedFCError: string;
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
  userAgreementCheckbox = [
    { id: 0, label: 'Yes', isChecked: false, note: `You must agree to terms and conditions before submitting.` },
  ]


  constructor(private fb: FormBuilder,
    private routeLink: RouteTo,
    private registerService: RegisterService,
    private modalService: NgbActiveModal) {
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
        isDisable: !this.registerForm.valid,
        tooltip: !this.registerForm.valid ? "Please, complete above fields to enable registration" : " Click here to register.",
        hasPopUp: false,
        buttonRoute: '',
        canRoute: false,
        HasDropDown: false,
        popUpName: 'clickSendEmailButton',
        parentEmit: true
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
      showOrHideCompanyPhonenumber: this.showOrHideCompanyPhonenumberFC,
      isUserAgreementChecked: this.isUserAgreementCheckedFC
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
  onChange_ToUserAgreementCheckbox(e: { target; value: string }, id) {
    this.selected = id;
    this.userAgreementCheckbox[id].isChecked = true;
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
  
  smallSpinner() {
    this.spinnerActivated = !this.spinnerActivated;
    console.log('turnOnSmallLoader :  ', this.spinnerActivated);
  }

  validateForm() {
    this.showFCError = true;
    if (this.registerForm.touched) {
      if (
        this.isUserAgreementCheckedFC.invalid &&
        this.isUserAgreementCheckedFC.hasError('required')
      ) {
        this.isUserAgreementCheckedFCError = this.userAgreementCheckbox[0].note;
        return;
      } else {
        this.isUserAgreementCheckedFCError = '';
      }

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
      this.isUserAgreementCheckedFCError = '';
      this.detailButttons.find(x => x.buttonLabel.toLocaleLowerCase() == 'register now').isDisable = false;
    }
  }

  RouteTo(routeTo: string, routingEnabled = true): void {
    console.log('now routing: ', routeTo);
    this.routeLink.RouteTo(routeTo, routingEnabled);
  }

  registerNow() {
    this.smallSpinner();
    this.validateForm();
    if (this.registerForm.valid) {
      this.mappings();
      console.log('FORM IS VALID');
      const secondLayerValidation = this.mappingValidation();
      if (secondLayerValidation === true) {
        this.registerService.register(this.UserRegister).subscribe((item: ResponseObjects) => {
          if (item.isSuccess === true) {
            console.log(item.strMessage);
            this.call_MessageAlertComponent('Success', item.strMessage[0]);
            this.userIsRegistered == true;
             this.modalService.close('close');
          } else {
            this.smallSpinner();
            console.log(item.strMessage);
            this.call_MessageAlertComponent('Error', item.strMessage[0]);
          }
        });
      } else {
        this.smallSpinner();
      }
    } else {
      this.smallSpinner();
    }
  }

  call_MessageAlertComponent(alertType, alertMsg) {
    this.showAlertMessages = true;
    this.messageAlerts.alertType = alertType;
    this.messageAlerts.alertMsg = alertMsg;
  }

  mappings() {
    this.UserRegister.UserDetail.UserEmail = this.registerForm.get('useremail').value;
    this.UserRegister.UserDetail.Firstname = this.registerForm.get('firstname').value;
    this.UserRegister.UserDetail.Lastname = this.registerForm.get('lastname').value;
    this.UserRegister.UserDetail.Middlename = this.registerForm.get('middlename').value;
    this.UserRegister.UserDetail.PSWDHASH = this.registerForm.get('password').value;
    this.UserRegister.UserDetail.Username = this.registerForm.get('username').value;
    this.UserRegister.UserDetail.PhoneCountryCode = this.registerForm.get('usercountryCode').value;
    this.UserRegister.UserDetail.PhoneNumber = this.registerForm.get('userphonenumber').value;
    this.UserRegister.UserDetail.ShowPhonenumber = this.registerForm.get('showOrHideUserPhonenumber').value;
    this.UserRegister.UserDetail.IsUserSeller = this.registerForm.get('userIsSeller').value ? this.registerForm.get('userIsSeller').value : false;
    this.UserRegister.CompanyDetails.CompanyEmailID = this.registerForm.get('companyemail').value;
    this.UserRegister.CompanyDetails.CompanyName = this.registerForm.get('companyname').value;
    this.UserRegister.CompanyDetails.Address = this.registerForm.get('address').value;
    this.UserRegister.CompanyDetails.PhoneNumber = this.registerForm.get('companyphonenumber').value;
    this.UserRegister.CompanyDetails.PhoneCountryCode = this.registerForm.get('companycountryCode').value;
    this.UserRegister.CompanyDetails.IsGOVRegisteredCompany = this.registerForm.get('showOrHideUserPhonenumber').value;
    this.UserRegister.CompanyDetails.ShowPhonenumber = this.registerForm.get('showOrHideCompanyPhonenumber').value;
    //agreement
    this.UserRegister.UserDetail.IsUserAgreementChecked = this.registerForm.get('isUserAgreementChecked').value;
  }

  mappingValidation(): boolean {
    if (this.userCheck() === true && this.isASeller === false) {
      return true;
    } else if (this.userCheck() === true && this.isASeller === true && this.companyCheck() === true) {
      return true;
    } else {
      return false;
    }
  };

  userCheck(): boolean {
    const result = (
      (this.UserRegister.UserDetail.UserEmail !== undefined && this.UserRegister.UserDetail.UserEmail !== null && this.UserRegister.UserDetail.UserEmail !== '')
      && (this.UserRegister.UserDetail.Firstname !== undefined && this.UserRegister.UserDetail.Firstname !== null && this.UserRegister.UserDetail.Firstname !== '')
      && (this.UserRegister.UserDetail.Lastname !== undefined && this.UserRegister.UserDetail.Lastname !== null && this.UserRegister.UserDetail.Lastname !== '')
      && (this.UserRegister.UserDetail.PSWDHASH !== undefined && this.UserRegister.UserDetail.PSWDHASH !== null && this.UserRegister.UserDetail.PSWDHASH !== '')
      && (this.UserRegister.UserDetail.Username !== undefined && this.UserRegister.UserDetail.Username !== null && this.UserRegister.UserDetail.Username !== '')
      && (this.UserRegister.UserDetail.PhoneCountryCode !== undefined && this.UserRegister.UserDetail.PhoneCountryCode !== null && this.UserRegister.UserDetail.PhoneCountryCode !== '')
      && (this.UserRegister.UserDetail.PhoneNumber !== undefined && this.UserRegister.UserDetail.PhoneNumber !== null && this.UserRegister.UserDetail.PhoneNumber !== '')
      && (this.UserRegister.UserDetail.IsUserAgreementChecked !== undefined && this.UserRegister.UserDetail.IsUserAgreementChecked !== null)
      && (this.UserRegister.UserDetail.IsUserSeller !== undefined && this.UserRegister.UserDetail.IsUserSeller !== null)
      && (this.UserRegister.UserDetail.ShowPhonenumber !== undefined && this.UserRegister.UserDetail.ShowPhonenumber !== null));
    return result;
  }
  companyCheck(): boolean {
    const result = (
      (this.UserRegister.CompanyDetails.CompanyEmailID !== undefined && this.UserRegister.CompanyDetails.CompanyEmailID !== null && this.UserRegister.CompanyDetails.CompanyEmailID == '')
      && (this.UserRegister.CompanyDetails.CompanyName !== undefined && this.UserRegister.CompanyDetails.CompanyName !== null && this.UserRegister.CompanyDetails.CompanyName == '')
      && (this.UserRegister.CompanyDetails.Address !== undefined && this.UserRegister.CompanyDetails.Address !== null && this.UserRegister.CompanyDetails.Address == '')
      && (this.UserRegister.CompanyDetails.PhoneNumber !== undefined && this.UserRegister.CompanyDetails.PhoneNumber !== null && this.UserRegister.CompanyDetails.PhoneNumber == '')
      && (this.UserRegister.CompanyDetails.PhoneCountryCode !== undefined && this.UserRegister.CompanyDetails.PhoneCountryCode !== null && this.UserRegister.CompanyDetails.PhoneCountryCode == '')
      && (this.UserRegister.CompanyDetails.IsGOVRegisteredCompany !== undefined && this.UserRegister.CompanyDetails.IsGOVRegisteredCompany !== null)
      && (this.UserRegister.CompanyDetails.ShowPhonenumber !== undefined && this.UserRegister.CompanyDetails.ShowPhonenumber !== null));
    return result;
  }
}
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
