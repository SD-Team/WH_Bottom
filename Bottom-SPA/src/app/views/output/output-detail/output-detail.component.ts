import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MaterialSheetSize } from '../../../_core/_models/material-sheet-size';
import { OutputService } from '../../../_core/_services/output.service';
import { TransferService } from '../../../_core/_services/transfer.service';
import { TransferDetail } from '../../../_core/_models/transfer-detail';
import { OutputM } from '../../../_core/_models/outputM';

@Component({
  selector: 'app-output-detail',
  templateUrl: './output-detail.component.html',
  styleUrls: ['./output-detail.component.scss'],
})
export class OutputDetailComponent implements OnInit {
  materialSheetSize: MaterialSheetSize[] = [];
  transactionDetails: TransferDetail[] = [];
  result1 = [];
  result2 = [];
  output: any = [];

  constructor(
    private router: Router,
    private outputService: OutputService,
    private transferService: TransferService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.outputService.currentOutputM.subscribe(res => {
      this.output = res;
    });

    this.getData();
    this.outputService.currentListMaterialSheetSize.subscribe((res) => {
      this.materialSheetSize = res;

      // Group by theo transferNo
      const groups = new Set(this.materialSheetSize.map((item) => item.tool_Size)),
        results = [];
      groups.forEach((g) =>
        results.push({
          name: g,
          value: this.materialSheetSize.filter((i) => i.tool_Size === g).reduce((qty, j) => {
            return qty += j.qty;
          }, 0)
        })
      );
      this.result1 = results;
    });
  }

  back() {
    this.router.navigate(['output/main']);
  }
  getData() {
    this.transferService.getTransferDetail(this.output.transacNo).subscribe(res => {
      this.transactionDetails = res;

      // Group by theo transferNo
      const groups = new Set(this.transactionDetails.map((item) => item.tool_Size)),
        results = [];
      groups.forEach((g) =>
        results.push({
          name: g,
          value: this.transactionDetails.filter((i) => i.tool_Size === g).reduce((instock_Qty, j) => {
            return instock_Qty += j.instock_Qty;
          }, 0),
          value2: this.transactionDetails.filter((i) => i.tool_Size === g).reduce((instock_Qty, j) => {
            return instock_Qty += j.instock_Qty;
          }, 0),
        })
      );
      this.result2 = results;
    });
  }
}
