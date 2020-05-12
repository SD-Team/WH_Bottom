import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialService } from '../../../_core/_services/material.service';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';
import { BatchQtyItem } from '../../../_core/_viewmodels/batch-qty-item';
import { MaterialMergingViewModel } from '../../../_core/_viewmodels/material-merging';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { ReceiveNoMain } from '../../../_core/_viewmodels/receive_no_main';
import { OrderSizeByBatch } from '../../../_core/_viewmodels/order-size-by-batch';

@Component({
  selector: 'app-record-form-batches',
  templateUrl: './record-form-batches.component.html',
  styleUrls: ['./record-form-batches.component.scss']
})
export class RecordFormBatchesComponent implements OnInit {
  type: string = 'Batches';
  materialModel: MaterialModel;
  materialByBatchList: OrderSizeByBatch[] = [];
  orderSizeByBatch: OrderSizeByBatch[];
  materialMerging: MaterialMergingViewModel[];
  receiveNoMain: ReceiveNoMain[] = [];
  delivery_No: string;
  constructor(private router: Router,
              private materialService: MaterialService,
              private alertifyService: AlertifyService) { }

  ngOnInit() {
    this.materialService.currentMaterial.subscribe(res => this.materialModel = res);
    this.getDataLoadTable();
    if(this.materialModel === undefined || this.materialModel === null) {
      this.router.navigate(['/receipt/main']);
    }
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
      // let count = this.orderSizeByBatch.length;
      // for (let i = 0; i < this.orderSizeByBatch.length; i++) {
      //   if (this.orderSizeByBatch[i].checkInsert === '1') {
      //     let orderSizeByBatchItem = this.orderSizeByBatch[count - (i +1)];
      //     this.orderSizeByBatch[count - (i +1)] = this.orderSizeByBatch[i];
          
      //   }
      // }
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
    // Những batch nào được nhấn insert thì sẽ được thêm vào mảng materialByBatchList để update vào DB
    this.orderSizeByBatch.forEach(item => {
      if(item.mO_Seq === idButton) {
        let itemPurchase = [] ;
        item.purchase_Qty.forEach(item1 => {
          let item2 = {
            order_Size: item1.order_Size,
            purchase_Qty: item1.purchase_Qty,
            accumlated_In_Qty: item1.purchase_Qty,
            model_Size: item1.model_Size,
            tool_Size: item1.tool_Size,
            spec_Size: item1.spec_Size,
            mO_Qty: item1.mO_Qty,
            purchase_Qty_Const: item1.purchase_Qty_Const,
            received_Qty: item1.purchase_Qty
          };
          itemPurchase.push(item2);
        });
        let materialItem = {
          mO_Seq: item.mO_Seq,
          purchase_No: item.purchase_No,
          missing_No: item.missing_No,
          purchase_Qty: itemPurchase,
          checkInsert: item.checkInsert,
          delivery_No: this.delivery_No,
          material_ID: item.material_ID,
          material_Name: item.material_Name,
          model_No: item.model_No,
          model_Name: item.model_Name,
          mO_No: item.mO_No,
          article: item.article,
          supplier_ID: item.supplier_ID,
          supplier_Name: item.supplier_Name,
          subcon_No: item.subcon_No,
          subcon_Name: item.subcon_Name,
          t3_Supplier: item.t3_Supplier,
          t3_Supplier_Name: item.t3_Supplier_Name,
        }
        this.materialByBatchList.push(materialItem);
      }
    });
  }
  submitData() {
      if (this.materialByBatchList.length !== 0) {
        this.materialByBatchList.map(item => {
          item.delivery_No = this.delivery_No;
          return item;
        });
        console.log(this.materialByBatchList);
        this.materialService.updateMaterial(this.materialByBatchList).subscribe(res => {
          // this.materialService.receiveNoMain(this.materialModel).subscribe(respo => {
          //   this.receiveNoMain = respo;
          //   // this.materialService.changeReceiveNoMain(this.receiveNoMain);
          this.router.navigate(['receipt/record']);
        });
        } else {
          this.alertifyService.error('Please click insert');
        }
  }
  backForm() {
    this.router.navigate(['/receipt/record']);
  }
}
