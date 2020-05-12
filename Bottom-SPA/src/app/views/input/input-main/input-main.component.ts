import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { Router } from '@angular/router';
import { InputService } from '../../../_core/_services/input.service';
import { InputDetail } from '../../../_core/_models/input-detail';

@Component({
  selector: 'app-input-main',
  templateUrl: './input-main.component.html',
  styleUrls: ['./input-main.component.scss']
})
export class InputMainComponent implements OnInit, OnDestroy {
  result: any = [];
  resultDetail: InputDetail;
  listInputNo: any = [];
  qrCodeID = "";
  rackLocation = "";
  err = true;
  checkSubmit = false;
  constructor(private inputService: InputService,
    private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
    this.inputService.currentFlag.subscribe(flag => this.rackLocation = flag);
    this.inputService.currentListInputMain.subscribe(listInputMain => this.result = listInputMain);
  }

  ngOnDestroy() {
    
  }
  getInputMain(e) {
    console.log(e.length);
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
                console.log("result: ", this.result);
                
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

  

  getDetailByQRCode(inputDetail: InputDetail) {
    if (this.rackLocation === "")
      this.alertify.error("Please Scan Rack Location!");
    else {
      this.inputService.changeListInputMain(this.result);
      this.inputService.changeInputDetail(inputDetail);
      this.inputService.changeFlag(this.rackLocation);
      this.router.navigate(["/input/print"])
    }
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
    console.log("Lists qr: ", this.listInputNo);
    if( this.err) {
      // Create Input
      // this.result.forEach(element => {
      //   this.inputService.saveInput(element).subscribe(res => {
      //     // Add succeed!
      //   }, error => {
      //     this.alertify.error("error");
      //   });
      // });

      // Submit Input
      let inputModel = {
        transactionList: this.result,
        inputNoList: this.listInputNo
      }
      this.inputService.submitInputMain(inputModel).subscribe(
        () => {
          this.rackLocation = '';
          this.result = [];
          this.alertify.success("Submit succeed");
          this.err = false;
          this.checkSubmit = true;
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
