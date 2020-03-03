import { Component, OnInit, Input } from '@angular/core';
import { InputM } from '../../../_core/_models/inputM';

@Component({
  selector: 'app-input-main',
  templateUrl: './input-main.component.html',
  styleUrls: ['./input-main.component.scss']
})
export class InputMainComponent implements OnInit {
  result = [];
  constructor() { }

  ngOnInit() {
  }


  search() {

    for (let index = 1; index < 3; index++) {
      let item = new InputM()
      item.seq = index;
      item.qrCodeID = "202020212asd";
      item.planNo = "013251566132566";
      item.purchaNo = "PW1956232YC";
      item.betch = -1;
      item.rackLocation = "A-01-02";
      item.receivdeQty = 1050;
      item.inputQty = 1050;
      item.stockQty = 1050;
      item.description = "SKYJ17S00A - (35876)(65A 80 SO RERUB NM EPM3) BLK"
      this.result.push(item);
    }

  }

}
