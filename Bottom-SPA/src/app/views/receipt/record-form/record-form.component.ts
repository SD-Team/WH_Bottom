import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MaterialService } from '../../../_core/_services/material.service';
import { MaterialModel } from '../../../_core/_viewmodels/material-model';
import { OrderSizeByBatch } from '../../../_core/_viewmodels/ordersize-by-batch';
import { MaterialMergingViewModel } from '../../../_core/_viewmodels/material-merging';
declare var $: any;
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
      console.log(this.orderSizeByBatch);
      console.log(this.materialMerging);
    })
  }
  changeInput(e) {
    let columnInput = 0;
    // Order_Size tuong ung
    let thisInput = e;
    for (let i = 0; i <  this.materialMerging.length; i++) {
      if (e.toString() === this.materialMerging[i].order_Size.toString()) {
        columnInput = i;
        break;
      }
    }
    // Mảng giá trị Purchase_Qty tương ứng với Order_Size đó.
    let listInput = [];
    this.orderSizeByBatch.forEach(element => {
      listInput.push(element.purchase_Qty[columnInput]);
    });
    // Giá trị lấy được khi nhập input.
    let valueInput = (<HTMLInputElement>document.getElementById(thisInput.toString())).value;
    console.log(valueInput);
    let n;
    for (let x = 0; x < listInput.length; x++) {
      if (x === 0) {
        debugger
        if (parseFloat(valueInput) < parseFloat(listInput[x]) || parseFloat(valueInput) === parseFloat(listInput[x])) {
          debugger
          listInput[0] = valueInput;
          for (let y = 1; y < listInput.length; y++) {
            listInput[y] = 0;
          }
          // listInput[1] = 0;
          break;
        } else {
          n = parseFloat(valueInput) - parseFloat(listInput[x]);
        }
      } else {
        if (n >= parseFloat(listInput[x])) {
          n = n - parseFloat(listInput[x]);
        } else {
          listInput[x] = n;
          for (let z = x + 1; z < listInput.length; z++) {
            listInput[z] = 0;
          }
          break;
        }
      };
    };
    
    for (let m = 0; m < this.orderSizeByBatch.length; m++) {
      for (let k = 0; k < this.orderSizeByBatch[m].purchase_Qty.length; k++) {
        if (k === columnInput) {
          this.orderSizeByBatch[m].purchase_Qty[k] = listInput[m];
        }
      }
    }
    // console.log(listInput);
  }
  // functionTest() {
  //   let n;
  //   let listInput = ['100', '500', '600'];
  //   let valueInput = '90';
  //   for (let x = 0; x < listInput.length; x++) {
  //     if (x === 0) {
  //       if (parseFloat(valueInput) <= parseFloat(listInput[x])) {
  //         listInput[0] = valueInput;
  //         for (let y = 1; y < listInput.length; y++) {
  //           listInput[y] = '0';
  //         }
  //       } else {
  //         n = parseFloat(valueInput) - parseFloat(listInput[x]);
  //       }
  //     } else {
  //       if (n >= parseFloat(listInput[x])) {
  //         n = n - parseFloat(listInput[x]);
  //       } else {
  //         listInput[x] = n;
  //         for (let z = x + 1; z < listInput.length; z++) {
  //           listInput[z] = '0';
  //         }
  //       }
  //     };
  //   };
  //   console.log(listInput);
  // }
  backForm() {
    this.router.navigate(['/receipt/record/']);
    // let test = (<HTMLInputElement>document.getElementById('04')).value;
  }
}
