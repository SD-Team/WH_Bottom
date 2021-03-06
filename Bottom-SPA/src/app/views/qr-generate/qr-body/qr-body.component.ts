import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QrcodeMainService } from '../../../_core/_services/qrcode-main.service';
import { QRCodeMainModel } from '../../../_core/_viewmodels/qrcode-main-model';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { PackingListDetailService } from '../../../_core/_services/packing-list-detail.service';
import { PackingListDetailModel } from '../../../_core/_viewmodels/packing-list-detail-model';
import { PackingPrintAll } from '../../../_core/_viewmodels/packing-print-all';
declare var $: any;
import * as _ from 'lodash';
import { QRCodeMainSearch } from '../../../_core/_viewmodels/qrcode-main-search';
import { InputService } from '../../../_core/_services/input.service';
import { FunctionUtility } from '../../../_core/_utility/function-utility';
@Component({
  selector: 'app-qr-body',
  templateUrl: './qr-body.component.html',
  styleUrls: ['./qr-body.component.scss']
})
export class QrBodyComponent implements OnInit {
  pagination: Pagination;
  bsConfig: Partial<BsDatepickerConfig>;
  listQrCodeMainModel: QRCodeMainModel[] = [];
  listQrCodeMainModelAll: QRCodeMainModel[] = [];
  packingListDetailAll: PackingListDetailModel[][] = [];
  time_start: string;
  time_end: string;
  mO_No: string;
  qrCodeMainItem: QRCodeMainModel;
  totalQty: number;
  qrCodeMainSearch: QRCodeMainSearch;
  totalQtyList: number[] = [];
  checkArray: any[] = [];
  packingPrintAll: PackingPrintAll[] = [];
  alerts: any = [
    {
      type: 'success',
      msg: `You successfully read this important alert message.`
    },
    {
      type: 'info',
      msg: `This alert needs your attention, but it's not super important.`
    },
    {
      type: 'danger',
      msg: `Better check yourself, you're not looking too good.`
    }
  ];
  constructor(private router: Router,
              private qrCodeMainService: QrcodeMainService,
              private packingListDetailService: PackingListDetailService,
              private inputService: InputService,
              private alertifyService: AlertifyService,
              private functionUtility: FunctionUtility) { }

