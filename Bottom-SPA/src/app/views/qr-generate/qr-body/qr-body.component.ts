import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QrcodeMainService } from '../../../_core/_services/qrcode-main.service';
import { QRCodeMainModel } from '../../../_core/_viewmodels/qrcode-main-model';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { AlertifyService } from '../../../_core/_services/alertify.service';

@Component({
  selector: 'app-qr-body',
  templateUrl: './qr-body.component.html',
  styleUrls: ['./qr-body.component.scss']
})
export class QrBodyComponent implements OnInit {
  pagination: Pagination;
  fromDate = new Date();
  toDate = new Date();
  bsConfig: Partial<BsDatepickerConfig>;
  listQrCode: QRCodeMainModel[] = [];
  time_start: string;
  time_end: string;
  mO_No: string;
  qrCodeMainItem: QRCodeMainModel;
  // ------print qr code----------------------
  elementType: 'url' | 'canvas' | 'img' = 'url';
  TSizeArray: any[];
  sizeArray:  any[];
  PQty:  any[];
  RQty:  any[];
  Bal:  any[];
  // -----------------------------------------
  constructor(private router: Router,
              private qrCodeMainService: QrcodeMainService,
              private alertifyService: AlertifyService) { }

  ngOnInit() {
    this.TSizeArray = ['03.5', '03.5', '03.5', '03.5', '03.5', '03.5', '03.5', '03.5'];
    this.sizeArray = ['03.5', '04', '04.5', '05', '05.5', '06', '06', '06'];
    this.PQty = [300, 200, 300, 100, 100, 50, 50, 50];
    this.RQty = [300, 200, 300, 100, 100, 50, 50, 50];
    this.Bal = [0, 0, 0, 0, 0, 0, 0, 0];

    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3,
      totalItems: 0,
      totalPages: 0
    };
      // Lấy ngày hiện tại
      const timeNow = new Date().getFullYear().toString() +
      '/' + (new Date().getMonth() + 1).toString() +
      '/' + new Date().getDate().toString();
      this.time_start = timeNow;
      this.time_end = timeNow;
      this.bsConfig = Object.assign({}, { containerClass: 'theme-blue' });
  }
  search() {
    if (this.time_start === undefined || this.time_end === undefined) {
      this.alertifyService.error('Please option start and end time');
    } else {
      let form_date = new Date(this.time_start).toLocaleDateString();
      let to_date = new Date(this.time_end).toLocaleDateString();
      let object = {
        mO_No: this.mO_No,
        from_Date: form_date,
        to_Date: to_date
      };
      this.qrCodeMainService.search(this.pagination.currentPage , this.pagination.itemsPerPage, object)
      .subscribe((res: PaginatedResult<QRCodeMainModel[]>) => {
        this.listQrCode = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertifyService.error(error);
      });
    }
  }
    print(qrCodeMain) {
    this.qrCodeMainItem =  qrCodeMain; 
    let self = this;
    setTimeout(function(){
      self.printHtml();
      window.location.reload();
    },2000);
  }
  printHtml() {
      let printContents = document.getElementById('wrap-print').innerHTML;
      let originalContents = document.body.innerHTML;
      document.body.innerHTML = printContents;
      window.print();
      document.body.innerHTML = originalContents;
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.search();
  }
  checkAll(e) {
  }
  back() {
    this.router.navigate(['/qr/main']);
  }
}
