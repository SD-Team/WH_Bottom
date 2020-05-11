import { Component, OnInit } from '@angular/core';
import { TransactionMain } from '../../../_core/_models/transaction-main';
import { InputService } from '../../../_core/_services/input.service';
import { Router } from '@angular/router';
import { PackingListDetailService } from '../../../_core/_services/packing-list-detail.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { FilterMissingParam } from '../../../_core/_viewmodels/missing-print-search';

@Component({
  selector: 'app-missing-again',
  templateUrl: './missing-again.component.html',
  styleUrls: ['./missing-again.component.scss']
})
export class MissingAgainComponent implements OnInit {
  transactionMainList: TransactionMain[] = [];
  mO_No: string;
  material_ID: string;
  material_Name: string;
  pagination: Pagination;
  missingParam: FilterMissingParam;
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
  constructor(private inputService: InputService,
              private alertifyService: AlertifyService) { }

  ngOnInit() {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3,
      totalItems: 0,
      totalPages: 0
    };
    this.inputService.currentMissingParam.subscribe(res => this.missingParam = res);
    console.log(this.missingParam);
    if (this.missingParam === undefined) {
      this.getDataLoadPage();
    } else {
      this.mO_No = this.missingParam.mO_No;
      this.material_ID = this.missingParam.material_ID;
      if(this.material_ID !== undefined) {
        this.findMaterialName();
      }
      this.search();
    }
    this.inputService.changeListInputMain([]);
    this.inputService.changeFlag('');
  }
  getDataLoadPage() {
    this.missingParam = {
      mO_No: '',
      material_ID: ''
    }
    this.inputService.missingPrintFilter(this.pagination.currentPage , this.pagination.itemsPerPage,this.missingParam)
    .subscribe((res: PaginatedResult<TransactionMain[]>) => {
      this.transactionMainList = res.result;
      this.pagination = res.pagination;
      if(this.transactionMainList.length === 0) {
        this.alertifyService.error('No Data!');
      }
    }, error => {
      this.alertifyService.error(error);
    });
  }
  findMaterialName() {
    this.inputService.findMaterialName(this.material_ID).subscribe(res => {
      this.material_Name = res.materialName;
    });
  }
  search(){
    this.missingParam = {
      mO_No: this.mO_No,
      material_ID: this.material_ID
    }
    this.inputService.changeMissingParam(this.missingParam);
    this.inputService.missingPrintFilter(this.pagination.currentPage , this.pagination.itemsPerPage,this.missingParam)
    .subscribe((res: PaginatedResult<TransactionMain[]>) => {
      this.transactionMainList = res.result;
      this.pagination = res.pagination;
      if(this.transactionMainList.length === 0) {
        this.alertifyService.error('No Data!');
      }
    }, error => {
      this.alertifyService.error(error);
    });
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.search();
  }
}
