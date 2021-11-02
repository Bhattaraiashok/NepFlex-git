import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { from, Observable, Observer, of } from 'rxjs';
import { ButtonProperties } from '../ResourceModels/ButtonProperties';
import { SearchResponse } from '../ResourceModels/SearchResponse';

@Component({
  selector: 'app-tab-layout',
  templateUrl: './tab-layout.component.html',
  styleUrls: ['./tab-layout.component.scss']
})
export class TabLayoutComponent implements OnInit {
  @Input() tabResults: SearchResponse[];
  detailButttons: ButtonProperties[] = new Array();
  totalCountOfItem: number;
  asyncTabs: Observable<SearchResponse[]> = new Observable<SearchResponse[]>();
  constructor(private router: Router) {

  }

  ngOnInit() {
    this.allButtons();
    this.totalCountOfItem = this.tabResults.length;
    if (this.tabResults !== undefined) {
      this.asyncTabs = this.getProducts()
      console.log('this.asyncTabs: ', this.asyncTabs);
    }
  }
  getProducts(): Observable<SearchResponse[]> {
    return of(this.tabResults);
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
        popUpName: 'clickMoreDetailsButton'
      }
    ];
  }
  goToMoreDetails(id: number) {
    this.router.navigate(['/detail', id.toString()]);
  }

}
export interface ExampleTab {
  label: string;
  content: string;
}
