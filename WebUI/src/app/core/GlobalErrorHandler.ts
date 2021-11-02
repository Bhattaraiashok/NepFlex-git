import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { LocationStrategy, PathLocationStrategy } from '@angular/common';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  constructor(private injector: Injector) { }
  handleError(error: Error): void {
    const loggingService: ErrorLoggingService = this.injector.get(ErrorLoggingService);
    const location: LocationStrategy = this.injector.get(LocationStrategy);
    const url: string = location instanceof PathLocationStrategy ? location.path() : '';
    console.error(error);

    loggingService.logError(error, url)
    .subscribe(() => {
    });
  }
}

@Injectable()
export class ErrorLoggingService {

  private apiUrl: string;

  private headers: Headers = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http, private onlineStatusService: OnlineStatusService) {
    this.apiUrl = '/api/ClientSideError';
  }

  logError(error: Error, url: string): Observable<Response> {
    return this.onlineStatusService.isNetworkConnectedObservable.flatMap((isApplicationConnected: boolean) => {
      if (isApplicationConnected) {
        let errorString: string = error.message;
        errorString = errorString + '\\r\\n\\r\\n';
        errorString = errorString + 'URL: ' + url;
        errorString = errorString + '\\r\\n\\r\\n';
        errorString = errorString + 'ERROR:' + error.toString();
        errorString = errorString + '\\r\\n';

       return this.http.post(this.apiUrl, errorString, { headers: this.headers })
       .catch(this.handleError);

      }
    });
  }

  // tslint:disable-next-line:no-any
  public handleError(error: Response | {message: string}): Observable<any> {
    return Observable.of({});
  };
}
