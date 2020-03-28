import { Component, OnInit, Input } from '@angular/core';
import { QRCodeMainModel } from '../../../_core/_viewmodels/qrcode-main-model';
import { PackingListDetailModel } from '../../../_core/_viewmodels/packing-list-detail-model';

@Component({
  selector: 'app-qr-print',
  templateUrl: './qr-print.component.html',
  styleUrls: ['./qr-print.component.scss']
})
export class QrPrintComponent implements OnInit {
  elementType: 'url' | 'canvas' | 'img' = 'url';
  @Input() qrCodeMainItem: QRCodeMainModel;
  @Input() packingListDetail: PackingListDetailModel[] = [];
  @Input() totalQty: number;
  constructor() { }

  ngOnInit() {

  }

}
