import { Component, OnInit } from '@angular/core';
import { IDeactivateComponent } from "app/shared/guards/can-deactivate-guard.service";
import { UserRegister } from "app/shared/ResourceModels/registerModel";
import { ResponseObjects } from "app/shared/ResourceModels/ResponseStatus";
import { CONSTList, AlertMessageProperties } from "app/shared/ResourceModels/AlertMessages";
import { FormGroup, FormControl, Validators, FormBuilder } from "@angular/forms";
import { ButtonProperties } from "app/shared/ResourceModels/ButtonProperties";
import { SpinnerService } from "app/shared/services/control-services/spinner.service";
import { RegisterService } from "app/shared/services/register.service";
import { NotificationService } from "app/shared/services/control-services/notification.service";

@Component({
  selector: 'app-standard-user-registration',
  templateUrl: './standard-user-registration.component.html',
  styleUrls: ['./standard-user-registration.component.scss']
})
export class StandardUserRegistrationComponent implements OnInit, IDeactivateComponent {
  canExit() {
    // if (this.userRegisterForm.dirty) {
    //   const res = window.confirm('You have not saved changes yet. Are you sure you want to cancel?');
    //   return res;
    // }
    return true;
  }

  submitStepButton: ButtonProperties[] = new Array();
  backStepButton: ButtonProperties[] = new Array();
  nextStepButton: ButtonProperties[] = new Array();
  registerButtton: ButtonProperties[] = new Array();
  syncingButton: ButtonProperties[] = new Array();

  UserRegister: UserRegister = new UserRegister();
  registerResponse: ResponseObjects;
  CONSTList: CONSTList = new CONSTList();
  standardUserRegisterForm: FormGroup;

  userRegCurrentFlow: string;
  usernameAndPassword_Flow: string = "UandPFlow";
  userDetail_Flow: string = "userDetail_Flow";
  userImage_Flow: string = "userImage_Flow";
  finalStep_Flow: string = "finalStep_Flow";

  currentActiveStep: number = 1;
  isBackClicked: boolean = false;

  pesonalDetailFilled: boolean = false;
  imageUploded: boolean = false;
  askUserToSetUpProfile: boolean = false;


  hide = true;
  passwordType = 'password';
  selected = -1;

  currentStatus: string = null;

  showAlertMessages: boolean = false;
  messageAlerts: AlertMessageProperties = new AlertMessageProperties();
  spinnerActivated: boolean = false;
  disableCurrentButton: boolean = true;
  disableBackButton: boolean = false;
  showSignAgreement: boolean = false;

  userEmailFCError: string;
  usernameFCError: string;
  passwordFCError: string;
  confirmPasswordFCError: string;
  isUserAgreementCheckedFCError: string;

  firstnameFCError: string;
  lastnameFCError: string;
  hideUserPhonenumber = true;
  userPhonenumberFCError: string;
  profilephotoFCSrror: string;

  userEmailFC: FormControl = new FormControl('', [Validators.required, Validators.email]);
  usernameFC: FormControl = new FormControl('', [Validators.required]);
  passwordFC: FormControl = new FormControl('', [Validators.required]);
  confirmPasswordFC: FormControl = new FormControl('', [Validators.required]);

  firstnameFC: FormControl = new FormControl('', [Validators.required]);
  middlenameFC: FormControl = new FormControl('');
  lastnameFC: FormControl = new FormControl('', [Validators.required]);

  userCountryCodeFC: FormControl = new FormControl('');
  userPhonenumberFC: FormControl = new FormControl('');
  showOrHideUserPhonenumberFC: FormControl = new FormControl('');

  profilephotoFC: FormControl = new FormControl('');


  //user agreement
  isUserAgreementCheckedFC: FormControl = new FormControl('');

  showFCError: boolean = false;

  current_fs = null;
  next_fs = null;
  previous_fs = null; //fieldsets
  opacity = null;
  current: number = 1;
  steps = document.getElementsByTagName("fieldset").length;
  constructor(private fb: FormBuilder,
    private registerService: RegisterService,
    private spinnerService: SpinnerService,
    private notificationService: NotificationService, ) { }

  ngOnInit(): void {
    this.createUserForm();
    this.allButtons();
    // this.registrationFlow(1, null);
  }

  get setProgressBar() {
    return this.current;
  }
  set setProgressBar(val) {
    this.current = val;
  }

