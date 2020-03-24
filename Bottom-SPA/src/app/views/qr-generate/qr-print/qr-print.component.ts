import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-qr-print',
  templateUrl: './qr-print.component.html',
  styleUrls: ['./qr-print.component.scss']
})
export class QrPrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  value: string = '0123456789';
  TSizeArray: any[];
  sizeArray:  any[];
  PQty:  any[];
  RQty:  any[];
  Bal:  any[];
  constructor() { }

  ngOnInit() {
    this.TSizeArray = ['03.5', '03.5', '03.5', '03.5', '03.5', '03.5', '03.5', '03.5'];
    this.sizeArray = ['03.5', '04', '04.5', '05', '05.5', '06', '06', '06'];
    this.PQty = [300, 200, 300, 100, 100, 50, 50, 50];
    this.RQty = [300, 200, 300, 100, 100, 50, 50, 50];
    this.Bal = [0, 0, 0, 0, 0, 0, 0, 0];
  }
  // generateQRCode() {
  //   if (this.qrcodename === '') {
  //     this.display = false;
  //     alert ('Please enter the name');
  //     return;
  //   } else {
  //     this.value = this.qrcodename;
  //     this.display = true;
  //   }
  // }
}
