import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import { QRCodeMainModel } from '../_viewmodels/qrcode-main-model';
import { QRCodeMainSearch } from '../_viewmodels/qrcode-main-search';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { QrcodePrint } from '../_models/qrcode-print';

@Injectable({
  providedIn: 'root'
})
export class QrcodeMainService {

constructor(private http: HttpClient) { }
  qrCodeMainListSource = new BehaviorSubject<QRCodeMainModel[]>([]);
  currentQrCodeMain = this.qrCodeMainListSource.asObservable();
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
  changeQrCodeMainList(qrCodeMainList: QRCodeMainModel[]) {
    this.qrCodeMainListSource.next(qrCodeMainList);
  }

  printQrCode(qrCodeId: string, qrCodeVersion: number) {
    return this.http.get<QrcodePrint>(this.baseUrl + 'QRCodeMain/printqrcode/' + qrCodeId + '/version/' + qrCodeVersion, {});
  }

  getQrCodeVersionLastest(qrCodeId: string) {
    return this.http.get<number>(this.baseUrl + 'QRCodeMain/GetQrCodeVersionLastest', {params: {qrCodeId: qrCodeId}});
  }
}
