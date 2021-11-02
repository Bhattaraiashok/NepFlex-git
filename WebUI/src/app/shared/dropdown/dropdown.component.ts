import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { DropDownList } from 'app/shared/ResourceModels/ButtonProperties';

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.scss']
})
export class DropdownComponent implements OnInit {
  @Input() dropdownOptions: DropDownList[]
  // @Input() dropdownLabel: string;
  // @Input() showDropdownLabel: boolean = true;
  // @Input() dropdownLabelRoute: string;
  constructor(private route: Router) { }

  ngOnInit() {
    //this.dropdownGenerator();
  }

  // dropdownGenerator() {
  //   console.log(this.dropdownOptions);
  // }

  routeDropdownTo(val: string) {
    this.route.navigate([val]);
  }
}
