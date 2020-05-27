import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Output } from '../_models/output';
import { BehaviorSubject } from 'rxjs';
import { MaterialSheetSize } from '../_models/material-sheet-size';
import { OutputM, OutputParam } from '../_models/outputM';
import { TransferDetail } from '../_models/transfer-detail';
import { OutputParams } from '../_viewmodels/output-param';
import { OutputDetail } from '../_models/output-detail';

@Injectable({
  providedIn: 'root'
})
export class OutputService {
  baseUrl = environment.apiUrl;
  listMaterialSheetSizeSource = new BehaviorSubject<Array<MaterialSheetSize>>([]);
  currentListMaterialSheetSize = this.listMaterialSheetSizeSource.asObservable();
  outputMSource = new BehaviorSubject<Object>({});
  currentOutputM = this.outputMSource.asObservable();
  listOutputMSource = new BehaviorSubject<Array<OutputM>>([]);
  currentListOutputM = this.listOutputMSource.asObservable();
  qrCodeIdSource = new BehaviorSubject<string>('');
  currentQrCodeId = this.qrCodeIdSource.asObservable();
  flagFinishSource = new BehaviorSubject<boolean>(false);
  currentFlagFinish = this.flagFinishSource.asObservable();
  flagSubmitSource = new BehaviorSubject<boolean>(false);
  currentFlagSubmit = this.flagFinishSource.asObservable();
  listOutputSaveSource = new BehaviorSubject<Array<any>>([]);
  currentListOutputSave = this.listOutputSaveSource.asObservable();

  constructor(private http: HttpClient) { }

  changeListMaterialSheetSize(listMaterialSheetSize: Array<MaterialSheetSize>) {
    this.listMaterialSheetSizeSource.next(listMaterialSheetSize);
  }
  changeOutputM(outputM: OutputM) {
    this.outputMSource.next(outputM);
  }
  changeListOutputM(listOutputM: OutputM[]) {
    this.listOutputMSource.next(listOutputM);
  }
  changeQrCodeId(qrCodeId: string) {
    this.qrCodeIdSource.next(qrCodeId);
  }
  changeFlagFinish(flag: boolean) {
    this.flagFinishSource.next(flag);
  }
  changeFlagSubmit(flag: boolean) {
    this.flagSubmitSource.next(flag);
  }
  changeListOutputSave(listOutputSave: any[]) {
    this.listOutputSaveSource.next(listOutputSave);
  }
  getMainByQrCodeId(qrCodeId: string) {
    return this.http.get<Output>(this.baseUrl + 'Output/GetByQrCodeId', { params: { qrCodeId: qrCodeId } });
  }
  getOutputDetail(transacNo: string) {
    return this.http.get<OutputDetail>(this.baseUrl + 'Output/detail/' + transacNo);
  }
  saveListOutput(listOutput: OutputParam[]) {
    return this.http.post(this.baseUrl + 'Output/savelistoutput', listOutput);
  }
}
