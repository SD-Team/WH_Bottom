import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { Router } from '@angular/router';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { PackingListService } from '../../../_core/_services/packing-list.service';
import { PackingList } from '../../../_core/_models/packingList';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { QrcodeMainService } from '../../../_core/_services/qrcode-main.service';
import { PackingSearch } from '../../../_core/_viewmodels/packing-search';
import { InputService } from '../../../_core/_services/input.service';
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
  packingSearchParam: PackingSearch;
  packingLists: PackingList[];
  supplier_ID: string;
  mO_No: string;
  supplier_Name: string;
  checkArray: any[] = [];
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
              private packingListService: PackingListService,
              private inputService: InputService,
              private qrcodeService: QrcodeMainService,
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
    this.getDataLoadPage();
    this.inputService.clearDataChangeMenu();
  }
  changeSupplier() {
    if (this.supplier_ID !== undefined && this.supplier_ID !== '') {
      this.packingListService.findBySupplier(this.supplier_ID).subscribe(res => {
        if (res === null) {
          this.supplier_Name = '';
        } else {
          this.supplier_Name = res.supplier_Name;
        }
      });
    }
  }
  getDataLoadPage() {
    let form_date = new Date(this.time_start).toLocaleDateString();
    let to_date = new Date(this.time_end).toLocaleDateString();
    this.packingSearchParam = {
      supplier_ID: '',
      mO_No: '',
      from_Date: form_date,
      to_Date: to_date
    };
    this.packingListService.search(this.pagination.currentPage , this.pagination.itemsPerPage, this.packingSearchParam)
    .subscribe((res: PaginatedResult<PackingList[]>) => {
      this.packingLists = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertifyService.error(error);
    });
  }
  search() {
    if (this.time_start === undefined || this.time_end === undefined) {
      this.alertifyService.error('Please option start and end time');
    } else {
      let form_date = new Date(this.time_start).toLocaleDateString();
      let to_date = new Date(this.time_end).toLocaleDateString();
      this.packingSearchParam = {
        supplier_ID: this.supplier_ID,
        mO_No: this.mO_No,
        from_Date: form_date,
        to_Date: to_date
      };
      this.packingListService.search(this.pagination.currentPage , this.pagination.itemsPerPage, this.packingSearchParam)
      .subscribe((res: PaginatedResult<PackingList[]>) => {
        this.packingLists = res.result;
        this.pagination = res.pagination;
        if(this.packingLists.length === 0) {
          this.alertifyService.error('No Data!');
        }
      }, error => {
        this.alertifyService.error(error);
      });
    }
  }
  onCheckboxChange(e) {
    if (e.target.checked) {
      this.checkArray.push(e.target.value);
    } else {
      let i = this.checkArray.findIndex(element => element === e.target.value);
      this.checkArray.splice(i, 1);
    }
  }
  // Khi stick chọn all checkbox
  checkAll(e) {
    let arrayCheck = [];
    if (e.target.checked) {
      this.packingLists.forEach(element => {
        let ele =  document.getElementById(element.receive_No.toString()) as HTMLInputElement;
        ele.checked = true;
        this.checkArray.length = 0;
        arrayCheck.push(element.receive_No);
      });
    } else {
      this.packingLists.forEach(element => {
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
    if (this.checkArray.length > 0) {
      this.qrcodeService.generateQrCode(this.checkArray).subscribe(res => {
        this.alertifyService.success('Generate QRCode successed!');
        this.router.navigate(['/qr/body']);
        // this.search();
      }, error => {
        this.alertifyService.error(error);
      });
    } else {
      this.alertifyService.error('Please check checkbox!');
    }
  }
  cancel() {
    this.clickSearch = false;
    this.packingLists.length = 0;
  }
}
