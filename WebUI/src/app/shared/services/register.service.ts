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
import { RegisterRequest, RegisterResponse, UserRegister } from "app/shared/ResourceModels/registerModel";

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private apiUrl_postReport = 'http://localhost/ServiceAPI/api/user/register';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  register(val: UserRegister): Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(
      `${this.apiUrl_postReport}`,
      JSON.stringify(val),
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
