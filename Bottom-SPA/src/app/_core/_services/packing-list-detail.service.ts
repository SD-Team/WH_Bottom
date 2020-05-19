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

  // In QrCodeId sau khi generate Qr Code ở Mục 2.2
  printMaterialForm(data: QRCodeIDVersion[]): Observable<PackingPrintAll[]> {
    return this.http.post<any>(this.baseUrl + 'packingListDetail/findPrint/', data);
  }

  // In QrCodeId ở mục Input
  findByQrCodeId(data: string[]): Observable<PackingPrintAll[]> {
    return this.http.post<any>(this.baseUrl + 'packingListDetail/findPrintQrCode/', data);
  }

  // In QrCodeId ở Mục 8.1 Và Output.
  findByQrCodeIdListAgain(data: QRCodeIDVersion[]): Observable<PackingPrintAll[]> {
    return this.http.post<any>(this.baseUrl + 'packingListDetail/findPrintAgain/', data);
  }
  changePackingPrint(packingPrintAll: PackingPrintAll[]) {
    this.packingPrintSourse.next(packingPrintAll);
  }
  changePrintQrCodeAgain(option: string) {
    this.printQrCodeAgainSource.next(option);
  }
}
