import { Component, OnInit } from '@angular/core';
import { Receipt } from '../../../_core/_models/receipt';
import { BsDatepickerConfig } from 'ngx-bootstrap';

@Component({
  selector: 'app-receipt-main',
  templateUrl: './receipt-main.component.html',
  styleUrls: ['./receipt-main.component.scss']
})
export class ReceiptMainComponent implements OnInit {
  bsConfig: Partial<BsDatepickerConfig>;
  
  fromDate = new Date();
  toDate = new Date();
  results=[];
  constructor() { }

  ngOnInit() {
    this.bsConfig = Object.assign({}, { containerClass: "theme-blue" });
  }

  search() {
    let a = new Receipt();
    a.close = "N";
    
    a.plan_no = "0123456789";
    a.purchase_No = "PW19B08MEC";
    a.mat_no = "001";
    a.mat_name = "ABC";
    a.unit = "pair";
    a.Batch = -1;
    a.model_no = "JAE06"
    a.model_name = "FALCON W"
    a.article = "EH3522"
    a.supplier_no = "VB02"
    a.supplier_name = "YUE SHENG"
    a.purchasing_qty = 1920
    a.received_qty = 0
    a.balance_qty = 1920
    a.text = "SFCD27W00A    -   (03003)(016 55C CM LS3)A8L5/MIDSOLE"
    this.results.push(a);
    let b = new Receipt();
    b.close = "N";
    b.plan_no = "01233434265";
    b.purchase_No = "DG49B78SDF";
    b.mat_no = "001";
    b.mat_name = "ABC";
    b.unit = "pair";
    b.Batch = -3;
    b.model_no = "JAE06"
    b.model_name = "FALCON W"
    b.article = "EH3522"
    b.supplier_no = "VB02"
    b.supplier_name = "YUE SHENG"
    b.purchasing_qty = 1000
    b.received_qty = 0
    b.balance_qty = 1000
    b.text = "SFCD27W00A    -   (03003)(016 55C CM LS3)A8L5/MIDSOLE"
    this.results.push(b);
    console.log(this.results);
    // this.results = [
    //   {
    //     close: "N",
    //     plan_no : "0123456789",
    //     purchase_No: "PW19B08MEC",
    //     mat_no: "001",
    //     mat_name: "ABC",
    //     unit: "pair",
    //     Batch: -1,
    //     model_no: "JAE06",
    //     model_name: "FALCON W",
    //     article: "EH3522",
    //     supplier_no: "VB02",
    //     supplier_name: "YUE SHENG",
    //     purchasing_qty: 1920,
    //     received_qty: 0,
    //     balance_qty: 1920,
    //     text: "SFCD27W00A    -   (03003)(016 55C CM LS3)A8L5/MIDSOLE"
    //   },
    //   {
    //     close: "Y",
    //     plan_no: "0123824987",
    //     purchase_No: "PW19B0FEYC",
    //     mat_no: "001",
    //     mat_name: "CCV",
    //     unit: "pair",
    //     Batch: -2,
    //     model_no: "JAE06",
    //     model_name: "FALCON W",
    //     article: "EH3522",
    //     supplier_no: "VB02",
    //     supplier_name: "YUE SHENG",
    //     purchasing_qty: 350,
    //     received_qty: 0,
    //     balance_qty: 350,
    //     text: "SFCD27W00A    -   (03003)(016 55C CM LS3)A8L5/MIDSOLE",
    //   }
    // ]
  }

}
