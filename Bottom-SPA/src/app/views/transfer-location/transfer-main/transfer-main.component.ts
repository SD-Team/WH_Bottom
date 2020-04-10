import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { TransferService } from '../../../_core/_services/transfer.service';

@Component({
  selector: 'app-transfer-main',
  templateUrl: './transfer-main.component.html',
  styleUrls: ['./transfer-main.component.scss'],
})
export class TransferMainComponent implements OnInit {
  result: any = [];
  qrCodeID = '';

  constructor(
    private transferService: TransferService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.result = [];
  }

  getInputMain(e) {
    console.log(e.length);
    if (e.length === 14) {
      let flag = true;
      this.result.forEach((item) => {
        if (item.qrCode_Id === e) {
          flag = false;
        }
      });
      if (flag) {
        this.transferService.getMainByQrCodeID(this.qrCodeID).subscribe(
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
      this.qrCodeID = '';
    }
  }
}
