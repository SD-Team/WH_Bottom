import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class QrcodeDetailService {
  urlBase = environment.apiUrl;
  constructor(private http: HttpClient) { }
  genareQrCodeDetail(listData: string[]) {
    return this.http.post<any>(this.urlBase + 'QRCodeDetail/', listData);
  }
}
