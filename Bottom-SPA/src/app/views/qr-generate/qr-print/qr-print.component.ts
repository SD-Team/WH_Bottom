import { Component, OnInit } from '@angular/core';
import { PackingListDetailService } from '../../../_core/_services/packing-list-detail.service';
import { PackingPrintAll } from '../../../_core/_viewmodels/packing-print-all';
import { Router } from '@angular/router';

@Component({
  selector: 'app-qr-print',
  templateUrl: './qr-print.component.html',
  styleUrls: ['./qr-print.component.scss']
})
export class QrPrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  packingPrint: PackingPrintAll[] = [];
  constructor(private packingListDetailService: PackingListDetailService,
              private router: Router) { }

  ngOnInit() {
    this.packingListDetailService.currentPackingPrint.subscribe(res => this.packingPrint = res);
    this.checkPackingPrint();
  }
  checkPackingPrint() {
    if (this.packingPrint.length === 0) {
      this.router.navigate(['/qr/body']);
    }
  }
  back() {
    this.router.navigate(['/qr/body']);
  }
}
