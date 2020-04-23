import { Component, OnInit } from '@angular/core';
import { Receipt } from '../../../_core/_models/receipt';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { MaterialService } from '../../../_core/_services/material.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { PackingListService } from '../../../_core/_services/packing-list.service';
import { Material } from '../../../_core/_models/material';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';
import { ReceiveNoMain } from '../../../_core/_viewmodels/receive_no_main';
import { AlertConfig } from 'ngx-bootstrap/alert';// 
import * as _ from 'lodash'; 
@Component({
  selector: 'app-receipt-main',
  templateUrl: './receipt-main.component.html',
  styleUrls: ['./receipt-main.component.scss']
})
export class ReceiptMainComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
  pagination: Pagination;
  time_start: string;
  time_end: string;
  fromDate = new Date();
  toDate = new Date();
  mO_No: string;
  supplier_ID: string;
  supplier_Name: string;
  materialLists: MaterialModel[];
  receiveNoMain: ReceiveNoMain[];
  status: string = 'all';
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

  constructor(private materialService: MaterialService,
              private packingListService: PackingListService,
              private router: Router,
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
  }
  changeSupplier() {
    if (this.supplier_ID !== undefined) {
      this.packingListService.findBySupplier(this.supplier_ID).subscribe(res => {
        this.supplier_Name = res.supplier_Name;
      });
    }
  }
  // search() {
  //   if (this.time_start === undefined || this.time_end === undefined) {
  //     this.alertifyService.error('Please option start and end time');
  //   } else {
  //     let form_date = new Date(this.time_start).toLocaleDateString();
  //     let to_date = new Date(this.time_end).toLocaleDateString();
  //     let object = {
  //       supplier_ID: this.supplier_ID,
  //       purchase_No: this.purchase_No,
  //       from_Date: form_date,
  //       to_Date: to_date,
  //       status: this.status
  //     };
  //     this.materialService.search(this.pagination.currentPage , this.pagination.itemsPerPage, object)
  //     .subscribe((res: PaginatedResult<MaterialModel[]>) => {
  //       this.materialLists = res.result;
  //       this.pagination = res.pagination;
  //     }, error => {
  //       this.alertifyService.error(error);
  //     });
  //   }
  // }
  search() {
    if (this.time_start === undefined || this.time_end === undefined) {
      this.alertifyService.error('Please option start and end time');
    } else {
      let form_date = new Date(this.time_start).toLocaleDateString();
      let to_date = new Date(this.time_end).toLocaleDateString();
      let object = {
        supplier_ID: this.supplier_ID,
        mO_No: this.mO_No,
        from_Date: form_date,
        to_Date: to_date,
        status: this.status
      };
      this.materialService.search(object)
      .subscribe(res => {
        this.materialLists = res;
        if(this.materialLists.length === 0) {
          this.alertifyService.error('No Data!');
        }
        console.log(this.materialLists.length);
      }, error => {
        this.alertifyService.error(error);
      });
    }
  }
  changePageAdd(materialModel) {
    this.materialService.changeMaterialModel(materialModel);
    this.router.navigate(['receipt/record']);
  }
  changeStatus(materialModel) {
    this.alertifyService.confirm('Close Purchase No', 'Are you sure Close ?', () => {
      this.materialService.closePurchase(materialModel).subscribe(res => {
        this.materialLists.forEach(item => {
          if (_.isEqual(item,materialModel)) {
            item.status = 'Y';
            return;
          }
        });
        this.alertifyService.success('Close successed!');
      }, error => {
        this.alertifyService.error(error);
      });
    });
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.search();
  }
}
