import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { QRCodeMainModel } from '../_viewmodels/qrcode-main-model';
import { QRCodeMainSearch } from '../_viewmodels/qrcode-main-search';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class QrcodeMainService {

constructor(private http: HttpClient) { }
  baseUrl = environment.apiUrl;
  generateQrCode(listData: string[]) {
    return this.http.post<any>(this.baseUrl + 'qRCodeMain/', listData);
  }
  search(page?, itemsPerPage?, modelSearch?: QRCodeMainSearch): Observable<PaginatedResult<QRCodeMainModel[]>> {
    const paginatedResult: PaginatedResult<QRCodeMainModel[]> = new PaginatedResult<QRCodeMainModel[]>();
    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    // tslint:disable-next-line:prefer-const
    let url = this.baseUrl + 'qRCodeMain/searchPlan/';
    return this.http.post<any>(url , modelSearch, {observe: 'response', params})
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
