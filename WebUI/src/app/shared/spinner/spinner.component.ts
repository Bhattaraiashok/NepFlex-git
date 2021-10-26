
import { LoaderService } from '../../shared/services/loader.service';
import { Component, OnInit, Input, ChangeDetectorRef } from '@angular/core';
import { SpinnerService } from "app/shared/services/control-services/spinner.service";

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent implements OnInit {
  showLargeLoader: boolean = false;
  showSmallLoader: boolean = false;
  showDisableSmallLoader: boolean = false;

  constructor(private cdRef: ChangeDetectorRef, private spinnerService: SpinnerService) {

  }

  ngOnInit() {
    this.spinner_lg();
    this.spinner_sm();
    this.spinner_disable_sm();
  }

  spinner_lg() {
    this.spinnerService.getSpinner_lg_Observer().subscribe((status) => {
      this.showLargeLoader = status === 'start';
      this.cdRef.detectChanges();
      console.log('Spinner spinner_lg :  ', this.showLargeLoader);
    })
  }
  spinner_sm() {
    this.spinnerService.getSpinner_sm_Observer().subscribe((status) => {
      this.showSmallLoader = status === 'start';
      this.cdRef.detectChanges();
      console.log('Spinner spinner_sm :  ', this.showSmallLoader);
    })
  }

  spinner_disable_sm() {
    this.spinnerService.getSpinner_disable_sm_Observer().subscribe((status) => {
      this.showDisableSmallLoader = status === 'start';
      this.cdRef.detectChanges();
      console.log('Spinner spinner_disable_sm :  ', this.showDisableSmallLoader);
    })
  }
}
