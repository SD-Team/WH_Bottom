import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { PackingDetailResult } from '../_viewmodels/packing-detail-result';
import { PackingPrintAll } from '../_viewmodels/packing-print-all';

@Injectable({
  providedIn: 'root'
})
export class PackingListDetailService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  findByReceive(receive: string): Observable<PackingDetailResult> {
    return this.http.get<any>(this.baseUrl + 'packingListDetail/findByReceive/' + receive, {});
  }
  findByRecevieNoList(receives: string[]): Observable<PackingPrintAll[]> {
    return this.http.post<any>(this.baseUrl + 'packingListDetail/findPrint/', receives);
  }
}
