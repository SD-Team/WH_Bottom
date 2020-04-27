import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { PackingSearch } from '../_viewmodels/packing-search';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { PackingList } from '../_models/packingList';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PackingListService {
    baseUrl = environment.apiUrl + 'PackingList/';
    constructor(private http: HttpClient) { }
    search(page?, itemsPerPage?, packingSearch?: PackingSearch): Observable<PaginatedResult<PackingList[]>> {
      const paginatedResult: PaginatedResult<PackingList[]> = new PaginatedResult<PackingList[]>();
      let params = new HttpParams();
      if (page != null && itemsPerPage != null) {
        params = params.append('pageNumber', page);
        params = params.append('pageSize', itemsPerPage);
      }
      let url = this.baseUrl + 'search/';
      return this.http.post<any>(url, packingSearch, {observe: 'response', params})
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
    }
    findBySupplier(supplier: any): any {
        return this.http.get<any>(this.baseUrl + 'findBySupplier/' + supplier, {});
    }
  }
