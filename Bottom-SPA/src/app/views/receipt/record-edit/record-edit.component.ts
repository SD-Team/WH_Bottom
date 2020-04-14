import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialService } from '../../../_core/_services/material.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { MaterialEditModel } from '../../../_core/_viewmodels/material-edit-model';
import { ReceiveNoMain } from '../../../_core/_viewmodels/receive_no_main';

@Component({
  selector: 'app-record-edit',
  templateUrl: './record-edit.component.html',
  styleUrls: ['./record-edit.component.scss']
})
export class RecordEditComponent implements OnInit {
  delivery_No: string;
  materialEditModels: MaterialEditModel[] = [];
  materialEditModelsConst: MaterialEditModel[] = [];
  receiveNoMain: ReceiveNoMain;
  constructor(private router: Router,
              private altertifyService: AlertifyService,
              private materialService: MaterialService) { }

  ngOnInit() {
    this.materialService.currentReceiveNoMainItem.subscribe(res => this.receiveNoMain = res);
    this.getLoadTable();

  }
  submitEdit() {
    let checkOnEdit = 0;
    this.materialEditModelsConst.forEach(element1 => {
      this.materialEditModels.forEach(element2 => {
        if (element2.order_Size.trim() === element1.order_Size.trim()) {
          if (element2.received_Qty_Edit > (element1.delivery_Qty + element1.received_Qty)) {
            checkOnEdit = checkOnEdit + 1;
            let errorMessage = 'Input of Order_Size ' + element1.order_Size.toString() + ' has errors';
            this.altertifyService.error(errorMessage);
          }
        }
      });
    });
    if (checkOnEdit < 1) {
      this.materialService.editDetail(this.materialEditModels).subscribe(res => {
        this.altertifyService.success('Edit data successed!');
        this.router.navigate(['receipt/record/']);
      }, error => {
        this.altertifyService.error(error);
      });
    }
  }
  getLoadTable() {
    if(this.receiveNoMain !== undefined) {
      this.delivery_No = this.receiveNoMain.delivery_No;
    }
    this.materialService.editMaterial(this.receiveNoMain).subscribe(res => {
        this.materialEditModels = res;
        // Mảng materialEditModelsConst sẽ không thay đổi
        this.materialEditModelsConst = JSON.parse(JSON.stringify(res));
    });
  }
  cancel() {
    this.getLoadTable();
  }
  changeInput(e) {
    let columnInput = 0;
    let thisInput = e.toString();
    for(let i = 0; i < this.materialEditModels.length; i++) {
      if (thisInput === this.materialEditModels[i].order_Size.toString()) {
        columnInput = i;
      }
    }
    // Giá trị lấy được khi nhập input.
    let valueInput = (<HTMLInputElement>document.getElementById(thisInput.toString())).value;
    this.materialEditModels.forEach(element => {
      if (element.order_Size.trim() === thisInput.trim()) {
        element.received_Qty_Edit = parseFloat(valueInput.toString());
      }
    });
  }
  backForm() {
    this.router.navigate(['receipt/record']);
  }
}
