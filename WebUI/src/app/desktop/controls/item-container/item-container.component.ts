import { Component, OnInit, Input } from '@angular/core';
import { SearchResponse } from 'app/shared/ResourceModels/SearchResponse';
import { ButtonProperties } from 'app/shared/ResourceModels/ButtonProperties';
import { Router } from '@angular/router';


@Component({
  selector: 'app-item-container',
  templateUrl: './item-container.component.html',
  styleUrls: ['./item-container.component.scss']
})
export class ItemContainerComponent implements OnInit {
  @Input() itemResults: SearchResponse[] = new Array();
  @Input() searchText: string;
  detailButttons: ButtonProperties[] = new Array();
  totalCountOfItem: number;

  AllTitles: string[];
  AllPost: string[];
  constructor(private router: Router) {
  }

  ngOnInit() {
    this.allButtons();
    this.totalCountOfItem = this.itemResults.length;
    this.render();
  }
  render() {
    console.log('this.itemResults: ', this.itemResults);
  }

  allButtons() {
    this.detailButttons = [
      {
        buttonId: 1,
        buttonLabel: 'More Details',
        hasPopUp: false,
        buttonRoute: 'detail/',
        canRoute: false,
        HasDropDown: false,
        DropDownList:null,
        popUpName: 'clickMoreDetailsButton'
      }
    ];
  }
  goToMoreDetails(id: number) {
    this.router.navigate(['/detail', id.toString()]);
  }
}

