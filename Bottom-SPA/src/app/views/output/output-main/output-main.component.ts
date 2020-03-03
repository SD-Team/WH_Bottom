import { Component, OnInit } from '@angular/core';
import { OutputM } from '../../../_core/_models/outputM';

@Component({
  selector: 'app-output-main',
  templateUrl: './output-main.component.html',
  styleUrls: ['./output-main.component.scss']
})
export class OutputMainComponent implements OnInit {
  result = [];
  constructor() { }

  ngOnInit() {
  }


  search() {
    for (let index = 1; index < 3; index++) {
      let b = new OutputM();
      b.qrCodeId = "20200224AAB";
      b.outputSheetNo="";
      b.planNo = "0132514846511";
      b.purchaseNo = "PW19B0FEYC";
      b.batch = -1;
      b.wH = "CA";
      b.building = "D";
      b.area = "Storage";
      b.rackLocation = "A-01";
      b.stockQty = 1050;
      b.outputQty = 100;
      b.remainingQty = 0;
      b.description="SKYJ17 -(35867)(65A 80 SO RERUB NM EPM3) BLK FOREFOOT";
      this.result.push(b);
    }
  }
}
