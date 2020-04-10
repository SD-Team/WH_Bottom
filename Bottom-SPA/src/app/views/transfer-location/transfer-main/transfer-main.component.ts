import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { TransferService } from '../../../_core/_services/transfer.service';
import { TransferM } from '../../../_core/_models/transferM';

@Component({
  selector: 'app-transfer-main',
  templateUrl: './transfer-main.component.html',
  styleUrls: ['./transfer-main.component.scss'],
})
export class TransferMainComponent implements OnInit {
  result: TransferM[];
  qrCodeId = '';
  toLocation = '';
  flagSubmit = false;

  constructor(
    private transferService: TransferService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.result = [];
  }

  getTransferMain(e) {
    if (e.length === 14) {
      let flag = true;
      this.result.forEach((item) => {
        if (item.qrCodeId === e) {
          flag = false;
        }
      });
      if (flag) {
        this.transferService.getMainByQrCodeId(this.qrCodeId).subscribe(
          (res) => {
            if (res != null) {
              this.result.push(res);
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
    this.result.forEach((e, i) => {
      if (e.qrCodeId === qrCodeId) {
        this.result.splice(i, 1);
      }
    });
  }

  submitMain() {
    this.flagSubmit = true;
    this.transferService.submitMain(this.result).subscribe(
      () => {
        this.alertify.success('Submit succeed');
      },
      error => {
        this.alertify.error(error);
      }
    )
  }
}
