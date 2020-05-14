import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { InputM } from '../_models/inputM';
import { InputDetail } from '../_models/input-detail';
import { BehaviorSubject, Observable } from 'rxjs';
import { MissingPrint } from '../_models/missing-print';
import { map } from 'rxjs/operators';
import { TransactionMain } from '../_models/transaction-main';
import { FilterQrCodeAgainParam } from '../_viewmodels/qrcode-again-search';
import { PaginatedResult } from '../_models/pagination';
import { FilterMissingParam } from '../_viewmodels/missing-print-search';

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
  qrCodeAgainParamStart: FilterQrCodeAgainParam;
  qrCodeAgainParamSource = new BehaviorSubject<FilterQrCodeAgainParam>(this.qrCodeAgainParamStart);
  currentQrCodeAgainParam = this.qrCodeAgainParamSource.asObservable();
  missingPrintStart: FilterMissingParam;
  missingPrintParamSource = new BehaviorSubject<FilterMissingParam>(this.missingPrintStart);
  currentMissingParam = this.missingPrintParamSource.asObservable();
  checkSubmitSource = new BehaviorSubject<boolean>(false);
  currentCheckSubmit = this.checkSubmitSource.asObservable();
  printMissingSource = new BehaviorSubject<string>('0');
  currentPrintMissing = this.printMissingSource.asObservable();
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

  submitInputMain(inputModel: any) {
    return this.http.post(this.baseUrl + 'input/submit/', inputModel);
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
  changeCodeAgainParam(param: FilterQrCodeAgainParam) {
    this.qrCodeAgainParamSource.next(param);
  }
  changeCheckSubmit(check: boolean) {
    this.checkSubmitSource.next(check);
  }
  changeMissingPrint(missing: string) {
    this.printMissingSource.next(missing);
  }
  clearDataChangeMenu() {
    this.listInputMainSource.next([]);
    this.flagSource.next('');
    this.checkSubmitSource.next(false);
    this.printMissingSource.next('0');
  }
  changeMissingParam(param: FilterMissingParam) {
    this.missingPrintParamSource.next(param);
  }
  printMissing(missingNo: string) {
    return this.http.get<MissingPrint>(this.baseUrl + 'input/printmissing/' + missingNo, {});
  }
  qrCodeAgainFilter(page?, itemsPerPage?, text?: FilterQrCodeAgainParam): Observable<PaginatedResult<TransactionMain[]>> {
    const paginatedResult: PaginatedResult<TransactionMain[]> = new PaginatedResult<TransactionMain[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    return this.http.post<any>(this.baseUrl + 'input/filterQrCodeAgain/', text, { observe: 'response', params })
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

  missingPrintFilter(page?, itemsPerPage?, text?: FilterMissingParam): Observable<PaginatedResult<TransactionMain[]>> {
    const paginatedResult: PaginatedResult<TransactionMain[]> = new PaginatedResult<TransactionMain[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    return this.http.post<any>(this.baseUrl + 'input/filterMissingPrint/', text, { observe: 'response', params })
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

  findMaterialName(materialID: string) {
    return this.http.get<any>(this.baseUrl + 'input/findMaterialName/' + materialID, {});
  }
  findMiss(qrCode: string) {
    return this.http.get<any>(this.baseUrl + 'input/findMiss/' + qrCode, {});
  }
}
