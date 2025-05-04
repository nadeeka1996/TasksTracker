import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

export abstract class BaseService {
  constructor(protected http: HttpClient) {}

  protected get<T>(
    endpointUrl: string,
    options: { headers?: HttpHeaders; params?: HttpParams } = {}
  ): Observable<T> {
    const { headers, params } = options;

    return this.http
      .get<{ result: T }>(endpointUrl, { headers, params })
      .pipe(map((response) => response.result));
  }

  protected post<T>(
    endpointUrl: string,
    body: any,
    options: { headers?: HttpHeaders } = {}
  ): Observable<T> {
    return this.http
      .post<{ result: T }>(endpointUrl, body, options)
      .pipe(map((response) => response.result));
  }

  protected put<T>(
    endpointUrl: string,
    body: any,
    options: { headers?: HttpHeaders } = {}
  ): Observable<T> {
    return this.http
      .put<{ result: T }>(endpointUrl, body, options)
      .pipe(map((response) => response.result));
  }
}
