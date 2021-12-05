import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { ItemDescription } from "app/shared/ResourceModels/ItemDescription";
import { FilteringSearch } from "app/shared/ResourceModels/FilteringSearch";
import { SearchService } from "app/shared/services/search.service";

@Component({
  selector: 'app-filter-search',
  templateUrl: './filter-search.component.html',
  styleUrls: ['./filter-search.component.scss']
})
export class FilterSearchComponent implements OnInit {
  @Input() itemDescriptions: ItemDescription[] = new Array();
  @Input() _selectedValue: string;
  @Output() filteredBy: EventEmitter<any> = new EventEmitter();
  @Output() closeFilterBar: EventEmitter<boolean> = new EventEmitter();
  orderListBy: string[] = ['Recently Added', 'Old Stuffs'];

  constructor(
    private filtering: FilteringSearch,
    private _searchService: SearchService
  ) { }

  get selectedValue() {
    return this._selectedValue;
  }

  set selectedValue(val) {
    this._selectedValue = val;
    this.propagateChange(this._selectedValue);
  }
  ngOnInit(): void {
    this.filterBar();
  }
  filterBar() {
    this._searchService.getItemDescription().subscribe(x => {
      this.itemDescriptions = x;
    });

    //   this._searchService.getItemDescription().pipe(
    //     map(res => res.values)
    //   )
    //   .subscribe(res => console.log(res));
    // }

    this._searchService
      .getItemDescription()
      .subscribe(res => console.log(res));
  }

  changedFilterBy(event: { target; value: string }, field: string) {
    const val = event.target.value;
    this.filtering.val = val;
    this.filtering.field = field;
    this.filteredBy.emit();
  }

  writeValue(obj: any): void { }
  registerOnChange(fn: any): void {
    this.propagateChange = fn;
  }
  registerOnTouched(fn: any): void { }
  setDisabledState?(isDisabled: boolean): void { }
  propagateChange = (_: any) => { };

}
