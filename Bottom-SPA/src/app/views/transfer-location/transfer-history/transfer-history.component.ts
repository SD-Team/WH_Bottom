import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { TransferM } from '../../../_core/_models/transferM';
import { TransferService } from '../../../_core/_services/transfer.service';
import { Router } from '@angular/router';
import { AlertifyService } from '../../../_core/_services/alertify.service';

@Component({
  selector: 'app-transfer-history',
  templateUrl: './transfer-history.component.html',
  styleUrls: ['./transfer-history.component.scss']
})
export class TransferHistoryComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
  fromDate: string;
  toDate: string;
  transfers: TransferM[] = [];
  printArray: TransferM[] = [];

  constructor(private transferService: TransferService, private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
    this.bsConfig = Object.assign({}, { containerClass: 'theme-blue', isAnimated: true, dateInputFormat: 'YYYY/MM/DD' });
    const timeNow = new Date().getFullYear().toString() +
      '/' + (new Date().getMonth() + 1).toString() +
      '/' + new Date().getDate().toString();
    this.fromDate = timeNow;
    this.toDate = timeNow;
    this.getData();
  }

  getData() {
    const t1 = new Date(this.fromDate).toLocaleDateString();
    const t2 = new Date(this.toDate).toLocaleDateString();
    this.transferService.search(t1, t2).subscribe(res => {
      this.transfers = res;
    });
  }

  checkEle(e) {
    if (e.target.checked) {
      const transfer = this.transfers[e.target.value];
      this.printArray.push(transfer);
    } else {
      const i = this.printArray.findIndex(element => element === e.target.value);
      this.printArray.splice(i, 1);
    }
    const ele = document.getElementById('checkAll') as HTMLInputElement;
    if (this.printArray.length === this.transfers.length) {
      ele.checked = true;
    } else { ele.checked = false; }
  }

  checkAll(e) {
    this.printArray = [];
    if (e.target.checked) {
      this.transfers.forEach(element => {
        const ele = document.getElementById(element.id.toString()) as HTMLInputElement;
        ele.checked = true;
        this.printArray.push(element);
      });
    } else {
      this.transfers.forEach(element => {
        const ele = document.getElementById(element.id.toString()) as HTMLInputElement;
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
