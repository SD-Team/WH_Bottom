import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { QrGenerate } from '../../../_core/_models/qr-generate';
import { Router } from '@angular/router';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { PackingListService } from '../../../_core/_services/packing-list.service';
import { PackingList } from '../../../_core/_models/packingList';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { QrcodeMainService } from '../../../_core/_services/qrcode-main.service';
import { QrcodeDetailService } from '../../../_core/_services/qrcode-detail.service';

@Component({
  selector: 'app-qr-main',
  templateUrl: './qr-main.component.html',
  styleUrls: ['./qr-main.component.scss']
})
export class QrMainComponent implements OnInit {
  pagination: Pagination;
  bsConfig: Partial<BsDatepickerConfig>;
  time_start: string;
  time_end: string;
  fromDate = new Date();
  toDate = new Date();
  clickSearch: boolean = false;
  packingLists: PackingList[] = [];
  supplier_ID: string;
  mO_No: string;
  supplier_Name: string;
  checkArray: any[] = [];
  constructor(private router: Router,
              private packingListService: PackingListService,
              private qrcodeService: QrcodeMainService,
              private alertifyService: AlertifyService) { }

  ngOnInit() {
    // tslint:disable-next-line:prefer-const
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
  changeSupplier() {
    if (this.supplier_ID !== undefined) {
      this.packingListService.findBySupplier(this.supplier_ID).subscribe(res => {
        this.supplier_Name = res.supplier_Name;
      });
    }
  }
  search() {
    if (this.time_start === undefined || this.time_end === undefined) {
      this.alertifyService.error('Please option start and end time');
    } else {
       // tslint:disable-next-line:prefer-const
      let form_date = new Date(this.time_start).toLocaleDateString();
       // tslint:disable-next-line:prefer-const
      let to_date = new Date(this.time_end).toLocaleDateString();
      // tslint:disable-next-line:prefer-const
      let object = {
        supplier_ID: this.supplier_ID,
        mO_No: this.mO_No,
        from_Date: form_date,
        to_Date: to_date
      };
      this.packingListService.search(this.pagination.currentPage , this.pagination.itemsPerPage, object)
      .subscribe((res: PaginatedResult<PackingList[]>) => {
        this.packingLists = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertifyService.error(error);
      });
    }
  }
  onCheckboxChange(e) {
    if (e.target.checked) {
      this.checkArray.push(e.target.value);
      // console.log(e.target.id);
    } else {
      // tslint:disable-next-line:prefer-const
      let i = this.checkArray.findIndex(element => element === e.target.value);
      this.checkArray.splice(i, 1);
    }
  }
  // Khi stick chọn all checkbox
  checkAll(e) {
    // tslint:disable-next-line:prefer-const
    let arrayCheck = [];
    if (e.target.checked) {
      this.packingLists.forEach(element => {
        // tslint:disable-next-line:prefer-const
        let ele =  document.getElementById(element.receive_No.toString()) as HTMLInputElement;
        ele.checked = true;
        this.checkArray.length = 0;
        arrayCheck.push(element.receive_No);
      });
    } else {
      this.packingLists.forEach(element => {
        // tslint:disable-next-line:prefer-const
        let ele =  document.getElementById(element.receive_No.toString()) as HTMLInputElement;
        ele.checked = false;
        arrayCheck.length = 0;
      });
    }
    this.checkArray = arrayCheck;
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.search();
  }
  // genare QrCode
  pageQrCode() {
    // tslint:disable-next-line:prefer-const
    if (this.checkArray.length > 0) {
      this.qrcodeService.generateQrCode(this.checkArray).subscribe(res => {
        this.alertifyService.success('Generate QRCode succed!');
        // this.search();
      }, error => {
        this.alertifyService.error(error);
      });
    } else {
      this.alertifyService.error('Please check checkbox!');
    }
  }
  qrCodeBody() {
    this.router.navigate(['/qr/body']);
  }
  cancel() {
    this.clickSearch = false;
    this.packingLists.length = 0;
  }
}
