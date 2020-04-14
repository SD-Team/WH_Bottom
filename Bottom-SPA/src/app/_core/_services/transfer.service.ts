import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

import { environment } from '../../../environments/environment';
import { TransferM } from '../_models/transferM';
import { TransferHistoryParam } from '../_viewmodels/transfer-history-param';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { TransferDetail } from '../_models/transfer-detail';

@Injectable({
  providedIn: 'root'
})
export class TransferService {
  baseUrl = environment.apiUrl;
  printTransfer = new BehaviorSubject<Array<TransferM>>([]);
  currentTransfer = this.printTransfer.asObservable();

  constructor(private http: HttpClient) { }

  getMainByQrCodeId(qrCodeId: string) {
    return this.http.get<TransferM>(this.baseUrl + 'TransferLocation/' + qrCodeId, {});
  }

  submitMain(lists: TransferM[]) {
    return this.http.post(this.baseUrl + 'TransferLocation/submit', lists);
  }

  changePrintTransfer(transfer: Array<TransferM>) {
    this.printTransfer.next(transfer);
  }

  search(pageNumber?, pageSize?, transferHistoryParam?: TransferHistoryParam) {
    const paginatedResult: PaginatedResult<TransferM[]> = new PaginatedResult<TransferM[]>();

    let params = new HttpParams();

    if (pageNumber != null && pageSize != null) {
      params = params.append('pageNumber', pageNumber);
      params = params.append('pageSize', pageSize);
    }

    return this.http.post<TransferM[]>(this.baseUrl + 'TransferLocation/search', transferHistoryParam, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        }),
      );
  }

  getTransferDetail(transferNo: string) {
    return this.http.get<TransferDetail[]>(this.baseUrl + 'TransferLocation/GetDetailTransaction', { params: { transferNo: transferNo } });
  }
}
