import { Component, OnInit, Input } from '@angular/core';
import { InputM } from '../../../_core/_models/inputM';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { InputService } from '../../../_core/_services/input.service';
import { InputDetail } from '../../../_core/_models/input-detail';

@Component({
  selector: 'app-input-main',
  templateUrl: './input-main.component.html',
  styleUrls: ['./input-main.component.scss']
})
export class InputMainComponent implements OnInit {
  result: any = [];
  listInputAfterSave: InputDetail[];
  resultDetail: InputDetail;
  listInputNo: any = [];
  qrCodeID = "";
  rackLocation = "";
  constructor(private inputService: InputService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit() {
    this.inputService.currentFlag.subscribe(flag => this.rackLocation = flag);
    this.inputService.currentListInputMain.subscribe(listInputMain => this.result = listInputMain);
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
            if (res != null) {
              this.result.push(res);
              console.log("result: ", this.result);
              
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
      this.listInputNo.push(e.input_No);
    });
    console.log("Lists qr: ", this.listInputNo);
    this.inputService.submitInputMain(this.listInputNo).subscribe(
      () => {
        this.alertify.success("Submit succeed");
      },
      error => {
        this.alertify.error(error);
      }
    )
  }

}
