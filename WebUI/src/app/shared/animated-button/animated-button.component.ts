import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { ButtonProperties } from 'app/shared/ResourceModels/ButtonProperties';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SendEmailComponent } from 'app/shared/send-email/send-email.component';
import { SpinnerService } from "app/shared/services/control-services/spinner.service";

@Component({
  selector: 'app-animated-button',
  templateUrl: './animated-button.component.html',
  styleUrls: ['./animated-button.component.scss']
})
export class AnimatedButtonComponent implements OnInit {
  @Input() buttonCollections: ButtonProperties[];
  @Output() ParentEmitter: EventEmitter<ButtonProperties> = new EventEmitter<
    ButtonProperties
    >();
  showPopUpModal: boolean = false;
  constructor(private router: Router,
    private modalService: NgbModal) { }

  ngOnInit() {
    console.log('buttonCollections: ', this.buttonCollections);
  }
  functionalButton(id: number) {
    const buttonFunctions = this.buttonCollections.find(a => a.buttonId === id);

    if (
      buttonFunctions.hasPopUp === true &&
      buttonFunctions.popUpName === 'sendEmail'
    ) {
      this.showPopUpModal = true;
      // console.log('showPopUpModal: ', this.showPopUpModal);
      this.modalService.open(SendEmailComponent, { windowClass: 'dark-modal' });
    }
    if (buttonFunctions.HasDropDown) {

    } else if (buttonFunctions.canRoute === true) {
      // console.log('buttonFunctions.buttonRoute: ', buttonFunctions.buttonRoute);
      this.router.navigate([buttonFunctions.buttonRoute]);
    }

    if (buttonFunctions.parentEmit === true) {
      this.ParentEmitter.emit(buttonFunctions);
    }
  }
}
