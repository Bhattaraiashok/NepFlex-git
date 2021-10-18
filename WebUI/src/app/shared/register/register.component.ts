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

  //three forms lined up
  userRegisterForm: FormGroup;
  companyRegisterForm: FormGroup;
  agreementRegisterForm: FormGroup;

  //three button types
  submitStepButton: ButtonProperties[] = new Array();
  nextStepButton: ButtonProperties[] = new Array();
  registerButtton: ButtonProperties[] = new Array();

  showFCError: boolean = false;
  hide = true;
  passwordType = 'password';
  matcher = new MyErrorStateMatcher();
  showAlertMessages: boolean = false;
  messageAlerts: AlertMessageProperties = new AlertMessageProperties();
  userIsRegistered: boolean = false;
  spinnerActivated: boolean = false;
  disableCurrentButton: boolean = true;


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
  SellerAccountFC: FormControl = new FormControl('', [Validators.required]);
  showOrHideUserPhonenumberFC: FormControl = new FormControl('');
  showOrHideCompanyPhonenumberFC: FormControl = new FormControl('');
  isCompanyRegisteredFC: FormControl = new FormControl('');
  //user agreement
  isUserAgreementCheckedFC: FormControl = new FormControl('', [Validators.required]);

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

  //flow of Reg
  userRegCurrentFlow: string;
  usernameAndPassword_Flow: string = "UandPFlow";
  userDetail_Flow: string = "userDetail_Flow";
  askIfSeller_flow: string = "askIfSeller_flow";
  companyDetail_flow: string = "companyDetail_flow";
  userAgreement_flow: string = "userAgreement_flow;"

  currentActiveStep: number = 1;

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
    this.createUserForm();
  }

  ngOnInit(): void {
    this.allButtons();
    this.registrationFlow(1);
  }

  allButtons() {
    this.submitStepButton = [
      {
        buttonId: 1,
        buttonLabel: 'Submit',
        isDisable: this.disableCurrentButton,
        tooltip: ((this.userRegCurrentFlow == this.usernameAndPassword_Flow) && this.disableCurrentButton) ? "Please, complete above fields to submit" : " Click here to submit.",
        hasPopUp: false,
        buttonRoute: '',
        canRoute: false,
        HasDropDown: false,
        parentEmit: true
      }];

    this.nextStepButton = [{
      buttonId: 2,
      buttonLabel: 'Next',
      isDisable: this.disableCurrentButton,
      tooltip: (this.disableCurrentButton) ? "Please, complete above fields to continue" : " Click here to continue.",
      hasPopUp: false,
      buttonRoute: '',
      canRoute: false,
      HasDropDown: false,
      parentEmit: true
    }];

    this.registerButtton = [{
      buttonId: 3,
      buttonLabel: 'Register Now',
      isDisable: this.disableCurrentButton,
      tooltip: (this.disableCurrentButton) ? "Please, complete above fields to enable registration" : " Click here to register.",
      hasPopUp: false,
      buttonRoute: '',
      canRoute: false,
      HasDropDown: false,
      parentEmit: true
    }];
  }

  registrationFlow(steps: number) {
    console.log('current steps: ', steps);
    if (steps == 1) {
      this.userRegCurrentFlow = this.usernameAndPassword_Flow;
    } else if (steps == 2) {
      this.userRegCurrentFlow = this.userDetail_Flow;
    } else if (steps == 3) {
      this.userRegCurrentFlow = this.askIfSeller_flow;
    } else if (steps == 4) {
      this.userRegCurrentFlow = this.companyDetail_flow;
      this.createCompanyForm();
    } else if (steps == 5) {
      this.userRegCurrentFlow = this.userAgreement_flow;
    } else {
      this.userRegCurrentFlow = this.usernameAndPassword_Flow;
      this.createAgreementForm();
    }
    console.log('steps shown: ', this.userRegCurrentFlow);
  }

  onchangeform(event: { target; value: string }, formControlName: string) {
    const val = event.target.value;
    if (this.userRegCurrentFlow == this.usernameAndPassword_Flow) {
      this.userRegisterForm.get(formControlName).patchValue(val);
    } else if (this.userRegCurrentFlow == this.userDetail_Flow) {
      this.userRegisterForm.get(formControlName).patchValue(val);
    } else if (this.userRegCurrentFlow == this.askIfSeller_flow) {
      this.userRegisterForm.get(formControlName).patchValue(val);
    } else if (this.userRegCurrentFlow == this.companyDetail_flow) {
      this.companyRegisterForm.get(formControlName).patchValue(val);
    } else if (this.userRegCurrentFlow == this.userAgreement_flow) {
      this.agreementRegisterForm.get(formControlName).patchValue(val);
    }

    this.validateForm();
    this.disableCurrentButton = this.showFCError;
    this.allButtons();
  }

  nextStep() {
    this.currentActiveStep++;
    this.registrationFlow(this.currentActiveStep);
  }

  submit() {
    //let user submit uname and password first
    this.registerTheFlow();
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

  createUserForm() {
    this.userRegisterForm = this.fb.group({
      firstname: this.firstnameFC,
      middlename: this.middlenameFC,
      lastname: this.lastnameFC,
      useremail: this.userEmailFC,
      usercountryCode: this.userCountryCodeFC,
      userphonenumber: this.userPhonenumberFC,
      username: this.usernameFC,
      password: this.passwordFC,
      userIsSeller: this.SellerAccountFC,
      showOrHideUserPhonenumber: this.showOrHideUserPhonenumberFC
    });
  }
  createCompanyForm() {
    //if(this.isASeller)
    this.companyRegisterForm = this.fb.group({
      companyname: this.companyFC,
      address: this.addressFC,
      address2: this.address2FC,
      city: this.cityFC,
      state: this.stateFC,
      zipcode: this.zipcodeFC,
      companyemail: this.companyEmailFC,
      companycountryCode: this.companyCountryCodeFC,
      companyphonenumber: this.companyPhonenumberFC,
      isCompanyRegistered: this.isCompanyRegisteredFC,
      showOrHideCompanyPhonenumber: this.showOrHideCompanyPhonenumberFC,
    });
  }
  createAgreementForm() {
    this.agreementRegisterForm = this.fb.group({
      isUserAgreementChecked: this.isUserAgreementCheckedFC
    });
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
    this.userRegisterForm.get('userIsSeller').patchValue(this.isASeller);
    this.validateForm();
    this.disableCurrentButton = this.showFCError;
    if (this.isASeller) {
      this.allButtons();
    }
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
    this.userRegisterForm.get('showOrHideUserPhonenumber').patchValue(this.hideUserPhonenumber);
    this.validateForm();
    this.disableCurrentButton = this.showFCError;
    this.allButtons();
  }
  onChange_ToUserAgreementCheckbox(e: { target; value: string }, id) {
    this.selected = id;
    this.userAgreementCheckbox[id].isChecked = !this.userAgreementCheckbox[id].isChecked;
    this.isUserAgreementCheckedFC.setValue(this.userAgreementCheckbox[id].isChecked);
    this.agreementRegisterForm.get('isUserAgreementChecked').patchValue(this.isUserAgreementCheckedFC);
    this.validateForm();
    this.disableCurrentButton = this.showFCError;
    this.allButtons();
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
    this.companyRegisterForm.get('showOrHideCompanyPhonenumber').patchValue(this.hideCompanyPhonenumber);
    this.validateForm();
    this.disableCurrentButton = this.showFCError;
    this.allButtons();
  }

  smallSpinner() {
    this.spinnerActivated = !this.spinnerActivated;
    console.log('turnOnSmallLoader :  ', this.spinnerActivated);
  }

  validateForm() {
    this.showFCError = true;
    if (this.userRegisterForm.dirty) {
      //check intital step 1 flow:
      if (this.userRegCurrentFlow == this.usernameAndPassword_Flow && this.currentActiveStep == 1) {
        if (this.userEmailFC.invalid && this.userEmailFC.hasError('email')) {
          this.userEmailFCError = 'Not a valid email';
          return;
        } else if (this.usernameFC.invalid && this.usernameFC.hasError('required')) {
          this.usernameFCError = 'You must create a username';
          return;
        } else if (this.passwordFC.invalid && this.passwordFC.hasError('required')) {
          this.passwordFCError = 'You must create a new password';
          return;
        } else {
          this.showFCError = false;
          this.userEmailFCError = '';
          this.usernameFCError = '';
          this.passwordFCError = '';
        }
      }

      //check intital step 2 flow:
      if (this.userRegCurrentFlow == this.userDetail_Flow && this.currentActiveStep == 2) {
        if (this.firstnameFC.invalid && this.firstnameFC.hasError('required')) {
          this.firstnameFCError = 'You must enter your firstname';
          return;
        } else if (this.lastnameFC.invalid && this.lastnameFC.hasError('required')) {
          this.lastnameFCError = 'You must enter your lastname';
          return;
        } else {
          this.showFCError = false;
          this.firstnameFCError = '';
          this.lastnameFCError = '';
        }
      }

      //check step 3 -ask is seller flow:
      if (this.userRegCurrentFlow == this.askIfSeller_flow && this.currentActiveStep == 3) {
        if (this.SellerAccountFC.invalid && this.SellerAccountFC.hasError('required')) {
          this.SellerAccountFCError = 'you must choose one for now. if you are not sure, please choose "may be later" option.'
        } else {
          this.showFCError = false;
          this.SellerAccountFCError = '';
        }
      }

      if (this.companyRegisterForm) {
        if (this.companyRegisterForm.touched && this.currentActiveStep == 4) {
          //check step 4 - check company detail if isSeller:
          if (this.isASeller && this.userRegCurrentFlow == this.companyDetail_flow) {
            if (this.companyFC.invalid && this.companyFC.hasError('required')) {
              this.companyFCError = 'You must enter your company name';
              return;
            } else if (this.companyEmailFC.invalid && this.companyEmailFC.hasError('email')) {
              this.companyEmailFCError = 'Not a valid email';
              return;
            } else {
              this.companyFCError = '';
              this.companyEmailFCError = '';
            }
          }
        }
      }

      if (this.agreementRegisterForm) {
        if (this.agreementRegisterForm.touched && this.currentActiveStep == 5) {
          //check step 5 -final flow check agreement:
          if (this.userRegCurrentFlow == this.userAgreement_flow) {
            if (this.isUserAgreementCheckedFC.invalid && this.isUserAgreementCheckedFC.hasError('required')
            ) {
              this.isUserAgreementCheckedFCError = this.userAgreementCheckbox[0].note;
              return;
            } else {
              this.showFCError = false;
              this.isUserAgreementCheckedFCError = '';
            }
          }
        }
      }
    }
  }

  RouteTo(routeTo: string, routingEnabled = true): void {
    console.log('now routing: ', routeTo);
    this.routeLink.RouteTo(routeTo, routingEnabled);
  }

  registerTheFlow() {
    this.smallSpinner();
    this.validateForm();
    console.log(this.userRegisterForm, this.companyRegisterForm, this.agreementRegisterForm)
    if (!this.showFCError) {
      this.mappings();
      console.log('FORM IS VALID');
      const secondLayerValidation = this.mappingValidation();
      if (secondLayerValidation === true) {
        this.serviceCall();
      } else {
        this.smallSpinner();
      }
    } else {
      this.smallSpinner();
    }
  }

  serviceCall() {
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
  }

  call_MessageAlertComponent(alertType, alertMsg) {
    this.showAlertMessages = true;
    this.messageAlerts.alertType = alertType;
    this.messageAlerts.alertMsg = alertMsg;
  }

  mappings() {
    //let's map first step and rest seperatly
    if (this.userRegCurrentFlow == this.usernameAndPassword_Flow && this.currentActiveStep == 1) {
      this.UserRegister.UserDetail.UserEmail = this.userRegisterForm.get('useremail').value;
      this.UserRegister.UserDetail.PSWDHASH = this.userRegisterForm.get('password').value;
      this.UserRegister.UserDetail.Username = this.userRegisterForm.get('username').value;
    } else {
      this.UserRegister.UserDetail.Firstname = this.userRegisterForm.get('firstname').value;
      this.UserRegister.UserDetail.Lastname = this.userRegisterForm.get('lastname').value;
      this.UserRegister.UserDetail.Middlename = this.userRegisterForm.get('middlename').value;
      this.UserRegister.UserDetail.PhoneCountryCode = this.userRegisterForm.get('usercountryCode').value;
      this.UserRegister.UserDetail.PhoneNumber = this.userRegisterForm.get('userphonenumber').value;
      this.UserRegister.UserDetail.ShowPhonenumber = this.userRegisterForm.get('showOrHideUserPhonenumber').value;
      this.UserRegister.UserDetail.IsUserSeller = this.userRegisterForm.get('userIsSeller').value ? this.userRegisterForm.get('userIsSeller').value : false;
      this.UserRegister.CompanyDetails.CompanyEmailID = this.companyRegisterForm.get('companyemail').value;
      this.UserRegister.CompanyDetails.CompanyName = this.companyRegisterForm.get('companyname').value;
      this.UserRegister.CompanyDetails.Address = this.companyRegisterForm.get('address').value;
      this.UserRegister.CompanyDetails.PhoneNumber = this.companyRegisterForm.get('companyphonenumber').value;
      this.UserRegister.CompanyDetails.PhoneCountryCode = this.companyRegisterForm.get('companycountryCode').value;
      this.UserRegister.CompanyDetails.IsGOVRegisteredCompany = true; //need actual mapping here.
      this.UserRegister.CompanyDetails.ShowPhonenumber = this.companyRegisterForm.get('showOrHideCompanyPhonenumber').value;
      //agreement
      this.UserRegister.UserDetail.IsUserAgreementChecked = this.agreementRegisterForm.get('isUserAgreementChecked').value;
    }
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
    let result = false;
    if (this.userRegCurrentFlow == this.usernameAndPassword_Flow && this.currentActiveStep == 1) {
      result = (
        (this.UserRegister.UserDetail.UserEmail !== undefined && this.UserRegister.UserDetail.UserEmail !== null && this.UserRegister.UserDetail.UserEmail !== '')
        && (this.UserRegister.UserDetail.PSWDHASH !== undefined && this.UserRegister.UserDetail.PSWDHASH !== null && this.UserRegister.UserDetail.PSWDHASH !== '')
        && (this.UserRegister.UserDetail.Username !== undefined && this.UserRegister.UserDetail.Username !== null && this.UserRegister.UserDetail.Username !== ''))
    } else {
      result = (
        (this.UserRegister.UserDetail.Firstname !== undefined && this.UserRegister.UserDetail.Firstname !== null && this.UserRegister.UserDetail.Firstname !== '')
        && (this.UserRegister.UserDetail.Lastname !== undefined && this.UserRegister.UserDetail.Lastname !== null && this.UserRegister.UserDetail.Lastname !== '')
        && (this.UserRegister.UserDetail.PSWDHASH !== undefined && this.UserRegister.UserDetail.PSWDHASH !== null && this.UserRegister.UserDetail.PSWDHASH !== '')
        && (this.UserRegister.UserDetail.Username !== undefined && this.UserRegister.UserDetail.Username !== null && this.UserRegister.UserDetail.Username !== '')
        && (this.UserRegister.UserDetail.PhoneCountryCode !== undefined && this.UserRegister.UserDetail.PhoneCountryCode !== null && this.UserRegister.UserDetail.PhoneCountryCode !== '')
        && (this.UserRegister.UserDetail.PhoneNumber !== undefined && this.UserRegister.UserDetail.PhoneNumber !== null && this.UserRegister.UserDetail.PhoneNumber !== '')
        && (this.UserRegister.UserDetail.IsUserAgreementChecked !== undefined && this.UserRegister.UserDetail.IsUserAgreementChecked !== null)
        && (this.UserRegister.UserDetail.IsUserSeller !== undefined && this.UserRegister.UserDetail.IsUserSeller !== null)
        && (this.UserRegister.UserDetail.ShowPhonenumber !== undefined && this.UserRegister.UserDetail.ShowPhonenumber !== null));
    }
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
