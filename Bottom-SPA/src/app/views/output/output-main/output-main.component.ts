import { Component, OnInit } from '@angular/core';
import { OutputM } from '../../../_core/_models/outputM';
import { OutputService } from '../../../_core/_services/output.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { FunctionUtility } from '../../../_core/_utility/function-utility';

@Component({
  selector: 'app-output-main',
  templateUrl: './output-main.component.html',
  styleUrls: ['./output-main.component.scss']
})
export class OutputMainComponent implements OnInit {
  ouputs: OutputM[] = [];
  qrCodeId = '';
  outputSheetNo = '';

  constructor(
    private outputService: OutputService,
    private alertify: AlertifyService,
    private functionUtility: FunctionUtility
  ) {}

  ngOnInit() {
    // lấy ra transferNo mới theo yêu cầu: TB(ngày thực hiện yyyymmdd) 3 mã số random number. (VD: TB20200310001)
    this.outputSheetNo = this.functionUtility.getOutSheetNo();
  }

  getOutputMain(e) {
    if (e.length >= 14) {
      let flag = true;
      this.ouputs.forEach((item) => {
        if (item.qrCodeId === e) {
          flag = false;
        }
      });
      if (flag) {
        this.outputService.getMainByQrCodeId(this.qrCodeId).subscribe(
          (res) => {
            if (res != null) {
              res.outputSheetNo = this.outputSheetNo;
              this.ouputs.push(res);
              debugger
            }
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      } else {
        this.alertify.error('This QRCode scanded!');
      }
      this.qrCodeId = '';
    }
  }

  remove(qrCodeId: string) {
    this.alertify.confirm('Delete', 'Are you sure Delete', () => {
      this.ouputs.forEach((e, i) => {
        if (e.qrCodeId === qrCodeId) {
          this.ouputs.splice(i, 1);
        }
      });
    });
  }

}
