import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Output } from '../_models/output';
import { BehaviorSubject } from 'rxjs';
import { MaterialSheetSize } from '../_models/material-sheet-size';
import { OutputM } from '../_models/outputM';

@Injectable({
  providedIn: 'root'
})
export class OutputService {
  baseUrl = environment.apiUrl;
  listMaterialSheetSizeSource = new BehaviorSubject<Array<MaterialSheetSize>>([]);
  currentListMaterialSheetSize = this.listMaterialSheetSizeSource.asObservable();
  outputMSource = new BehaviorSubject<Object>({});
  currentOutputM = this.outputMSource.asObservable();

  constructor(private http: HttpClient) { }

  changeListMaterialSheetSize(listMaterialSheetSize: Array<MaterialSheetSize>) {
    this.listMaterialSheetSizeSource.next(listMaterialSheetSize);
  }
  changeOutputM(outputM: OutputM) {
    this.outputMSource.next(outputM);
  }
  getMainByQrCodeId(qrCodeId: string) {
    return this.http.get<Output>(this.baseUrl + 'Output/GetByQrCodeId', {params: {qrCodeId: qrCodeId}});
  }
}