  createUserForm() {
    this.standardUserRegisterForm = this.fb.group({
      useremail: this.userEmailFC,
      username: this.usernameFC,
      password: this.passwordFC,
      confirmpassword: this.confirmPasswordFC,
      isUserAgreementChecked: this.isUserAgreementCheckedFC,
      firstname: this.firstnameFC,
      middlename: this.middlenameFC,
      lastname: this.lastnameFC,
      usercountryCode: this.userCountryCodeFC,
      showOrHideUserPhonenumber: this.showOrHideUserPhonenumberFC,
      userphonenumber: this.userPhonenumberFC,
      profilephoto: this.profilephotoFC
    })
  }

  onBlurVision(event: { target; value: string },
    formControlName: string) {
    const val = event.target.value;
    this.standardUserRegisterForm.get(formControlName).patchValue(val);
    console.log("BLURREDDDDDDDDD: ", val);
    this.validateForm();
    this.formAndButtons();
  }

  onchangeform(event: { target; value: string },
    formControlName: string) {
    const val = event.target.value;
    this.standardUserRegisterForm.get(formControlName).patchValue(val);
  }

  formAndButtons() {
    this.disableCurrentButton = this.showFCError;
    if ((this.currentActiveStep !== 1 && this.currentActiveStep !== 2)) {
      this.disableBackButton = false;
    }

    if (this.currentActiveStep == 1 && this.formInitialUserCheck()) {
      this.showSignAgreement = true;
    }
    this.allButtons();
  }

  formInitialUserCheck(): boolean {
    const result = false;
    if ((this.userEmailFC.valid && !this.userEmailFC.hasError('email') && this.userEmailFC.value !== undefined && this.userEmailFC.value !== null && this.userEmailFC.value !== '')
      && (this.usernameFC.valid && !this.usernameFC.hasError('required') && this.usernameFC.value !== undefined && this.usernameFC.value !== null && this.usernameFC.value !== '')
      && (this.passwordFC.valid && !this.passwordFC.hasError('required') && this.passwordFC.value !== undefined && this.passwordFC.value !== null && this.passwordFC.value !== '')
      // && (this.isUserAgreementCheckedFC !== undefined && this.isUserAgreementCheckedFC !== null)
      && (this.confirmPasswordFC.value == this.passwordFC.value && this.confirmPasswordFC.valid && !this.confirmPasswordFC.hasError('required')
        && this.confirmPasswordFC.value !== undefined && this.confirmPasswordFC.value !== null && this.confirmPasswordFC.value !== '')) {
      return true;
    } else {
      return false;
    }
  }

  nextStep(e: Event) {
    this.formAndButtons();
    if (!this.showFCError) {
      this.currentActiveStep++;
      // this.registrationFlow(this.currentActiveStep, 'next');
    }
  }

  submit(e: Event) {
    this.formAndButtons();
    //let user submit uname and password first
    this.registerTheFlow(e);
  }

  // registrationFlow(steps: number, btnRecognize: string) {
  //   console.log('current steps: ', steps);
  //   if (steps == 1) {
  //     this.userRegCurrentFlow = this.usernameAndPassword_Flow;
  //   } else if (steps == 2) {
  //     this.userRegCurrentFlow = this.userDetail_Flow;
  //     this.askUserToSetUpProfile = true;
  //   } else if (steps == 3) {
  //     this.userRegCurrentFlow = this.userImage_Flow;
  //   }else if (steps == 4) {
  //     this.userRegCurrentFlow = this.userImage_Flow;
  //   }
  //   else {
  //     console.log('route to profile here ', this.userRegCurrentFlow);
  //   }

  //   if (btnRecognize == 'back') {
  //     this.isBackClicked = true;
  //   }
  //   console.log('steps shown: ', this.userRegCurrentFlow);
  //}


  registerTheFlow(e: Event) {
    this.smallSpinner();
    this.formAndButtons();
    console.log(this.standardUserRegisterForm)
    if (!this.showFCError) {
      this.mappings();
      console.log('FORM IS VALID');
      const secondLayerValidation = this.formInitialUserCheck();
      if (secondLayerValidation === true) {
        this.registerService.register(this.UserRegister).subscribe((item: ResponseObjects) => {
          if (item.isSuccess === true) {
            console.log(item.strMessage);
            this.call_MessageAlertComponent(this.CONSTList.success, item.strMessage[0]);
            this.nextStep(e);
            //this.modalService.close('close'); --no close here on first step
          } else {
            this.smallSpinner();
            console.log(item.strMessage);
            this.call_MessageAlertComponent(this.CONSTList.error, item.strMessage[0]);
          }
        });
      } else {
        this.smallSpinner();
      }
    } else {
      this.smallSpinner();
    }
  }

