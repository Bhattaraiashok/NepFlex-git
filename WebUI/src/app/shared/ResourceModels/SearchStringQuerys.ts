import { Injectable } from "@angular/core";
@Injectable()
export class SearchStringQuery {
  DefaultSearchQueryList: string[] = [
    'Auto',
    'Education Materials',
    'Services',
    'Rentals',
    'Real Estate',
    'Jobs'
  ];
  searchQueryList: string[] = [''];
}
