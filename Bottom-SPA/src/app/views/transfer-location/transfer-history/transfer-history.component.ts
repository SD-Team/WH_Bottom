import { Component, OnInit, OnDestroy } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { Router, RouterStateSnapshot } from '@angular/router';

import { TransferM } from '../../../_core/_models/transferM';
import { TransferService } from '../../../_core/_services/transfer.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { FunctionUtility } from '../../../_core/_utility/function-utility';
import { TransferHistoryParam } from '../../../_core/_viewmodels/transfer-history-param';
import { Pagination } from '../../../_core/_models/pagination';

@Component({
  selector: 'app-transfer-history',
  templateUrl: './transfer-history.component.html',
  styleUrls: ['./transfer-history.component.scss'],
})
export class TransferHistoryComponent implements OnInit, OnDestroy {
  bsConfig: Partial<BsDatepickerConfig>;
  fromDate: string;
  toDate: string;
  transfers: TransferM[] = [];
  printArray: TransferM[] = [];
  status: string = '';
  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: 10,
    totalItems: 0,
    totalPages: 0
  };

  constructor(
    private transferService: TransferService,
    private router: Router,
    private alertify: AlertifyService,
    private functionUtility: FunctionUtility
  ) { }
  ngOnDestroy(): void {
    const t1 = this.functionUtility.getDateFormat(new Date(this.fromDate));
    const t2 = this.functionUtility.getDateFormat(new Date(this.toDate));
    const paramSearch = {
      currentPage: this.pagination.currentPage,
      fromDate: t1,
      toDate: t2,
      status: this.status
    };
    this.transferService.changeParamSearch(paramSearch);
  }

  ngOnInit() {
    this.bsConfig = Object.assign(
      {},
      {
        containerClass: 'theme-blue',
        isAnimated: true,
        dateInputFormat: 'YYYY/MM/DD',
      }
    );
    this.transferService.currentParamSearch.subscribe(res => {
      this.fromDate = res.fromDate;
      this.toDate = res.toDate;
      this.status = res.status;
      this.pagination.currentPage = res.currentPage;
    });
    this.getData();
  }

  getData() {
    const t1 = this.functionUtility.getDateFormat(new Date(this.fromDate));
    const t2 = this.functionUtility.getDateFormat(new Date(this.toDate));
    const transferHistoryParam = new TransferHistoryParam();
    transferHistoryParam.toDate = t2;
    transferHistoryParam.fromDate = t1;
    transferHistoryParam.status = this.status;
    this.transferService.search(this.pagination.currentPage, this.pagination.itemsPerPage, transferHistoryParam).subscribe((res) => {
      this.transfers = res.result;
      this.pagination = res.pagination;
    });
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.getData();
  }

  search() {
    this.pagination.currentPage = 1;
    this.getData();
  }

  // Mấy đoạn ở dưới là để in mà giờ chưa dùng tới để khi nào xài thì nhớ là mấy hàm ở dưới
  checkEle(e) {
    if (e.target.checked) {
      const transfer = this.transfers[e.target.value];
      this.printArray.push(transfer);
    } else {
      const i = this.printArray.findIndex(
        (element) => element === e.target.value
      );
      this.printArray.splice(i, 1);
    }
    const ele = document.getElementById('checkAll') as HTMLInputElement;
    if (this.printArray.length === this.transfers.length) {
      ele.checked = true;
    } else {
      ele.checked = false;
    }
  }

  checkAll(e) {
    this.printArray = [];
    if (e.target.checked) {
      this.transfers.forEach((element) => {
        const ele = document.getElementById(
          element.id.toString()
        ) as HTMLInputElement;
        ele.checked = true;
        this.printArray.push(element);
      });
    } else {
      this.transfers.forEach((element) => {
        const ele = document.getElementById(
          element.id.toString()
        ) as HTMLInputElement;
        ele.checked = false;
      });
    }
  }

  print() {
    if (this.printArray.length > 0) {
      this.transferService.changePrintTransfer(this.printArray);
      this.router.navigate(['/transfer/print']);
    } else {
      this.alertify.error('Please choose Transfer Location!');
    }
  }
}
