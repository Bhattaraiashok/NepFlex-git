import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ContextManagerService {

  constructor() { }

  Set(key: string, value: any): boolean {
    let result = false;
    if (key != null && value != null) {
      const keyAlreadyContains = localStorage.getItem(key);
      if (keyAlreadyContains != null) {
        localStorage.removeItem(key);
      }
      localStorage.setItem(key, value);
      result = true;
    }
    return result;
  }

  Get(key: string) {
    return localStorage.getItem(key);
  }

  Distroy() {
    localStorage.clear();
  }

}
