import {
  Component,
  OnInit,
  Input,
  SimpleChanges,
  Output,
  EventEmitter,
  AfterViewInit,
  OnChanges
} from '@angular/core';
import { SearchResponse } from 'app/shared/ResourceModels/SearchResponse';
import { Observable } from 'rxjs';
import { OrderByPipe } from 'app/shared/pipes/order-by.pipe';
import { FilterByPipe } from 'app/shared/pipes/filter-by.pipe';
import { FilteringSearch } from 'app/shared/ResourceModels/FilteringSearch';
import { SearchService } from '../../../../shared/services/search.service';
import { SpinnerService } from "app/shared/services/control-services/spinner.service";

@Component({
  selector: 'app-search-results-overlay',
  templateUrl: './search-results-overlay.component.html',
  styleUrls: ['./search-results-overlay.component.scss']
})
export class SearchResultsOverlayComponent
  implements OnInit, AfterViewInit, OnChanges {
  @Input() searchText: string;
  @Output() turnOverlayOn: EventEmitter<boolean> = new EventEmitter();
  searchResults: SearchResponse[] = new Array();
  searchResponse: SearchResponse[] = new Array();
  TotalCount: number;
  turnLargeLoader: boolean = false;

  constructor(
    private searchService: SearchService,
    private filtering: FilteringSearch,
    private spinnerService: SpinnerService
  ) {
    this.turnLargeLoader = true;
    this.spinnerService.showSpinner_lg();
  }
  ngAfterViewInit(): void {
    this.searching();
  }
  ngOnInit() { }

  ngOnChanges(changes: SimpleChanges) {
    this.searching();
  }
  closeOverlay(event: Event) {
    this.turnOverlayOn.emit(false);
  }

  filterBy() {
    this.turnLargeLoader = true;
    this.spinnerService.showSpinner_lg();
    const pipe = new FilterByPipe();
    this.searchResults = this.searchResponse; // I have to always pretend fresh copy
    const field: string = this.filtering.field.toLowerCase();
    let val: string = this.filtering.val;
    switch (field) {
      case 'topcategory':
        this.searchResults = pipe.transform(
          this.searchResults,
          'topCategory',
          val
        );
        break;
      case 'orderby':
        const pipe2 = new OrderByPipe();
        if (val === 'recently added') {
          val = 'dateAdded';
          this.searchResults = pipe2.transform(this.searchResults, val, true);
        } else {
          this.searchResults = pipe2.transform(this.searchResults, val, false);
        }
        break;
      case 'condition':
        this.searchResults = pipe.transform(
          this.searchResults,
          'condition',
          val
        );
        break;
      case 'warranty':
        this.searchResults = pipe.transform(
          this.searchResults,
          'warranty',
          val
        );
        break;
    }
    this.turnLargeLoader = false;
    this.spinnerService.disableSpinner_lg();
  }

  searching() {
    // this.searchService.getSearchResponse(this.searchText).subscribe(x => {
    //   this.TotalCount = x.length;
    //   this.searchResponse = x;
    //   this.searchResults = x;
    //   this.turnLargeLoader = false;
    // });
  }
}
