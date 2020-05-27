import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';
import { MaterialService } from '../../../_core/_services/material.service';
import { ReceiveNoMain } from '../../../_core/_viewmodels/receive_no_main';
import { ReceiveNoDetail } from '../../../_core/_viewmodels/receive-no-detail';
import { MaterialEditModel } from '../../../_core/_viewmodels/material-edit-model';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.scss']
})
export class RecordComponent implements OnInit {
  materialModel: MaterialModel;
  receiveNoMain: ReceiveNoMain[]= [];
  receiveDetail: ReceiveNoDetail[] = [];
  materialEditModels: MaterialEditModel[] = [];
  checkButtonAdd = true;
  
  constructor(private router: Router,
              private materialService: MaterialService) { }

  ngOnInit() {
    this.materialService.currentMaterial.subscribe(materialModel => this.materialModel = materialModel);
    if (this.materialModel !== undefined) {
      this.materialService.statusPurchase(this.materialModel).subscribe(res => {
        if(res.status === 'no') {
          this.checkButtonAdd = false;
        } else {
          this.checkButtonAdd = true;
        }
        console.log(this.checkButtonAdd);
      })
    }
    this.getLoadTable();
    if (this.materialModel === undefined || this.materialModel === null) {
      this.router.navigate(['/receipt/main']);
    }
  }
  getLoadTable() {
    this.materialService.receiveNoMain(this.materialModel).subscribe(res => {
      this.receiveNoMain = res;
    });
  }
  changeFormDetail(item: ReceiveNoMain) {
    this.materialService.changeReceiveNoMainItem(item);
    this.materialService.receiveNoDetails(item.receive_No).subscribe(res => {
      this.receiveDetail = res;
      this.materialService.changeReceiveNoDetail(this.receiveDetail);
      this.router.navigate(['/receipt/record/record-detail']);
    })
  }
  editReceiveNo(receiveNoMain: ReceiveNoMain) {
    this.materialService.changeReceiveNoMainItem(receiveNoMain);
    this.router.navigate(['/receipt/record/record-edit']);
  }
  changeForm() {
    this.router.navigate(['/receipt/record/add']);
  }
  backForm() {
    this.router.navigate(['/receipt/main/']);
  }
}
