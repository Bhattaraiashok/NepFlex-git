import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpResponse,
  HttpRequest,
  HttpHeaders,
  HttpErrorResponse
} from '@angular/common/http';
import { ReportGetData } from 'app/shared/ResourceModels/ReportGetData';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Headers, RequestOptions } from '@angular/http';
import { LoginRequest, LoginResponse } from "app/shared/ResourceModels/LoginModel";

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private apiUrl_login = 'http://localhost/ServiceAPI/api/user/login';
  private apiUrl_logOut = 'http://localhost/ServiceAPI/api/user/logoff';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) {}

  login(val: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${this.apiUrl_login}`,
      JSON.stringify(val),
      this.httpOptions
    );
  }

  logout(){
    return this.http.post(
      `${this.apiUrl_logOut}`,
      this.httpOptions
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` + `body was: ${error.error}`
      );
    }
    // return an ErrorObservable with a user-facing error message
    return throwError('Something bad happened; please try again later.');
  }
}
