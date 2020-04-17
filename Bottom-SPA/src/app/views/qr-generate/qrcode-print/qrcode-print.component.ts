import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { QrcodeMainService } from '../../../_core/_services/qrcode-main.service';
import { TransferDetail } from '../../../_core/_models/transfer-detail';

@Component({
  selector: 'app-qrcode-print',
  templateUrl: './qrcode-print.component.html',
  styleUrls: ['./qrcode-print.component.scss']
})
export class QrcodePrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  today = new Date();
  qrCodeId: string = 'B0124778453020';
  qrCodeVersion: number = 1;
  qrCodePrint: any = [];
  trasnferDetail: TransferDetail[] = [];
  totalQty = 0;

  constructor(private router: Router, private qrcodeMainService: QrcodeMainService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.qrCodeId = this.route.snapshot.params['qrCodeId'];
    this.qrCodeVersion = this.route.snapshot.params['qrCodeVersion'];
    this.getQrCodePrint();
  }

  back() {
    this.router.navigate(['/input/qrcode-again']);
  }

  print(e) {
    e.preventDefault();
    const printContents = document.getElementById('wrap-print').innerHTML;
    const originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
  }

  getQrCodePrint() {
    this.qrcodeMainService.printQrCode(this.qrCodeId, this.qrCodeVersion).subscribe(res => {
      this.qrCodePrint = res.packingListByQrCodeId;
      this.trasnferDetail = res.transactionDetailByQrCodeId;

      this.totalQty = this.trasnferDetail.reduce((qty, i) => {
        return qty += i.qty;
      }, 0);
    });
  }

}
