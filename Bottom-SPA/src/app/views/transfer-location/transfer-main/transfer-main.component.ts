import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { TransferService } from '../../../_core/_services/transfer.service';
import { TransferM } from '../../../_core/_models/transferM';
import { FunctionUtility } from '../../../_core/_utility/function-utility';
import { InputService } from '../../../_core/_services/input.service';

@Component({
  selector: 'app-transfer-main',
  templateUrl: './transfer-main.component.html',
  styleUrls: ['./transfer-main.component.scss'],
})
export class TransferMainComponent implements OnInit {
  transfers: TransferM[] = [];
  qrCodeId = '';
  toLocation = '';
  transferNo = '';
  flagSubmit = false;

  constructor(
    private transferService: TransferService,
    private alertify: AlertifyService,
    private route: ActivatedRoute,
    private inputService: InputService,
    private router: Router,
    private functionUtility: FunctionUtility
  ) {}

  ngOnInit() {
    // lấy ra transferNo mới theo yêu cầu: TB(ngày thực hiện yyyymmdd) 3 mã số random number. (VD: TB20200310001)
    this.transferNo = this.functionUtility.getTransferNo();
    this.inputService.changeListInputMain([]);
    this.inputService.changeFlag('');
  }

  getTransferMain(e) {
    if (e.length >= 14) {
      let flag = true;
      this.transfers.forEach((item) => {
        if (item.qrCodeId === e) {
          flag = false;
        }
      });
      if (flag) {
        this.transferService.getMainByQrCodeId(this.qrCodeId).subscribe(
          (res) => {
            debugger
            if (res != null) {
              res.transferNo = this.transferNo;
              res.toLocation = this.toLocation;
              this.transfers.push(res);
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
      this.transfers.forEach((e, i) => {
        if (e.qrCodeId === qrCodeId) {
          this.transfers.splice(i, 1);
        }
      });
    });
  }

  submitMain() {
    this.flagSubmit = true;
    this.transferService.submitMain(this.transfers).subscribe(
      () => {
        this.alertify.success('Submit succeed');
      },
      (error) => {
        this.alertify.error(error);
      }
    );
  }

  printMain() {
    this.transferService.changePrintTransfer(this.transfers);
    this.router.navigate(['/transfer/print']);
  }

  // khi thay đổi to location thì thay đổi transferNo
  changeTransferNo() {
    this.transferNo = this.functionUtility.getTransferNo();
  }
}
