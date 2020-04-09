import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { MaterialSearch } from '../_viewmodels/material-search';
import { Observable, BehaviorSubject } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { MaterialModel } from '../_viewmodels/material-model';
import { BatchQtyItem } from '../_viewmodels/batch-qty-item';
import { ReceiveNoMain } from '../_viewmodels/receive_no_main';
import { ReceiveNoDetail } from '../_viewmodels/receive-no-detail';

@Injectable({
  providedIn: 'root'
})
export class MaterialService {
  baseUrl = environment.apiUrl;
  materialModel: MaterialModel;
  materialSource = new BehaviorSubject<MaterialModel>(this.materialModel);
  receiveNoMainSource = new BehaviorSubject<ReceiveNoMain[]>([]);
  receiveNoDetailSource = new BehaviorSubject<ReceiveNoDetail[]>([]);
  currentMaterial = this.materialSource.asObservable();
  currentReceiveNoMain = this.receiveNoMainSource.asObservable();
  currentReceiveNoDetail = this.receiveNoDetailSource.asObservable();
  constructor(private http: HttpClient) { }
  // search(page?, itemsPerPage?, materialSearch?: MaterialSearch): Observable<PaginatedResult<MaterialModel[]>> {
  //   const paginatedResult: PaginatedResult<MaterialModel[]> = new PaginatedResult<MaterialModel[]>();
  //   let params = new HttpParams();
  //   if (page != null && itemsPerPage != null) {
  //     params = params.append('pageNumber', page);
  //     params = params.append('pageSize', itemsPerPage);
  //   }
  //   let url = this.baseUrl + 'materialPurchase/search/';
  //   return this.http.post<any>(url, materialSearch, {observe: 'response', params})
  //   .pipe(
  //     map(response => {
  //       paginatedResult.result = response.body;
  //       if (response.headers.get('Pagination') != null) {
  //         paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
  //       }
  //       return paginatedResult;
  //     })
  //   );
  // }
  search(materialSearch: MaterialSearch): Observable<MaterialModel[]> {
    return this.http.post<any>(this.baseUrl + 'receiving/search/', materialSearch);
  }
  changeMaterialModel(materialModel: MaterialModel) {
    this.materialSource.next(materialModel);
  }
  changeReceiveNoMain(receiveNoMain: ReceiveNoMain[]) {
    this.receiveNoMainSource.next(receiveNoMain);
  }
  changeReceiveNoDetail(receiveNoDetails: ReceiveNoDetail[]) {
    this.receiveNoDetailSource.next(receiveNoDetails);
  }
  searchByPurchase(model: MaterialModel) {
    return this.http.post<any>(this.baseUrl + 'receiving/searchTable/', model);
  }
  updateMaterial(model: BatchQtyItem[]): Observable<ReceiveNoMain[]> {
    return this.http.post<any>(this.baseUrl + 'receiving/updateMaterial/', model);
  }
  receiveNoDetails(receiveNo: any): Observable<ReceiveNoDetail[]> {
    return this.http.get<ReceiveNoDetail[]>(this.baseUrl + 'receiving/receiveNoDetails/' + receiveNo, {});
  }
  purchaseNoDetail(materialModel: MaterialModel): Observable<ReceiveNoMain[]> {
    return this.http.post<any>(this.baseUrl + 'receiving/purchaseNoDetail', materialModel);
  }
}
