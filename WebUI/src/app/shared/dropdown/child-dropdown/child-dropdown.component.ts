import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { DropDownList } from 'app/shared/ResourceModels/ButtonProperties';

@Component({
  selector: 'app-child-dropdown',
  templateUrl: './child-dropdown.component.html',
  styleUrls: ['./child-dropdown.component.scss']
})
export class ChildDropdownComponent implements OnInit {
  @Input() items: DropDownList[];
  @ViewChild('childMenu', {static: true}) public childMenu;
  constructor() {}

  ngOnInit() {}
}
