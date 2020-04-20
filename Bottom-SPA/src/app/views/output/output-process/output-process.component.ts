import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MaterialSheetSize } from '../../../_core/_models/material-sheet-size';
import { OutputService } from '../../../_core/_services/output.service';
import { TransferService } from '../../../_core/_services/transfer.service';
import { TransferDetail } from '../../../_core/_models/transfer-detail';
import { OutputM } from '../../../_core/_models/outputM';
import { FunctionUtility } from '../../../_core/_utility/function-utility';
import { AlertifyService } from '../../../_core/_services/alertify.service';

@Component({
  selector: 'app-output-process',
  templateUrl: './output-process.component.html',
  styleUrls: ['./output-process.component.scss'],
})
export class OutputProcessComponent implements OnInit {
  materialSheetSize: MaterialSheetSize[] = [];
  transactionDetails: TransferDetail[] = [];
  result1 = [];
  result2 = [];
  result3 = [];
  output: any = [];

  constructor(
    private router: Router,
    private outputService: OutputService,
    private transferService: TransferService,
    private route: ActivatedRoute,
    private functionUtility: FunctionUtility,
    private alertify: AlertifyService
  ) {}

  ngOnInit() {
    this.outputService.currentOutputM.subscribe((res) => {
      this.output = res;
    });

    this.outputService.currentListMaterialSheetSize.subscribe((res) => {
      this.materialSheetSize = res;

      // Group by theo tool_Size
      const groups = new Set(
          this.materialSheetSize.map((item) => item.tool_Size)
        ),
        results = [];
      groups.forEach((g) =>
        results.push({
          name: g,
          value: this.materialSheetSize
            .filter((i) => i.tool_Size === g)
            .reduce((qty, j) => {
              return (qty += j.qty);
            }, 0),
        })
      );
      this.result1 = results;
    });

    this.getData();
  }

  back() {
    this.router.navigate(['output/main']);
  }
  getData() {
    this.transferService
      .getTransferDetail(this.output.transacNo)
      .subscribe((res) => {
        this.transactionDetails = res;

        // Group by theo tool_Size
        const groups = new Set(
            this.transactionDetails.map((item) => item.tool_Size)
          ),
          results = [];
        groups.forEach((g) =>
          results.push({
            name: g,
            value: this.transactionDetails
              .filter((i) => i.tool_Size === g)
              .reduce((instock_Qty, j) => {
                return (instock_Qty += j.instock_Qty);
              }, 0),
            array: this.transactionDetails.filter((i) => i.tool_Size === g),
          })
        );
        this.result2 = results;

        for (let i = 0; i < this.result1.length; i++) {
          this.result3.push({
            value:
              this.result1[i].value > this.result2[i].value
                ? this.result2[i].value
                : this.result1[i].value,
            name: this.result1[i].name,
          });
        }
      });
  }

  save() {
    let listOutputM: OutputM[];
    this.outputService.currentListOutputM.subscribe(
      (res) => (listOutputM = res)
    );
    const indexOutput = listOutputM.indexOf(this.output);

    this.output.transacNo = this.functionUtility.getOutSheetNo(
      this.output.planNo
    );
    this.output.transOutQty = this.result3.reduce((value, i) => {
      return (value += i.value);
    }, 0);
    this.output.remainingQty = this.output.inStockQty - this.output.transOutQty;
    this.outputService.currentQrCodeId.subscribe((res) => {
      this.output.pickupNo = res;
    });

    if (indexOutput !== -1) {
      listOutputM[indexOutput] = this.output;
    }

    const tmpTranssactionDetails = [];
    this.result3.forEach((i) => {
      this.result2.forEach((j) => {
        if (i.name === j.name) {
          j.array.forEach((k) => {
            if (i.value > k.instock_Qty) {
              const tmpInstock_Qty = k.instock_Qty;
              k.instock_Qty = 0;
              i.value = i.value - tmpInstock_Qty;
              k.qty = k.trans_Qty;
              k.trans_Qty = tmpInstock_Qty;
            } else if (i.value !== 0) {
              k.instock_Qty = k.instock_Qty - i.value;
              k.qty = k.trans_Qty;
              k.trans_Qty = i.value;
              i.value = 0;
            }
            tmpTranssactionDetails.push(k);
          });
        }
      });
    });

    this.outputService
      .saveOutput(this.output, tmpTranssactionDetails)
      .subscribe(
        () => {
          this.alertify.success('Save succeed');
        },
        (error) => {
          this.alertify.error(error);
        }
      );

    this.outputService.changeListOutputM(listOutputM);
    this.outputService.changeFlagFinish(true);
    this.router.navigate(['/output/main']);
  }
}
