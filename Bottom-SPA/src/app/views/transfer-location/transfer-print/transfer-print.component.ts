import { Component, OnInit } from '@angular/core';
import { TransferService } from '../../../_core/_services/transfer.service';
import { Router } from '@angular/router';
import { TransferM } from '../../../_core/_models/transferM';

@Component({
  selector: 'app-transfer-print',
  templateUrl: './transfer-print.component.html',
  styleUrls: ['./transfer-print.component.scss'],
})
export class TransferPrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  transfers: TransferM[] = [];
  transferNo = '';
  today = new Date();
  constructor(
    private transferService: TransferService,
    private router: Router
  ) {}

  ngOnInit() {
    this.transferService.currentTransfer.subscribe((res) => {
      this.transfers = res;
      if (res.length > 0) {
        this.transferNo = this.transfers[0].transferNo;
      }
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
