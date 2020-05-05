import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import { PackingDetailResult } from '../_viewmodels/packing-detail-result';
import { PackingPrintAll } from '../_viewmodels/packing-print-all';

@Injectable({
  providedIn: 'root'
})
export class PackingListDetailService {
  baseUrl = environment.apiUrl;
  packingPrintSourse = new BehaviorSubject<PackingPrintAll[]>([]);
  currentPackingPrint = this.packingPrintSourse.asObservable();
  printQrCodeAgainSource = new BehaviorSubject<string>('0');
  currentPrintQrCodeAgain = this.printQrCodeAgainSource.asObservable();
  constructor(private http: HttpClient) { }
  findByQrCodeIdList(receives: string[]): Observable<PackingPrintAll[]> {
    return this.http.post<any>(this.baseUrl + 'packingListDetail/findPrint/', receives);
  }
  changePackingPrint(packingPrintAll: PackingPrintAll[]) {
    this.packingPrintSourse.next(packingPrintAll);
  }
  changePrintQrCodeAgain(option: string) {
    this.printQrCodeAgainSource.next(option);
  }
}
