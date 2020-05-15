import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { Router } from '@angular/router';
import { InputService } from '../../../_core/_services/input.service';
import { InputDetail } from '../../../_core/_models/input-detail';
import { PackingListDetailService } from '../../../_core/_services/packing-list-detail.service';
import { PackingPrintAll } from '../../../_core/_viewmodels/packing-print-all';

@Component({
  selector: 'app-input-main',
  templateUrl: './input-main.component.html',
  styleUrls: ['./input-main.component.scss']
})
export class InputMainComponent implements OnInit {
  result: any = [];
  resultDetail: InputDetail;
  listInputNo: any = [];
  qrCodeID = "";
  rackLocation = "";
  err = true;
  checkSubmit: boolean;
  packingPrintAll: PackingPrintAll[] = [];
  constructor(private inputService: InputService,
    private packingListDetailService: PackingListDetailService,
    private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
    this.inputService.currentCheckSubmit.subscribe(res => this.checkSubmit = res);
    this.inputService.currentFlag.subscribe(flag => this.rackLocation = flag);
    this.inputService.currentListInputMain.subscribe(listInputMain => this.result = listInputMain);
  }
  enter() {
    document.getElementById("scanQrCodeId").focus();
  }
  printMiss(qrCode: string) {
    this.inputService.findMiss(qrCode).subscribe(res => {
      this.inputService.changeMissingPrint('1');
      let missingNo = res.missingNo;
      this.router.navigate(['/input/missing-print/', missingNo]);
    }, error => {
      this.alertify.error(error);
    })
  }
  upperCase() {
    this.rackLocation = this.rackLocation.toUpperCase();
  }
  getInputMain(e) {
    if(this.rackLocation === "") {
      this.alertify.error("Please Scan Rack Location!");
    } else {
      if (e.length === 14) {
        let flag = true;
        this.result.forEach(item => {
          if (item.qrCode_Id === e)
            flag = false;
        });
        if (flag) {
          this.inputService.getMainByQrCodeID(this.qrCodeID)
            .subscribe((res) => {
              console.log(res);
              if(res === null) {
                this.alertify.error('Does not exist QRCodeID!!!');
              } else if(res.is_Scanned === 'Y') {
                this.alertify.error('This qrCode has been scanned!!!');
              } else {
                if (res != null) {
                  this.result.push(res);
                  this.result.forEach(element => {
                    if(element.qrCode_Id.trim() === e.toString().trim()) {
                      element.rack_Location = this.rackLocation;
                      element.inStock_Qty = element.accumated_Qty;
                      element.trans_In_Qty = element.accumated_Qty;
                      // element.input_No = "BI" + element.plan_No + (Math.floor(Math.random() * (999 - 100)) + 100);
                    }
                  });
                  console.log(this.result);
                }
              }
            }, error => {
              this.alertify.error(error);
            });
        } else
          this.alertify.error("This QRCode scanded!");
        this.qrCodeID = ""
      }
    }

  }
  printQrCode(qrCodeId: string) {
    this.packingListDetailService.changePrintQrCodeAgain('inputMain');
    let qrCode = [];
    qrCode.push(qrCodeId);
    this.packingListDetailService.findByQrCodeId(qrCode).subscribe(res => {
      this.packingPrintAll = res;
      this.packingListDetailService.changePackingPrint(this.packingPrintAll);
      this.router.navigate(['/qr/print']);
    })
  }
  getDetailByQRCode(inputDetail: InputDetail) {
      this.inputService.changeListInputMain(this.result);
      this.inputService.changeInputDetail(inputDetail);
      this.inputService.changeFlag(this.rackLocation);
      this.router.navigate(["/input/print"]);
      // this.inputService.getDetailByQrCodeID(qrCode)
      //   .subscribe((res) => {
      //     this.resultDetail = res;
          
      //     console.log("Main: ", this.resultDetail);
      //   }, error => {
      //     this.alertify.error(error);
      //   });
  }

  remove(qrCode_Id: string) {
    this.result.forEach((e, i) => {
      if (e.qrCode_Id === qrCode_Id) {
        this.result.splice(i, 1);
      }
    });
  }

  submitInput() {
    this.result.forEach((e, i) => {
      if (e.input_No == null)
        this.err = false;
      else
        this.listInputNo.push(e.input_No);
    });
    this.result.forEach(element => {
      element.input_No = "BI" + element.plan_No + (Math.floor(Math.random() * (999 - 100)) + 100);
    });
    console.log("Lists qr: ", this.listInputNo);
    if( this.err) {
      // Submit Input
      let inputModel = {
        transactionList: this.result,
        inputNoList: this.listInputNo
      }
      this.inputService.submitInputMain(inputModel).subscribe(
        () => {
          this.rackLocation = '';
          // this.result = [];
          this.alertify.success("Submit succeed");
          this.err = false;
          this.checkSubmit = true;
          this.inputService.changeCheckSubmit(this.checkSubmit);
        },
        error => {
          this.alertify.error(error);
        }
      )
    } else {
      this.alertify.error("error");
    }
  }
}
