import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';
import { MaterialService } from '../../../_core/_services/material.service';
import { ReceiveNoMain } from '../../../_core/_viewmodels/receive_no_main';
import { ReceiveNoDetail } from '../../../_core/_viewmodels/receive-no-detail';

@Component({
  selector: 'app-record',
  templateUrl: './record.component.html',
  styleUrls: ['./record.component.scss']
})
export class RecordComponent implements OnInit {
  materialModel: MaterialModel;
  receiveNoMain: ReceiveNoMain[]= [];
  receiveDetail: ReceiveNoDetail[] = [];
  constructor(private router: Router,
              private materialService: MaterialService) { }

  ngOnInit() {
    this.materialService.currentMaterial.subscribe(materialModel => this.materialModel = materialModel);
    // console.log(this.materialModel);
    this.materialService.currentReceiveNoMain.subscribe(receiveNoMain => this.receiveNoMain = receiveNoMain);
  }
  changeFormDetail(receiveNo) {
    this.materialService.receiveNoDetails(receiveNo).subscribe(res => {
      this.receiveDetail = res;
      this.materialService.changeReceiveNoDetail(this.receiveDetail);
      this.router.navigate(['/receipt/record/record-detail']);
    })
  }
  changeForm() {
    this.router.navigate(['/receipt/record/add']);
  }
  backForm() {
    this.router.navigate(['/receipt/main/']);
  }
}
