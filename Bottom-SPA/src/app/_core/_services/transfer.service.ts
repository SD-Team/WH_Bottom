import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';

import { environment } from '../../../environments/environment';
import { TransferM } from '../_models/transferM';

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
}
