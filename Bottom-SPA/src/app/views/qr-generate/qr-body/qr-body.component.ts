import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QrcodeMainService } from '../../../_core/_services/qrcode-main.service';
import { QRCodeMainModel } from '../../../_core/_viewmodels/qrcode-main-model';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { PackingDetailResult } from '../../../_core/_viewmodels/packing-detail-result';
import { PackingListDetailService } from '../../../_core/_services/packing-list-detail.service';
import { PackingListDetailModel } from '../../../_core/_viewmodels/packing-list-detail-model';
import { PackingPrintAll } from '../../../_core/_viewmodels/packing-print-all';
declare var $: any;
import * as _ from 'lodash';
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
  listQrCodeMainModel: QRCodeMainModel[] = [];
  packingDetailResult: PackingDetailResult;
  packingDetailResultList: PackingDetailResult[] = [];
  packingListDetail: PackingListDetailModel[] = [];
  packingListDetailAll: PackingListDetailModel[][] = [];
  time_start: string;
  time_end: string;
  mO_No: string;
  qrCodeMainItem: QRCodeMainModel;
  totalQty: number;
  totalQtyList: number[] = [];
  checkArray: any[] = [];
  packingPrintAll: PackingPrintAll[] = [];
  // ------print qr code----------------------
  elementType: 'url' | 'canvas' | 'img' = 'url';
  // -----------------------------------------
  constructor(private router: Router,
              private qrCodeMainService: QrcodeMainService,
              private packingListDetailService: PackingListDetailService,
              private alertifyService: AlertifyService) { }

  ngOnInit() {
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
      // Nếu khi đã vừa print
      if (window.sessionStorage.getItem('checkPrint') === '1') {
        this.search();
        let modelSearch = JSON.parse(window.sessionStorage.getItem('modelSearch'));
        // Set lại giá trị date khi mới in xong.
        this.time_start = this.convertDate(modelSearch.from_Date);
        this.time_end =  this.convertDate(modelSearch.to_Date);
      }
      // window.sessionStorage.clear();
  }
  search() {
    // Khi chưa print
    if(window.sessionStorage.getItem('checkPrint') === null) {
      if (this.time_start === undefined || this.time_end === undefined) {
        this.alertifyService.error('Please option start and end time');
      } else {
        let form_date = new Date(this.time_start).toLocaleDateString();
        let to_date = new Date(this.time_end).toLocaleDateString();
        if (this.mO_No === undefined) {
          this.mO_No = null;
        }
        let object = {
          mO_No: this.mO_No,
          from_Date: form_date,
          to_Date: to_date
        };
        window.sessionStorage.setItem('modelSearch', JSON.stringify(object));
        this.qrCodeMainService.search(this.pagination.currentPage , this.pagination.itemsPerPage, object)
        .subscribe((res: PaginatedResult<QRCodeMainModel[]>) => {
          this.listQrCodeMainModel = res.result;
          this.pagination = res.pagination;
        }, error => {
          this.alertifyService.error(error);
        });
      }
    } else {
      // Khi đã vừa print xong.
      this.qrCodeMainService.search(this.pagination.currentPage , this.pagination.itemsPerPage, JSON.parse(window.sessionStorage.getItem('modelSearch')))
        .subscribe((res: PaginatedResult<QRCodeMainModel[]>) => {
          this.listQrCodeMainModel = res.result;
          this.pagination = res.pagination;
        }, error => {
          this.alertifyService.error(error);
        });
        window.sessionStorage.removeItem('checkPrint');
    }
  }
  print(qrCodeMain) {
      window.sessionStorage.setItem('checkPrint', '1');
      this.qrCodeMainItem =  qrCodeMain;
      this.packingListDetailService.findByReceive(this.qrCodeMainItem.receive_No).subscribe(res => {
        this.packingDetailResult = res;
        this.totalQty = this.packingDetailResult.totalQty;
        this.packingListDetail = this.packingDetailResult.packingListDetailModel;
      })
      let self = this;
      setTimeout(function(){
        self.printHtml('wrap-print');
        window.location.reload();
      },1000);
  }
  printHtml(divID: string) {
      let printContents = document.getElementById(divID).innerHTML;
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
    let arrayCheck = [];
    if (e.target.checked) {
      $('input:checkbox').not(this).prop('checked', true);
      this.listQrCodeMainModel.forEach(item => {
        arrayCheck.push(item.qrCode_ID);
      });
      this.checkArray.length = 0;
      this.checkArray = arrayCheck;
    } else {
      $('input:checkbox').not(this).prop('checked', false);
      this.checkArray.length = 0;
    }
  }
  onCheckboxChange(e) {
    let arrayCheck = [];
    if (e.target.checked) {
      this.checkArray.push(e.target.value);
      this.listQrCodeMainModel.forEach(item => {
        arrayCheck.push(item.qrCode_ID);
      });
      let difference = _.difference(arrayCheck,this.checkArray);
      if (difference.length < 1) {
        $('#all').prop('checked', true);
      }
    } else {
      $('#all').prop('checked', false);
      let i = this.checkArray.findIndex(element => element === e.target.value);
      this.checkArray.splice(i, 1);
    }
  }
  printAll() {
    window.sessionStorage.setItem('checkPrint', '1');
    this.totalQtyList.length = 0;
    this.packingListDetailAll.length = 0;
    console.log(this.checkArray);
    if (this.checkArray.length > 0) {
      this.packingListDetailService.findByRecevieNoList(this.checkArray).subscribe(res => {
        this.packingPrintAll = res;
        console.log(this.packingPrintAll);
      });
      let self = this;
      setTimeout(function(){
        self.printHtml('wrap-print-all');
        window.location.reload();
      },2000);
    } else {
      this.alertifyService.error('Please check in checkbox!');
    }
  }
  convertDate(dateString: string) {
    let arrayDate = dateString.split('/');
    let date = arrayDate[2] + '/' + arrayDate[0] + '/' + arrayDate[1];
    return date;
  }
  back() {
    window.sessionStorage.clear();
    this.router.navigate(['/qr/main']);
  }
}
