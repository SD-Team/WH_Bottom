import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { InputM } from '../_models/inputM';
import { InputDetail } from '../_models/input-detail';
import { BehaviorSubject } from 'rxjs';
import { MissingPrint } from '../_models/missing-print';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class InputService {
  baseUrl = environment.apiUrl;
  listInputMainSource = new BehaviorSubject<Object[]>([]);
  currentListInputMain = this.listInputMainSource.asObservable();
  inputDetailSource = new BehaviorSubject<Object>({});
  currentInputDetail = this.inputDetailSource.asObservable();
  flagSource = new BehaviorSubject<string>("");
  currentFlag = this.flagSource.asObservable();
  constructor(private http: HttpClient) { }

  getMainByQrCodeID(qrCodeID: string) {
    return this.http.get<InputDetail>(this.baseUrl + 'input/detail/' + qrCodeID, {});
  }

  getDetailByQrCodeID(qrCodeID: string) {
    return this.http.get<InputDetail>(this.baseUrl + 'input/detail/' + qrCodeID, {});
  }

  saveInput(params: InputDetail) {
    return this.http.post(this.baseUrl + 'input/create', params);
  }

  submitInputMain(listInput: string[]) {
    return this.http.put(this.baseUrl + 'input/submit', listInput);
  }

  changeListInputMain(listInputDetail: InputDetail[]) {
    this.listInputMainSource.next(listInputDetail);
  }

  changeInputDetail(inputDetail: InputDetail) {
    this.inputDetailSource.next(inputDetail);
  }

  changeFlag(flag: string) {
    this.flagSource.next(flag);
  }

  printMissing(missingNo: string) {
    return this.http.get<MissingPrint>(this.baseUrl + 'input/printmissing/' + missingNo, {});
  }
}
