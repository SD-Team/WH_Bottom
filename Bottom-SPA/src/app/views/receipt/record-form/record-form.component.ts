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
    let n;
    for (let x = 0; x < listInput.length; x++) {
      if (x === 0) {
        debugger
        // Khi accumlated_In_Qty ở dòng đầu tiên  = 0 có nghĩa là chưa nhận lô hàng nào.
        if (parseFloat(listInput[x].accumlated_In_Qty) === 0) {
          if (parseFloat(valueInput) <= parseFloat(listInput[x].purchase_Qty)) {
            listInput[0].purchase_Qty = valueInput;
            listInput[0].accumlated_In_Qty = valueInput;
            for (let y = 1; y < listInput.length; y++) {
              listInput[y].purchase_Qty = 0;
            }
            break;
          } else {
            n = parseFloat(valueInput) - parseFloat(listInput[x].purchase_Qty);
            listInput[0].accumlated_In_Qty = listInput[0].purchase_Qty;
          }
        // Khi lô hàng đã nhận đủ.
      } else if (parseFloat(listInput[x].accumlated_In_Qty) === parseFloat(listInput[x].purchase_Qty)) {
        n = parseFloat(valueInput);
      } 
      // Khi lô hàng nhận chưa đủ.
      else if (parseFloat(listInput[x].accumlated_In_Qty) < parseFloat(listInput[x].purchase_Qty)) {
        // Lượng nhận vào + lượng đã nhận nhỏ thua hoặc bằng lượng cần nhận để đủ.
        if ((parseFloat(valueInput) + parseFloat(listInput[x].accumlated_In_Qty)) <= parseFloat(listInput[x].purchase_Qty)) {
          listInput[0].purchase_Qty = parseFloat(valueInput) + parseFloat(listInput[x].accumlated_In_Qty);
          listInput[0].accumlated_In_Qty = parseFloat(valueInput) + parseFloat(listInput[x].accumlated_In_Qty);
        } 
        // Lượng nhận vào + lượng đã nhận lớn hơn lượng cần nhận để đủ.
        else {
          // Lượng nhận còn lại sau khi nhập hàng đủ ở trên.
          n = parseFloat(valueInput) - (parseFloat(listInput[0].purchase_Qty) - parseFloat(listInput[0].accumlated_In_Qty));
          listInput[0].accumlated_In_Qty = listInput[0].purchase_Qty;
        }
      } else {

      }
    } else {
      if (parseFloat(listInput[x].accumlated_In_Qty) === 0) {
        if (n <= parseFloat(listInput[x].purchase_Qty)) {
            listInput[x].purchase_Qty = n;
            listInput[x].accumlated_In_Qty = n;
            for (let y = x + 1; y < listInput.length; y++) {
              listInput[y].purchase_Qty = 0;
            }
            break;
        } else {
          n = n - parseFloat(listInput[x].purchase_Qty);
          listInput[x].accumlated_In_Qty = listInput[x].purchase_Qty;
        }
      }
      // Khi lô hàng đã nhận đủ.
       else if (parseFloat(listInput[x].accumlated_In_Qty) === parseFloat(listInput[x].purchase_Qty)){

      }
      // Khi lô hàng nhận chưa đủ.
      else if(parseFloat(listInput[x].accumlated_In_Qty) < parseFloat(listInput[x].purchase_Qty)) {
         // Lượng nhận vào + lượng đã nhận nhỏ thua hoặc bằng lượng cần nhận để đủ.
         if ((n + parseFloat(listInput[x].accumlated_In_Qty)) <= parseFloat(listInput[x].purchase_Qty)) {
          listInput[x].purchase_Qty = n + parseFloat(listInput[x].accumlated_In_Qty);
          listInput[x].accumlated_In_Qty = n + parseFloat(listInput[x].accumlated_In_Qty);
          for (let z = x + 1; z < listInput.length; z++) {
            listInput[z].purchase_Qty = 0;
          }
          break;
        } 
         // Lượng nhận vào + lượng đã nhận lớn hơn lượng cần nhận để đủ.
        else {
           // Lượng nhận còn lại sau khi nhập hàng đủ ở trên.
           n = n - (parseFloat(listInput[x].purchase_Qty) - parseFloat(listInput[x].accumlated_In_Qty));
           listInput[x].accumlated_In_Qty = listInput[x].purchase_Qty;
        }
      }
    };
  }
  console.log(listInput);
}
  updateReceiving() {

  }
  backForm() {
    this.router.navigate(['/receipt/record/']);
    // let test = (<HTMLInputElement>document.getElementById('04')).value;
  }
}
