import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialService } from '../../../_core/_services/material.service';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';
import { BatchQtyItem } from '../../../_core/_viewmodels/batch-qty-item';
import { MaterialMergingViewModel } from '../../../_core/_viewmodels/material-merging';
import { AlertifyService } from '../../../_core/_services/alertify.service';

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
              private materialService: MaterialService,
              private alertiryService: AlertifyService) { }

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
      let count = this.orderSizeByBatch.length;
      for (let i = 0; i < this.orderSizeByBatch.length; i++) {
        if (this.orderSizeByBatch[i].checkInsert === '1') {
          let orderSizeByBatchItem = this.orderSizeByBatch[count - (i +1)];
          this.orderSizeByBatch[count - (i +1)] = this.orderSizeByBatch[i];
          
        }
      }
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
          item1.delivery_Qty_Batches = item1.delivery_Qty_Batches + item.purchase_Qty;
        };
      });
    });
    this.orderSizeByBatch.forEach(item => {
      if(item.mO_Seq === idButton) {
        let itemPurchase = [] ;
        item.purchase_Qty.forEach(item1 => {
          let item2 = {
            order_Size: item1.order_Size,
            purchase_Qty: item1.purchase_Qty,
            accumlated_In_Qty: item1.purchase_Qty
          };
          itemPurchase.push(item2);
        });
        let materialItem = {
          mO_Seq: item.mO_Seq,
          purchase_No: item.purchase_No,
          missing_No: item.missing_No,
          purchase_Qty: itemPurchase,
          checkInsert: item.checkInsert
        }
        this.materialByBatchList.push(materialItem);
      }
    });
    console.log(this.materialByBatchList);
  }
  submitData() {
    if (this.materialByBatchList.length !== 0) {
      this.materialService.updateMaterial(this.materialByBatchList).subscribe(res => {
        this.alertiryService.success('Insert success');
      });
    } else {
      this.alertiryService.error('Please click insert');
    }
  }
  backForm() {
    this.router.navigate(['/receipt/record']);
  }
}
