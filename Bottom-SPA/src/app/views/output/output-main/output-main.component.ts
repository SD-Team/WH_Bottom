import { Component, OnInit } from '@angular/core';
import { OutputM } from '../../../_core/_models/outputM';
import { OutputService } from '../../../_core/_services/output.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { FunctionUtility } from '../../../_core/_utility/function-utility';
import { Router } from '@angular/router';

@Component({
  selector: 'app-output-main',
  templateUrl: './output-main.component.html',
  styleUrls: ['./output-main.component.scss']
})
export class OutputMainComponent implements OnInit {
  ouputs: OutputM[] = [];
  qrCodeId = '';
  output: any = [];

  constructor(
    private outputService: OutputService,
    private alertify: AlertifyService,
    private functionUtility: FunctionUtility,
    private router: Router
  ) {}

  ngOnInit() {
  }

  getOutputMain(e) {
    if (e.length >= 10) {
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
              this.ouputs = res.outputs;
              this.outputService.changeListMaterialSheetSize(res.materialSheetSizes);
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

  remove(qrCodeId: string) {
    this.alertify.confirm('Delete', 'Are you sure Delete', () => {
      this.ouputs.forEach((e, i) => {
        if (e.qrCodeId === qrCodeId) {
          this.ouputs.splice(i, 1);
        }
      });
    });
  }

  detail(output: OutputM) {
    this.outputService.changeOutputM(output);
    this.router.navigate(['output/detail']);
  }

}
