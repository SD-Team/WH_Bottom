import { Component, OnInit, OnDestroy } from '@angular/core';
import { TransferService } from '../../../_core/_services/transfer.service';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { TransferM } from '../../../_core/_models/transferM';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-transfer-print',
  templateUrl: './transfer-print.component.html',
  styleUrls: ['./transfer-print.component.scss'],
})
export class TransferPrintComponent implements OnInit, OnDestroy {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  transfers: TransferM[] = [];
  resultsPrint = [];
  today = new Date();

  constructor(
    private transferService: TransferService,
    private router: Router
  ) {
  }
  ngOnDestroy(): void {
    this.transferService.changePrintTransfer([]);
  }

  ngOnInit() {
    this.transferService.currentTransfer.subscribe((res) => {
      this.transfers = res;

      // Group by theo transferNo
      const groups = new Set(this.transfers.map((item) => item.transferNo)),
        results = [];
      groups.forEach((g) =>
        results.push({
          name: g,
          values: this.transfers.filter((i) => i.transferNo === g),
        })
      );

      this.resultsPrint = results;
    });
  }

  print(e) {
    e.preventDefault();
    const printContents = document.getElementById('wrap-print').innerHTML;
    const originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
  }

  back() {
    this.router.navigate(['/transfer/main']);
  }
}
