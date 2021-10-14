import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Component, ElementRef, ViewChild, OnInit, Input, Output, EventEmitter, Injectable } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent, MatAutocomplete } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { SearchStringQuery } from 'app/shared/ResourceModels/SearchStringQuerys';
import { ButtonProperties } from 'app/shared/ResourceModels/ButtonProperties';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auto-complete-searchbox',
  templateUrl: './auto-complete-searchbox.component.html',
  styleUrls: ['./auto-complete-searchbox.component.scss']
})
export class AutoCompleteSearchboxComponent implements OnInit {
  @Input() multiStringOptions: boolean;
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  searchStringCtrl = new FormControl();
  filteredSearchStrings: Observable<string[]>;
  searchStringOption: string[] = [];
  allSearchStringOptions: string[];
  searchButtton: ButtonProperties[] = new Array();
  searchString: SearchString[] = [];
  @ViewChild('searchInput', {static: true}) searchInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto', {static: true}) matAutocomplete: MatAutocomplete;
  @Output() ParentButtonEmitter: EventEmitter<SearchString[]> = new EventEmitter<SearchString[]>();

  constructor(el: SearchStringQuery, private router: Router) {
    this.allSearchStringOptions = el.DefaultSearchQueryList;
    this.filteredSearchStrings = this.searchStringCtrl.valueChanges.pipe(
      startWith(null),
      map((fruit: string | null) =>
        fruit ? this._filter(fruit) : this.allSearchStringOptions.slice()
      )
    );
  }

  add(event: MatChipInputEvent): void {
    // Add fruit only when MatAutocomplete is not open
    // To make sure this does not conflict with OptionSelected Event
    if (!this.matAutocomplete.isOpen) {
      const input = event.input;
      const value = event.value;

      // Add our fruit
      if ((value || '').trim()) {
        this.searchStringOption.push(value.trim());
      }

      // Reset the input value
      if (input) {
        input.value = '';
      }

      this.searchStringCtrl.setValue(null);
    }
  }

  remove(searchString: string): void {
    const index = this.searchStringOption.indexOf(searchString);

    if (index >= 0) {
      this.searchStringOption.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.searchStringOption.push(event.option.viewValue);
    if (this.searchStringOption.length > 0) {
      this.searchButtonOnOff();
    }
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.allSearchStringOptions.filter(
      searchString => searchString.toLowerCase().indexOf(filterValue) === 0
    );
  }

  searchButtonOnOff(): boolean {
    if (this.searchStringOption.length > 0) {
      return true;
    } else {
      return false;
    }
  }
  ngOnInit(): void {
    this.searchButtton = [
      {
        buttonId: 1,
        buttonLabel: 'Search',
        hasPopUp: false,
        buttonRoute: '',
        canRoute: false,
        HasDropDown: false,
        popUpName: '',
        parentEmit: true
      }
    ];
  }
  searchBtnClicked(e: Event) {
    // console.log(this.searchString, this.searchStringOption);
    if (this.searchStringOption.length > 0) {
      this.searchString['name'] = this.searchStringOption;
      this.ParentButtonEmitter.emit(this.searchString['name']);
    }

    // if (this.searchStringOption.length > 1) {
    //   this.searchString['name'] = this.searchStringOption;
    //   this.ParentButtonEmitter.emit(this.searchString['name']);
    // }
  }
}

@Injectable()
export class SearchString {
  name: string;
  public constructor() { }
}
