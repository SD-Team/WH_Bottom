import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import { PackingPrintAll } from '../_viewmodels/packing-print-all';
import { QRCodeIDVersion } from '../_viewmodels/qrcode-version';

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
  findByQrCodeIdList(receives: QRCodeIDVersion[]): Observable<PackingPrintAll[]> {
    return this.http.post<any>(this.baseUrl + 'packingListDetail/findPrint/', receives);
  }
  findByQrCodeIdListAgain(receives: QRCodeIDVersion[]): Observable<PackingPrintAll[]> {
    return this.http.post<any>(this.baseUrl + 'packingListDetail/findPrintAgain/', receives);
  }
  changePackingPrint(packingPrintAll: PackingPrintAll[]) {
    this.packingPrintSourse.next(packingPrintAll);
  }
  changePrintQrCodeAgain(option: string) {
    this.printQrCodeAgainSource.next(option);
  }
}