  updateOnBlur(event: any,
    formControlName: string, formval: string | boolean = '') {
    this.currentStatus = null;
    this.currentStatus = 'Syncing...';
    console.log("BLURRR");

    let val = null;

    if (event.target !== null && event.target !== undefined) {
      val = event.target.value
    } else {
      val = formval;
    }

    if (val != null && val != "") {
      this.standardUserRegisterForm.get(formControlName).patchValue(val);
      this.UserRegister.FieldUpdateRequest = formControlName;
      this.mappings();
      this.registerService.update(this.UserRegister).subscribe((item: ResponseObjects) => {
        if (item.isSuccess === true) {
          console.log(item.strMessage);
          this.currentStatus = formControlName + ' Saved';
          this.smallSpinner();
          this.call_MessageAlertComponent(this.CONSTList.success, item.strMessage[0]);
        } else {
          this.smallSpinner();
          console.log(item.strMessage);
          this.currentStatus = null;
          this.call_MessageAlertComponent(this.CONSTList.error, item.strMessage[0]);
        }
      });
    }
  }

  smallSpinner() {
    this.spinnerActivated = !this.spinnerActivated;
    console.log('turnOnSmallLoader :  ', this.spinnerActivated);
    if (this.spinnerActivated) {
      this.spinnerActivated = this.spinnerService.showSpinner_Disabled_sm();
    } else {
      this.spinnerActivated = this.spinnerService.disableSpinner_Disabled_sm();
    }
    this.allButtons();
  }

  call_MessageAlertComponent(alertType, alertMsg) {
    this.showAlertMessages = true;
    this.messageAlerts.alertType = alertType;
    this.messageAlerts.alertMsg = alertMsg;
    this.messageAlerts.alertBtnLabel = "OK";
    this.messageAlerts.showButton = this.showAlertMessages;
    this.notificationService.showNotification(this.messageAlerts);
  }

  onChange_ToUserAgreementCheckbox(e: { target; value: string }, id) {
    this.selected = id;
    this.userAgreementCheckbox[id].isChecked = !this.userAgreementCheckbox[id].isChecked;
    this.isUserAgreementCheckedFC.setValue(this.userAgreementCheckbox[id].isChecked);

    this.standardUserRegisterForm.get('isUserAgreementChecked').patchValue(this.userAgreementCheckbox[id].isChecked);
    this.validateForm();
    this.formAndButtons();
  }

  onchangeUserPhoneNumberCheckbox(e: KeyboardEvent, id) {
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
    this.standardUserRegisterForm.get('showOrHideUserPhonenumber').patchValue(this.hideUserPhonenumber);
    this.formAndButtons();
    this.updateOnBlur(e, 'showOrHideUserPhonenumber', this.hideUserPhonenumber);
  }

  allButtons() {
    this.submitStepButton = [
      {
        buttonId: 1,
        buttonLabel: 'Submit',
        isDisable: (this.disableCurrentButton || !(this.isUserAgreementCheckedFC.value == true)) || this.spinnerActivated,
        tooltip: ((this.currentActiveStep == 1) && this.disableCurrentButton) ? "Please, complete above fields to submit" : " Click here to submit.",
        parentEmit: true,
        spinnerActive: this.spinnerActivated
      }];

    this.backStepButton = [{
      buttonId: 2,
      buttonLabel: 'Back',
      isDisable: this.disableBackButton,
      tooltip: (this.disableBackButton) ? "Back is diabled." : " Go back.",
      parentEmit: true,
    }];

    this.nextStepButton = [{
      buttonId: 3,
      buttonLabel: 'Next or Skip',
      isDisable: this.disableCurrentButton,
      parentEmit: true,
    }];

    this.syncingButton = [{
      buttonId: 5,
      buttonLabel: this.currentStatus,
      isDisable: true,
      tooltip: 'your changes are awaiting to saved.',
      showButtonLabelWithSpinner: true,
      spinnerActive: this.spinnerActivated
    }];
  }

