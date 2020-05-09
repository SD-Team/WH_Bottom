import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { InputService } from '../../../_core/_services/input.service';
import { TransactionMain } from '../../../_core/_models/transaction-main';
import { Pagination, PaginatedResult } from '../../../_core/_models/pagination';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { FilterQrCodeAgainParam } from '../../../_core/_viewmodels/qrcode-again-search';
import { PackingListDetailService } from '../../../_core/_services/packing-list-detail.service';
import { PackingPrintAll } from '../../../_core/_viewmodels/packing-print-all';
import { ModalBackdropComponent } from 'ngx-bootstrap';
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
  qrCodeAgainParam: FilterQrCodeAgainParam;
  transactionMainList:  TransactionMain[] = [];
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
  constructor(private inputService: InputService,
              private router: Router,
              private packingListDetailService: PackingListDetailService,
              private alertifyService: AlertifyService) { }

  ngOnInit() {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3,
      totalItems: 0,
      totalPages: 0
    };
    this.inputService.currentQrCodeAgainParam.subscribe(res => this.qrCodeAgainParam = res);
    if(this.qrCodeAgainParam === undefined) {
      this.getDataLoadPage();
    } else {
      this.mO_No = this.qrCodeAgainParam.mO_No;
      this.rack_Location = this.qrCodeAgainParam.rack_Location;
      this.material_ID = this.qrCodeAgainParam.material_ID;
      this.findMaterialName();
      this.search();
    }
    this.inputService.changeListInputMain([]);
    this.inputService.changeFlag('');
  }
  getDataLoadPage() {
    this.qrCodeAgainParam = {
      mO_No: '',
      rack_Location: '',
      material_ID: ''
    }
    this.inputService.qrCodeAgainFilter(this.pagination.currentPage , this.pagination.itemsPerPage,this.qrCodeAgainParam)
    .subscribe((res: PaginatedResult<TransactionMain[]>) => {
      this.transactionMainList = res.result;
      this.pagination = res.pagination;
    }, error => {
      this.alertifyService.error(error);
    });
  }
  search(){
    this.qrCodeAgainParam = {
      mO_No: this.mO_No,
      rack_Location: this.rack_Location,
      material_ID: this.material_ID
    }
    this.inputService.changeCodeAgainParam(this.qrCodeAgainParam);
    this.inputService.qrCodeAgainFilter(this.pagination.currentPage , this.pagination.itemsPerPage,this.qrCodeAgainParam)
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
  printQrCodeAgain(model: TransactionMain) {
    this.packingListDetailService.changePrintQrCodeAgain('1');
    let qrCodeIdVersion = [];
    let item = {
      qrCode_ID: model.qrCode_ID,
      qrCode_Version: model.qrCode_Version
    };
    qrCodeIdVersion.push(item);
    this.packingListDetailService.findByQrCodeIdListAgain(qrCodeIdVersion).subscribe(res => {
      this.packingPrintAll = res;
      this.packingListDetailService.changePackingPrint(this.packingPrintAll);
      this.router.navigate(['/qr/print']);
    })
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.search();
  }
}
