import { Component, OnInit } from '@angular/core';
import { OutputM } from '../../../_core/_models/outputM';
import { OutputService } from '../../../_core/_services/output.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { Router } from '@angular/router';
import { PackingListDetailService } from '../../../_core/_services/packing-list-detail.service';
import { PackingPrintAll } from '../../../_core/_viewmodels/packing-print-all';
import { InputService } from '../../../_core/_services/input.service';

@Component({
  selector: 'app-output-main',
  templateUrl: './output-main.component.html',
  styleUrls: ['./output-main.component.scss'],
})
export class OutputMainComponent implements OnInit {
  outputs: OutputM[] = [];
  qrCodeId = '';
  output: any = [];
  flagSubmit: boolean = false;
  flagFinish: boolean = false;
  packingPrintAll: PackingPrintAll[] = [];
  constructor(
    private outputService: OutputService,
    private alertify: AlertifyService,
    private inputService: InputService,
    private router: Router,
    private packingListDetailService: PackingListDetailService,
  ) { }

  ngOnInit() {
    this.outputService.currentListOutputM.subscribe((res) => {
      this.outputs = res;
    });
    this.outputService.currentQrCodeId.subscribe((res) => {
      this.qrCodeId = res;
    });
    this.outputService.currentFlagFinish.subscribe((res) => {
      this.flagFinish = res;
    });
    this.outputService.currentFlagSubmit.subscribe((res) => {
      this.flagSubmit = res;
    });
    this.inputService.clearDataChangeMenu();
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
              this.outputService.changeListOutputM(this.outputs);

              // Group by materialsheetsize theo tool_Size rồi gán vào listmaterialsheetsize trong output service để dùng chung
              const groups = new Set(
                res.materialSheetSizes.map((item) => item.tool_Size)
              ),
                results = [];
              groups.forEach((g) =>
                results.push({
                  name: g,
                  value: res.materialSheetSizes
                    .filter((i) => i.tool_Size === g)
                    .reduce((qty, j) => {
                      return (qty += j.qty);
                    }, 0),
                })
              );
              this.outputService.changeListMaterialSheetSize(results);
            }
          },
          (error) => {
            this.alertify.error(error);
          }
        );
      } else {
        this.alertify.error('This QRCode scanded!');
      }

      this.outputService.changeFlagSubmit(false);
    }
  }

  detail(output: OutputM) {
    this.router.navigate(['output/detail', output.transacNo]);
  }

  process(output: OutputM) {
    this.outputService.changeOutputM(output);
    this.outputService.changeQrCodeId(this.qrCodeId);

    this.router.navigate(['output/process']);
  }

  print(qrCodeId: string, qrCodeVersion: number) {
    this.packingListDetailService.changePrintQrCodeAgain('2');
    let qrCodeIdVersion = [];
    let item = {
      qrCode_ID: qrCodeId,
      qrCode_Version: qrCodeVersion
    }
    qrCodeIdVersion.push(item);
    this.packingListDetailService.findByQrCodeIdList(qrCodeIdVersion).subscribe(res => {
      this.packingPrintAll = res;
      this.packingListDetailService.changePackingPrint(this.packingPrintAll);
      this.router.navigate(['/qr/print']);
    })
  }

  submit() {
    // flag submit == false
    this.flagSubmit = true;
    this.outputService.changeFlagSubmit(true);
    // kiểm tra output nào mà không output ra(không process) thì loại bỏ
    this.outputs.forEach((e, i) => {
      if (e.transOutQty === 0) {
        this.outputs.splice(i, 1);
      }
    });
    // gửi lên server để update transacsheet no những output được process
    this.outputService.submitOutput(this.outputs).subscribe(
      () => {
        this.alertify.success('Submit succeed');
      },
      (error) => {
        this.alertify.error(error);
      }
    );

    // gán lại mấy giá trị dùng chung trên service thành giá trị mặc định ban đầu để ouput đơn khác không bị nhớ ouput cũ
    this.outputService.changeListMaterialSheetSize([]);
    this.outputService.changeFlagFinish(false);
    this.outputService.changeQrCodeId('');
    const  outputM: OutputM = {
      transacNo: '',
      qrCodeId: '',
      planNo: '',
      supplierNo: '',
      supplierName: '',
      batch: '',
      matId: '',
      matName: '',
      wh: '',
      building: '',
      area: '',
      rackLocation: '',
      inStockQty: 0,
      transOutQty: 0,
      remainingQty: 0,
      pickupNo: '',
      qrCodeVersion: 0
    };
    this.outputService.changeOutputM(outputM);
  }
}
