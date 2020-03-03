import { Component, OnInit } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { QrGenerate } from '../../../_core/_models/qr-generate';

@Component({
  selector: 'app-qr-main',
  templateUrl: './qr-main.component.html',
  styleUrls: ['./qr-main.component.scss']
})
export class QrMainComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;

  fromDate = new Date();
  toDate = new Date();
  results = [];
  clickSearch: boolean = false;
  constructor() { }

  ngOnInit() {
    this.bsConfig = Object.assign({}, { containerClass: 'theme-blue' });
  }

  search() {
    this.clickSearch = true;
    const a = new QrGenerate();
    a.qrCodeId = 'B0124676670';
    a.plan_no = '0124696503';
    a.recevie_no = 'RW19c00QCY';
    a.batch = -1;
    a.mat = 'SFCD27W00A';
    a.mat_name = '(35876)(65A 80 SO RERUB NM EPM3)BLK Forefoot';
    this.results.push(a);
    const b = new QrGenerate();
    b.qrCodeId = 'D0124676670';
    b.plan_no = '654756756';
    b.recevie_no = 'YN19c00QUH';
    b.batch = -3;
    b.mat = 'GKDD27W09';
    b.mat_name = '(35876)(65A 80 SO RERUB NM EPM3)BLK Forefoot';
    this.results.push(b);
  }

  cancel() {
    this.clickSearch = false;
    this.results = [];
  }
}
