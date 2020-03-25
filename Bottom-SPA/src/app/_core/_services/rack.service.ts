import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { WmsCode } from '../_models/wms-code';
import { PaginatedResult } from '../_models/pagination';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { RackLocation } from '../_models/rack-location';
import { FilerRackParam } from '../_models/filer-rack-param';

@Injectable({
  providedIn: 'root'
})
export class RackService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getFactories() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/factory', {});
  }

  getWHs() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/wh', {});
  }

  getBuildings() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/building', {});
  }

  getFloors() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/floor', {});
  }

  getAreas() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/area', {});
  }


  // filter(param: Object) {
  //   console.log("Service: ",param);
  //   return this.http.post(this.baseUrl + 'rackLocation/', param);
  // }

  filter(page?, itemsPerPage?, text?: FilerRackParam): Observable<PaginatedResult<RackLocation[]>> {
    const paginatedResult: PaginatedResult<RackLocation[]> = new PaginatedResult<RackLocation[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    console.log(">>>", text)
    return this.http.post<RackLocation[]>(this.baseUrl + 'rackLocation', text, { observe: 'response', params })
      .pipe(
        map(response => {
          console.log("result: ",response.body);
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        }),
      );
  }

  
}
