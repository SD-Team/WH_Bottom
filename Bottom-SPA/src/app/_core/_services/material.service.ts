import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { MaterialSearch } from '../_viewmodels/material-search';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { Material } from '../_models/material';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MaterialService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  search(page?, itemsPerPage?, materialSearch?: MaterialSearch): Observable<PaginatedResult<Material[]>> {
    const paginatedResult: PaginatedResult<Material[]> = new PaginatedResult<Material[]>();
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    let url = this.baseUrl + 'materialPurchase/search/';
    return this.http.post<any>(url, materialSearch, {observe: 'response', params})
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
}
