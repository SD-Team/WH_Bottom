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
import { FunctionUtility } from '../../../_core/_utility/function-utility';
import { element } from 'protractor';
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
  packingListsAll: PackingList[];
  mO_No: string;
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
              private alertifyService: AlertifyService,
              private functionUtility: FunctionUtility) { }

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
  getData() {
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
  getDataLoadPage() {
    let form_date = this.functionUtility.getDateFormat(new Date(this.time_start));
    let to_date = this.functionUtility.getDateFormat(new Date(this.time_end));
    this.packingSearchParam = {
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
        checkSearch = false;
        this.alertifyService.error('Please option time end!')
      }
    } else {
      if (this.time_end !== null) {
        checkSearch = false;
        this.alertifyService.error('Please option time start!')
      }
    }
    if (this.time_start === null) {
      this.packingSearchParam = {
        mO_No: this.mO_No,
        from_Date: null,
        to_Date: null
      };
    } else {
      let form_date = this.functionUtility.getDateFormat(new Date(this.time_start));
      let to_date = this.functionUtility.getDateFormat(new Date(this.time_end));
      this.packingSearchParam = {
        mO_No: this.mO_No,
        from_Date: form_date,
        to_Date: to_date
      };
    }
    if(checkSearch) {
      this.getData();
    }
  }
  onCheckboxChange(e) {
    if (e.target.checked) {
      let check = 0;
      this.checkArray.push(e.target.value);
      this.packingListsAll.forEach(element => {
          let testCheck = this.checkArray.includes(element.receive_No.toString());
          if(testCheck === false) {
            check ++;
          }
      });
      if(check === 0) {
        let checkAll = document.getElementById('all') as HTMLInputElement;
        checkAll.checked = true;
      }
    } else {
      let i = this.checkArray.findIndex(element => element === e.target.value);
      this.checkArray.splice(i, 1);
      let checkAll = document.getElementById('all') as HTMLInputElement;
      checkAll.checked = false;
    }
  }
  // Khi stick chọn all checkbox
  checkAll(e) {
    let arrayCheck = [];
    if (e.target.checked) {
        this.packingListService.searchNotPagination(this.packingSearchParam).subscribe(res => {
          this.packingListsAll = res;
          this.checkArray.length = 0;
          this.packingListsAll.forEach(element1 => {
            arrayCheck.push(element1.receive_No);
          });
      })
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
    this.getData();
  }
  // genare QrCode
  pageQrCode() {
    console.log(this.checkArray);
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
  ngAfterViewChecked() {
    this.checkArray.forEach(element => {
      let ele =  document.getElementById(element.toString()) as HTMLInputElement;
        if(ele) {
          ele.checked = true;
      }
    })
  }
}
