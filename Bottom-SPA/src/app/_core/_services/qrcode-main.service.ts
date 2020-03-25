import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { QRCodeMain } from '../_models/qrcode-main';

@Injectable({
  providedIn: 'root'
})
export class QrcodeMainService {

constructor(private http: HttpClient) { }
  urlBase = environment.apiUrl;
  generateQrCode(listData: string[]) {
    return this.http.post<any>(this.urlBase + 'qRCodeMain/', listData);
  }

}