  mappings() {
    //let's map first step and rest seperatly
    // if (this.userRegCurrentFlow == this.usernameAndPassword_Flow && this.currentActiveStep == 1) {
    this.UserRegister.UserDetail.UserEmail = this.standardUserRegisterForm.get('useremail').value;
    this.UserRegister.UserDetail.PSWDHASH = this.standardUserRegisterForm.get('password').value;
    this.UserRegister.UserDetail.Username = this.standardUserRegisterForm.get('username').value;
    // } else {
    this.UserRegister.UserDetail.Firstname = this.standardUserRegisterForm.get('firstname').value;
    this.UserRegister.UserDetail.Lastname = this.standardUserRegisterForm.get('lastname').value;
    this.UserRegister.UserDetail.Middlename = this.standardUserRegisterForm.get('middlename').value;
    this.UserRegister.UserDetail.PhoneCountryCode = this.standardUserRegisterForm.get('usercountryCode').value;
    this.UserRegister.UserDetail.PhoneNumber = this.standardUserRegisterForm.get('userphonenumber').value;
    this.UserRegister.UserDetail.ShowPhonenumber = this.standardUserRegisterForm.get('showOrHideUserPhonenumber').value;

    //agreement
    this.UserRegister.UserDetail.IsUserAgreementChecked = this.standardUserRegisterForm.get('isUserAgreementChecked').value;
    //}
  }

  validateForm() {
    this.showFCError = true;
    this.userEmailFCError = '';
    this.usernameFCError = '';
    this.passwordFCError = '';
    this.confirmPasswordFCError = '';
    this.isUserAgreementCheckedFCError = '';

    if (this.currentActiveStep == 1) {
      if ((this.userEmailFC.invalid || this.userEmailFC.hasError('email') || this.userEmailFC.value === null || this.userEmailFC.value === '')) {
        this.userEmailFCError = 'Not a valid email.';
        return;
      }

      if ((this.usernameFC.invalid || this.usernameFC.hasError('required') || this.usernameFC.value === null || this.usernameFC.value === '')) {
        this.usernameFCError = 'You must create a username.';
        return;
      }

      if ((this.passwordFC.invalid || this.passwordFC.hasError('required') || this.passwordFC.value === null || this.passwordFC.value === '')) {
        this.passwordFCError = 'You must create a new password.';
        return;
      }

      if ((this.confirmPasswordFC.value !== this.passwordFC.value || this.confirmPasswordFC.invalid || this.confirmPasswordFC.hasError('required')
        || this.confirmPasswordFC.value === null || this.confirmPasswordFC.value === '')) {
        this.confirmPasswordFCError = 'Password not matching.';
        return;
      }

      if ((this.showSignAgreement) && (this.isUserAgreementCheckedFC.invalid || this.isUserAgreementCheckedFC.value === false
        || this.isUserAgreementCheckedFC.hasError('required') || this.isUserAgreementCheckedFC.value === null || this.isUserAgreementCheckedFC.value === ''
      )) {
        this.isUserAgreementCheckedFCError = this.userAgreementCheckbox[0].note;
        return;
      }
      else {
        if (this.showSignAgreement) {
          this.showFCError = false;
          this.isUserAgreementCheckedFCError = '';
        }

        this.userEmailFCError = '';
        this.usernameFCError = '';
        this.passwordFCError = '';
        this.confirmPasswordFCError = '';
      }
    }

    //check step 2 flow: no mandatory here.. but check if is dirty and saved so that check mark apears on top
    if (this.currentActiveStep == 2) {
      if (this.firstnameFC.value !== null || this.firstnameFC.value !== ''
        || this.lastnameFC.value !== null || this.lastnameFC.value !== ''
        || this.userPhonenumberFC.value !== null || this.userPhonenumberFC.value !== '') {
        this.pesonalDetailFilled = true;
      }
      this.showFCError = false;
    }

    if (this.currentActiveStep == 3) {
      // check for this.profilephotoFC.value
    }
  }

  userAgreementCheckbox = [
    { id: 0, label: 'Yes', isChecked: false, note: `You must agree to terms and conditions before submitting.` },
  ]

  userChoiceOnPhonenumber = [
    { id: 0, label: 'Show', isChecked: false, note: `you have choosen to show your phone number, this will allow other to see your number when ever you post something or on your profile. ` },
    { id: 1, label: 'Hide', isChecked: true, note: `you have choosen to hide your phone number, we will not show your number on your future post or on your profile unless you change it through profile.` }
  ]

}
