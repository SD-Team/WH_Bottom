import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MaterialSheetSize } from '../../../_core/_models/material-sheet-size';
import { OutputService } from '../../../_core/_services/output.service';
import { TransferService } from '../../../_core/_services/transfer.service';
import { TransferDetail } from '../../../_core/_models/transfer-detail';
import { OutputM } from '../../../_core/_models/outputM';
import { FunctionUtility } from '../../../_core/_utility/function-utility';
import { OutputDetail } from '../../../_core/_models/output-detail';

@Component({
  selector: 'app-output-detail',
  templateUrl: './output-detail.component.html',
  styleUrls: ['./output-detail.component.scss'],
})
export class OutputDetailComponent implements OnInit {
  outputDetail: any = [];
  result1 = [];
  transacNo: string = '';

  constructor(
    private router: Router,
    private outputService: OutputService,
    private transferService: TransferService,
    private route: ActivatedRoute,
    private functionUtility: FunctionUtility
  ) { }

  ngOnInit() {
    this.transacNo = this.route.snapshot.params['transacNo'];
    this.getData();
  }

  back() {
    this.router.navigate(['output/main']);
  }
  getData() {
    this.outputService.getOutputDetail(this.transacNo).subscribe(res => {
      debugger
      this.outputDetail = res;

      // Group by theo tool_Size
      const groups = new Set(this.outputDetail.transactionDetail.map((item) => item.tool_Size)), results = [];
      groups.forEach((g) =>
        results.push({
          name: g,
          value: this.outputDetail.transactionDetail.filter((i) => i.tool_Size === g).reduce((trans_Qty, j) => {
            return trans_Qty += j.trans_Qty;
          }, 0),
          colspan: this.outputDetail.transactionDetail.filter((i) => i.tool_Size === g).length
        })
      );
      this.result1 = results;
    });
  }
}