  ngOnInit() {
      this.pagination = {
        currentPage: 1,
        itemsPerPage: 3,
        totalItems: 0,
        totalPages: 0
      };
      this.getTimeNow();
      // this.bsConfig = Object.assign({}, { containerClass: 'theme-blue' });
      this.bsConfig = Object.assign(
        {},
        {
          containerClass: 'theme-blue',
          isAnimated: true,
          dateInputFormat: 'YYYY/MM/DD',
        }
      );
      this.qrCodeMainService.currentQrCodeMainSearch.subscribe(res => this.qrCodeMainSearch = res);
      if (this.qrCodeMainSearch === undefined) {
        this.getDataLoadPage();
      } else {
        this.mO_No = this.qrCodeMainSearch.mO_No;
        this.time_start = this.qrCodeMainSearch.from_Date;
        this.time_end = this.qrCodeMainSearch.to_Date;
        this.search();
      }
      this.inputService.clearDataChangeMenu();
  }
  getTimeNow() {
     // Lấy ngày hiện tại
      const timeNow = new Date().getFullYear().toString() +
      '/' + (new Date().getMonth() + 1).toString() +
      '/' + new Date().getDate().toString();
      this.time_start = timeNow;
      this.time_end = timeNow;
      let form_date = this.functionUtility.getDateFormat(new Date(this.time_start));
      let to_date = this.functionUtility.getDateFormat(new Date(this.time_end));
      this.qrCodeMainSearch = {
        mO_No: '',
        from_Date: form_date,
        to_Date: to_date
      }
      this.qrCodeMainService.changeQrCodeMainSearch(this.qrCodeMainSearch);
  }
  getData() {
    this.qrCodeMainService.search(this.pagination.currentPage , this.pagination.itemsPerPage, this.qrCodeMainSearch)
    .subscribe((res: PaginatedResult<QRCodeMainModel[]>) => {
      this.listQrCodeMainModel = res.result;
      this.pagination = res.pagination;
      if(this.listQrCodeMainModel.length === 0) {
        this.alertifyService.error('No Data!');
      }
    }, error => {
      this.alertifyService.error(error);
    });
  }
  getDataLoadPage() {
    let form_date = this.functionUtility.getDateFormat(new Date(this.time_start));
    let to_date = this.functionUtility.getDateFormat(new Date(this.time_end));
    if (this.mO_No === undefined) {
      this.mO_No = null;
    }
    this.qrCodeMainSearch = {
      mO_No: '',
      from_Date: form_date,
      to_Date: to_date
    };
    this.getData();
  }
  search() {
    this.pagination.currentPage = 1;
        let checkSearch = true;
        if (this.time_start !== null) {
          if (this.time_end === null) {
            this.alertifyService.error('Please option time end!');
            checkSearch = false;
          }
        }
        if(checkSearch) {
          if (this.time_start === null) {
            this.qrCodeMainSearch = {mO_No: this.mO_No,from_Date: null,to_Date: null};
          } else {
            let form_date = this.functionUtility.getDateFormat(new Date(this.time_start));
            let to_date = this.functionUtility.getDateFormat(new Date(this.time_end));
            this.qrCodeMainSearch = {mO_No: this.mO_No,from_Date: form_date,to_Date: to_date};
          }
          if (this.mO_No === undefined) {
            this.mO_No = null;
          }
          this.qrCodeMainService.changeQrCodeMainSearch(this.qrCodeMainSearch);
          this.getData();
      }
  }
  print(qrCodeMain) {
      this.qrCodeMainItem =  qrCodeMain;
      let qrCodeId = [];
      let qrCodeIDItem = {
        qrCode_ID: qrCodeMain.qrCode_ID,
        qrCode_Version: qrCodeMain.qrCode_Version
      };
      qrCodeId.push(qrCodeIDItem);
      this.packingListDetailService.printMaterialForm(qrCodeId).subscribe(res => {
        this.packingPrintAll = res;
        this.packingListDetailService.changePackingPrint(this.packingPrintAll);
        this.packingListDetailService.changePrintQrCodeAgain('0');
        this.router.navigate(['/qr/print']);
      })
  }
  pageChanged(event: any): void {
    debugger
    this.qrCodeMainService.currentQrCodeMainSearch.subscribe(res => {
      this.qrCodeMainSearch = res;
    })
    this.pagination.currentPage = event.page;
    this.getData();
  }
  checkAll(e) {
    let arrayCheck = [];
    if (e.target.checked) {
      this.qrCodeMainService.searchNotPagination(this.qrCodeMainSearch).subscribe(res => {
        this.listQrCodeMainModelAll = res;
        this.listQrCodeMainModelAll.forEach(element => {
          arrayCheck.push(element.qrCode_ID);
        });
      });
    } else {
      this.listQrCodeMainModel.forEach(element => {
        let ele =  document.getElementById(element.qrCode_ID.toString()) as HTMLInputElement;
        ele.checked = false;
        arrayCheck.length = 0;
      });
    }
    this.checkArray = arrayCheck;
    console.log(this.checkArray);
  }
  onCheckboxChange(e) {
    let arrayCheck = [];
    if (e.target.checked) {
      this.checkArray.push(e.target.value);
      this.listQrCodeMainModelAll.forEach(item => {
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
    this.totalQtyList.length = 0;
    this.packingListDetailAll.length = 0;
    let qrCodeVersionList = [];
    if (this.checkArray.length > 0) {
      this.checkArray.forEach(element => {
        this.listQrCodeMainModelAll.forEach(element1 => {
          if (element1.qrCode_ID === element) {
            let item = {
              qrCode_ID: element1.qrCode_ID,
              qrCode_Version: element1.qrCode_Version
            }
            qrCodeVersionList.push(item);
          }
        });
      });
      this.packingListDetailService.printMaterialForm(qrCodeVersionList).subscribe(res => {
        this.packingPrintAll = res;
        this.packingListDetailService.changePackingPrint(this.packingPrintAll);
        this.packingListDetailService.changePrintQrCodeAgain('0');
        this.router.navigate(['/qr/print']);
      });
    } else {
      this.alertifyService.error('Please check in checkbox!');
    }
  }
  cancel() {
    this.getTimeNow();
    this.mO_No = '';
    this.listQrCodeMainModel= [];
  }
  ngAfterViewChecked() {
    this.checkArray.forEach(element => {
      let ele =  document.getElementById(element.toString()) as HTMLInputElement;
        if(ele) {
          ele.checked = true;
      }
    })
  }
}
