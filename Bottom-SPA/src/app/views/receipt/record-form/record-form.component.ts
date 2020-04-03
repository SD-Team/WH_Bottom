import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialService } from '../../../_core/_services/material.service';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';
import { OrderSizeByBatch } from '../../../_core/_viewmodels/ordersize-by-batch';
import { MaterialMergingViewModel } from '../../../_core/_viewmodels/material-merging';

@Component({
  selector: 'app-record-form',
  templateUrl: './record-form.component.html',
  styleUrls: ['./record-form.component.scss']
})
export class RecordFormComponent implements OnInit {
  fromDate = new Date();
  isDisabled = true;
  type: string = 'No Batch';
  materialModel: MaterialModel;
  orderSizeByBatch: OrderSizeByBatch[];
  materialMerging: MaterialMergingViewModel[];
  constructor(private router: Router,
              private materialService: MaterialService) { }

  ngOnInit() {
    this.materialService.currentMaterial.subscribe(res => this.materialModel = res);
    this.getDataLoadTable();
  }
  changeForm() {
    if (this.type === 'Batches') {
      this.router.navigate(['/receipt/record/add-batches']);
    }
  }
  getDataLoadTable() {
    this.materialService.searchByPurchase(this.materialModel).subscribe(res => {
      this.orderSizeByBatch = res.list3;
      this.materialMerging = res.list4;
      // console.log(this.orderSizeByBatch);
      // console.log(this.materialMerging);
    })
  }
  backForm() {
    // this.router.navigate(['/receipt/record/']);
    
  }
}
