import { Component, OnInit } from '@angular/core';
import { OutputM } from '../../../_core/_models/outputM';
import { OutputService } from '../../../_core/_services/output.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { FunctionUtility } from '../../../_core/_utility/function-utility';
import { Router } from '@angular/router';
import { QrcodeMainService } from '../../../_core/_services/qrcode-main.service';

@Component({
  selector: 'app-output-main',
  templateUrl: './output-main.component.html',
  styleUrls: ['./output-main.component.scss']
})
export class OutputMainComponent implements OnInit {
  outputs: OutputM[] = [];
  qrCodeId = '';
  output: any = [];
  flagFinish: boolean = false;

  constructor(
    private outputService: OutputService,
    private alertify: AlertifyService,
    private functionUtility: FunctionUtility,
    private router: Router,
    private qrCodeMainService: QrcodeMainService
  ) { }

  ngOnInit() {
    this.outputService.currentListOutputM.subscribe(res => {
      this.outputs = res;
    });
    this.outputService.currentQrCodeId.subscribe(res => {
      this.qrCodeId = res;
    });
    this.outputService.currentFlagFinish.subscribe(res => {
      this.flagFinish = res;
    });
  }

  getOutputMain(e) {
    if (e.length >= 10) {
      let flag = true;
      this.outputs.forEach((item) => {
        if (item.qrCodeId === e) {
          flag = false;
        }
      });
      if (flag) {
        this.outputService.getMainByQrCodeId(this.qrCodeId).subscribe(
          (res) => {
            if (res != null) {
              this.outputs = res.outputs;
              this.outputService.changeListMaterialSheetSize(res.materialSheetSizes);
              this.outputService.changeListOutputM(this.outputs);
            }
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      } else {
        this.alertify.error('This QRCode scanded!');
      }
    }
  }

  detail(output: OutputM) {
    this.outputService.changeOutputM(output);
    this.outputService.changeQrCodeId(this.qrCodeId);

    this.router.navigate(['output/detail']);
  }

  process(output: OutputM) {
    this.outputService.changeOutputM(output);
    this.outputService.changeQrCodeId(this.qrCodeId);

    this.router.navigate(['output/process']);
  }

  print(qrCodeId: string) {
    let qrCodeVerison = 0;
    this.qrCodeMainService.getQrCodeVersionLastest(qrCodeId).subscribe(res => {
      qrCodeVerison = res;
      this.router.navigate(['/qr/qrcode-print/' + qrCodeId + '/version/' + qrCodeVerison]);
    });
  }

  submit() {
    this.outputService.changeListMaterialSheetSize([]);
    this.outputService.changeListOutputM([]);
    this.outputService.changeFlagFinish(false);
    this.outputService.changeQrCodeId('');
    // this.outputs = [];
    // this.qrCodeId = '';
  }
}
