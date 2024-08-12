import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";

import { Observable } from "rxjs";
import { tap } from 'rxjs/internal/operators/tap';

import { IApiBaseActions, ParamsType } from "./api-base-actions.interface";

@Injectable({
  providedIn: 'root'
})
export class ApiService implements IApiBaseActions {
  constructor(public httpClient: HttpClient) { }

  get(url: string, params?: ParamsType): Observable<any> {
    return this.httpClient
      .get(url, {params: this.createParams(params)})
      .pipe(tap((x) => this.handleResponse(x)));
  }

  getAll(url: string, params?: ParamsType): Observable<any> {
    return this.httpClient
      .get(url, {params: this.createParams(params)})
      .pipe(tap((x) => this.handleResponse(x)));
  }

  delete(url: string, data:any, params?: ParamsType): Observable<any> {
    return this.httpClient
      .delete(url, {params: this.createParams(params)})
      .pipe(tap((x) => this.handleResponse(x)));
  }

  post(url: string, data: any, params?: ParamsType): Observable<any> {
    return this.httpClient
      .post(url, data, {params: this.createParams(params)})
      .pipe(tap((x) => this.handleResponse(x)));
  }

  put(url: string, data: any, params?: ParamsType): Observable<any> {
    return this.httpClient
      .put(url, data, {params: this.createParams(params)})
      .pipe(tap((x) => this.handleResponse(x)));
  }

  private handleResponse(response: any) {
    if (response.Status === 403) {
      //this.window.location.replace(response.error);
    }
  }

  private createParams(params?: ParamsType) {
    let httpParams = new HttpParams();

    if (params) {
      Object.entries(params).forEach(([key, value]) => {
        httpParams = httpParams.append(key, `${value}`);
      });
    }

    return httpParams;
  }
}
