import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { OutputM } from '../_models/outputM';

@Injectable({
  providedIn: 'root'
})
export class OutputService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getMainByQrCodeId(qrCodeId: string) {
    return this.http.get<OutputM[]>(this.baseUrl + 'Output/GetByQrCodeId', {params: {qrCodeId: qrCodeId}});
  }
}
