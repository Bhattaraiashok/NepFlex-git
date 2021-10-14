
import { LoaderService } from '../../shared/services/loader.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent implements OnInit {
  @Input() showLargeLoader: boolean = false;
  @Input() showSmallLoader: boolean = false;

  constructor() { }

  ngOnInit() {
    console.log('spinner component large and small loader :', this.showLargeLoader, this.showSmallLoader);
  }
}
