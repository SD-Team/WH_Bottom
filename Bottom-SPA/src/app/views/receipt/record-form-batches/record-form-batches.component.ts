import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialService } from '../../../_core/_services/material.service';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';
import { BatchQtyItem } from '../../../_core/_viewmodels/batch-qty-item';
import { MaterialMergingViewModel } from '../../../_core/_viewmodels/material-merging';

@Component({
  selector: 'app-record-form-batches',
  templateUrl: './record-form-batches.component.html',
  styleUrls: ['./record-form-batches.component.scss']
})
export class RecordFormBatchesComponent implements OnInit {
  type: string = 'Batches';
  materialModel: MaterialModel;
  materialByBatchList: BatchQtyItem[] = [];
  orderSizeByBatch: BatchQtyItem[];
  materialMerging: MaterialMergingViewModel[];
  constructor(private router: Router,
              private materialService: MaterialService) { }

  ngOnInit() {
    this.materialService.currentMaterial.subscribe(res => this.materialModel = res);
    this.getDataLoadTable();
  }
  changeForm() {
    if (this.type === 'No Batch') {
      this.router.navigate(['/receipt/record/add']);
    }
  }
  getDataLoadTable() {
    this.materialService.searchByPurchase(this.materialModel).subscribe(res => {
      this.orderSizeByBatch = res.list3;
      this.materialMerging = res.list4;
    })
  }
  insertMaterial(mO_Seq) {
    let idButton = mO_Seq.toString();
    let materialByBatch; 
    (<HTMLInputElement>document.getElementById(idButton)).disabled = true;
    this.orderSizeByBatch.forEach(item => {
      if (item.mO_Seq.toString() === idButton) {
        materialByBatch = item;
      }
    });
    materialByBatch.purchase_Qty.forEach(item => {
      this.materialMerging.forEach(item1 => {
        if (item1.order_Size === item.order_Size) {
          item1.accumlated_In_Qty = item1.accumlated_In_Qty + item.purchase_Qty;
        };
      });
    });
    this.orderSizeByBatch.forEach(item => {
      if(item.mO_Seq === idButton) {
        item.purchase_Qty.forEach(item1 => {

        });
        // this.materialByBatchList.push(item);
      }
    });
    console.log(this.materialByBatchList);
  }
  backForm() {
    this.router.navigate(['/receipt/record']);
  }
}
