import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { WmsCode } from '../_models/wms-code';

@Injectable({
  providedIn: 'root'
})
export class RackService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getFactories() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/factory', {});
  }

  getWHs() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/wh', {});
  }

  getBuildings() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/building', {});
  }

  getFloors() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/floor', {});
  }

  getAreas() {
    return this.http.get<WmsCode[]>(this.baseUrl + 'codeIDDetail/area', {});
  }


  filter(param: Object) {
    console.log("Service: ",param);
    return this.http.post(this.baseUrl + 'rackLocation/', param);
  }

  
}
