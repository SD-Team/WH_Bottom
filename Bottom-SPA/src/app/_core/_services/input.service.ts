import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { InputM } from '../_models/inputM';

@Injectable({
  providedIn: 'root'
})
export class InputService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getMainByQrCodeID(qrCodeID: string) {
    return this.http.get<InputM>(this.baseUrl + 'input/' + qrCodeID, {});
  }
}
