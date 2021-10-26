import { Injectable } from '@angular/core';
import { Observable, Subject } from "rxjs";
import { CanDeactivate } from "@angular/router/router";
import { FormGroup } from "@angular/forms/forms";
import { ModalComponent } from "app/shared/modal/modal.component";
import { BsModalService } from "ngx-bootstrap/modal";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

export interface IDeactivateComponent {
  canExit: () => boolean;
}

@Injectable({
  providedIn: 'root'
})
export class CanDeactivateGuardService implements CanDeactivate<IDeactivateComponent> {
  constructor(private modalService: NgbModal) { }
  canDeactivate(component: IDeactivateComponent): boolean {
    console.log('can deactivate component: ', component.canExit);
    if (component.canExit) {
      return true;
    } else {
      false;
    }
  }
}
