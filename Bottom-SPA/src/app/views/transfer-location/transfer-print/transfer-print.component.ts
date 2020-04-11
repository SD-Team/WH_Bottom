import { Component, OnInit } from '@angular/core';
import { TransferService } from '../../../_core/_services/transfer.service';
import { Router, NavigationEnd } from '@angular/router';
import { TransferM } from '../../../_core/_models/transferM';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-transfer-print',
  templateUrl: './transfer-print.component.html',
  styleUrls: ['./transfer-print.component.scss'],
})
export class TransferPrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  transfers: TransferM[] = [];
  results = [];
  today = new Date();
  private previousUrl: string = undefined;
  private currentUrl: string = undefined;
  constructor(
    private transferService: TransferService,
    private router: Router
  ) {
    this.currentUrl = this.router.url;
    router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.previousUrl = this.currentUrl;
        this.currentUrl = event.url;
      }
    });
  }

  ngOnInit() {
    this.transferService.currentTransfer.subscribe((res) => {
      this.transfers = res;

      const groups = new Set(this.transfers.map((item) => item.transferNo)),
        results = [];
      groups.forEach((g) =>
        results.push({
          name: g,
          values: this.transfers.filter((i) => i.transferNo === g),
        })
      );

      this.results = results;
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
    this.router.navigate([this.previousUrl]);
  }
}
