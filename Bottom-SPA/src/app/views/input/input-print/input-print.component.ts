import { Component, OnInit } from '@angular/core';
import { InputService } from '../../../_core/_services/input.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-input-print',
  templateUrl: './input-print.component.html',
  styleUrls: ['./input-print.component.scss']
})
export class InputPrintComponent implements OnInit {
  listInputMain: any = [];
  inputDetailItem: any = [];
  transInModel: any = [];
  rackLocation = "";
  constructor(
    private inputService: InputService,
    private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
      this.inputService.currentInputDetail.subscribe(inputDetailItem => this.inputDetailItem = inputDetailItem);
      this.inputDetailItem.detail_Size.forEach(e => {
          this.transInModel.push(e.qty)
      });
      this.inputService.currentListInputMain.subscribe(listInputMain => this.listInputMain = listInputMain);
  }

  changeInput(e, i) {
    if (e > this.inputDetailItem.detail_Size[i].qty) {
      let ele = document.getElementById("id-" + i) as HTMLInputElement;
      ele.value = this.inputDetailItem.detail_Size[i].qty;
      this.transInModel[i] = this.inputDetailItem.detail_Size[i].qty;
    }
  }
  cancel() {
    this.router.navigate(["/input/main"]);
  }
  saveInput() {
    console.log(this.inputDetailItem);
    let params: any = [];
    params = this.inputDetailItem;
    // params.rack_Location = this.rackLocation;
    params.trans_In_Qty = 0;
    params.detail_Size.forEach((element, index) => {
      element.qty = this.transInModel[index];
      params.trans_In_Qty += element.qty;
    });
    params.inStock_Qty = params.trans_In_Qty;
    //params.input_No = "BI" + params.plan_No + (Math.floor(Math.random() * (999 - 100)) + 100);
    console.log(params);
    // this.inputService.saveInput(params).subscribe(
    //   () => {
    //     this.alertify.success("Save succeed");
    //     this.listInputMain.forEach((e, i) => {
    //       if (e.qrCode_Id === params.qrCode_Id)
    //         this.listInputMain[i] = params;
    //     });
    //     this.inputService.changeListInputMain(this.listInputMain);
    //     console.log("new List: ", this.listInputMain);
    //     this.router.navigate(["/input/main"])
    //   },
    //   error => {
    //     this.alertify.error(error);
    //   }
    // )

    //--------------------- Tạm thời cmt lại--------------------------------//
    this.listInputMain.forEach((e, i) => {
      if (e.qrCode_Id === params.qrCode_Id)
        this.listInputMain[i] = params;
      });
      this.alertify.success("Save succeed");
      this.inputService.changeListInputMain(this.listInputMain);
      this.router.navigate(["/input/main"]);
    localStorage.setItem("inputMain", JSON.stringify(params));
    
    console.log(params)
    console.log(this.rackLocation)
    console.log(">>>", this.transInModel)
  }

}
