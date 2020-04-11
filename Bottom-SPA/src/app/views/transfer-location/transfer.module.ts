import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxQRCodeModule } from 'ngx-qrcode2';
import { NgxPrintModule } from 'ngx-print';
import { BsDatepickerModule } from 'ngx-bootstrap';

import { TransferMainComponent } from './transfer-main/transfer-main.component';
import { TransferRoutingModule } from './transfer-routing.module';
import { TransferPrintComponent } from './transfer-print/transfer-print.component';
import { TransferHistoryComponent } from './transfer-history/transfer-history.component';

@NgModule({
  imports: [
    CommonModule,
    TransferRoutingModule,
    FormsModule,
    NgxQRCodeModule,
    NgxPrintModule,
    BsDatepickerModule
  ],
  declarations: [
    TransferMainComponent,
    TransferPrintComponent,
    TransferHistoryComponent
  ]
})
export class TransferModule { }
