import { Component, OnInit, Input, ViewEncapsulation, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TermsOfUseComponent } from '../../desktop/pages/terms-of-use/terms-of-use.component';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ModalComponent implements OnInit {
  @Input() name;
  closeResult: string;
  constructor() { }
  ngOnInit(): void {
  }
}
