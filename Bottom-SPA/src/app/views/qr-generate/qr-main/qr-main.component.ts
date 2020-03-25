import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { QrGenerate } from '../../../_core/_models/qr-generate';
import { Router } from '@angular/router';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { PackingListService } from '../../../_core/_services/packing-list.service';
import { PackingList } from '../../../_core/_models/packingList';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';

@Component({
  selector: 'app-qr-main',
  templateUrl: './qr-main.component.html',
  styleUrls: ['./qr-main.component.scss']
})
export class QrMainComponent implements OnInit {
  pagination: Pagination;
  test: Pagination;
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
  constructor(private router: Router,
              private packingListService: PackingListService,
              private alertifyService: AlertifyService) { }

  ngOnInit() {
    // tslint:disable-next-line:prefer-const
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3,
      totalItems: 0,
      totalPages: 0
    };
    this.bsConfig = Object.assign({}, { containerClass: 'theme-blue' });
  }

  search() {
    console.log("Search")
    this.clickSearch = true;
    if (this.time_start === undefined || this.time_end === undefined) {
      this.alertifyService.error('Please option start and end time');
    } else if (this.supplier_ID === '' || this.supplier_ID === undefined) {
      this.alertifyService.error('Please enter Supplier No');
    } else if (this.mO_No === '' || this.mO_No === undefined) {
      this.alertifyService.error('Please enter Plan No');
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
      this.packingListService.findBySupplier(this.supplier_ID).subscribe(res => {
        this.supplier_Name = res.supplier_Name;
      });
      this.packingListService.search(this.pagination.currentPage , this.pagination.itemsPerPage, object)
      .subscribe((res: PaginatedResult<PackingList[]>) => {
        this.packingLists = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertifyService.error(error);
      });
    }
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.search();
  }
  pageQrCode() {
    this.router.navigate(['/qr/print']);
  }
  cancel() {
    this.clickSearch = false;
    this.packingLists.length = 0;
  }
}
