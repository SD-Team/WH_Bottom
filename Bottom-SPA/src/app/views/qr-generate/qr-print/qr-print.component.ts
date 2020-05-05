import { Component, OnInit } from '@angular/core';
import { PackingListDetailService } from '../../../_core/_services/packing-list-detail.service';
import { PackingPrintAll } from '../../../_core/_viewmodels/packing-print-all';
import { Router } from '@angular/router';

@Component({
  selector: 'app-qr-print',
  templateUrl: './qr-print.component.html',
  styleUrls: ['./qr-print.component.scss']
})
export class QrPrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  packingPrint: PackingPrintAll[] = [];
  printQrCodeAgain: string;
  titleForm: string;
  constructor(private packingListDetailService: PackingListDetailService,
              private router: Router) { }

  ngOnInit() {
    this.packingListDetailService.currentPackingPrint.subscribe(res => this.packingPrint = res);
    this.packingListDetailService.currentPrintQrCodeAgain.subscribe(res => this.printQrCodeAgain = res);
    if (this.printQrCodeAgain === '0' || this.printQrCodeAgain === '1') {
      this.titleForm = 'Material Form';
    } else {
      this.titleForm = 'Sorting Form';
    }
    this.checkPackingPrint();
  }
  checkPackingPrint() {
    if (this.packingPrint.length === 0) {
      this.router.navigate(['/qr/body']);
    }
  }
  back() {
    if (this.printQrCodeAgain === '0') {
      this.router.navigate(['/qr/body']);
    } else if(this.printQrCodeAgain === '1') {
      this.router.navigate(['/input/qrcode-again']);
    } else if(this.printQrCodeAgain === '2') {
      this.router.navigate(['/output/main']);
    } else {

    }
  }
}
