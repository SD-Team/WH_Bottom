import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InputService } from '../../../_core/_services/input.service';
import { TransactionMain } from '../../../_core/_models/transaction-main';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { AlertModule } from 'ngx-bootstrap/alert';
@Component({
  selector: 'app-qrcode-again',
  templateUrl: './qrcode-again.component.html',
  styleUrls: ['./qrcode-again.component.scss']
})
export class QrcodeAgainComponent implements OnInit {
  mO_No: string;
  rack_Location: string;
  material_ID: string;
  material_Name: string;
  pagination: Pagination;
  transactionMainList:  TransactionMain[];
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
  }
  search(){
    let filterparam = {
      mO_No: this.mO_No,
      rack_Location: this.rack_Location,
      material_ID: this.material_ID
    }
    this.inputService.qrCodeAgainFilter(this.pagination.currentPage , this.pagination.itemsPerPage,filterparam)
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
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.search();
  }
  back() {}
}
