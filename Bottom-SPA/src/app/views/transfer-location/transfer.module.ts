import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxQRCodeModule } from 'ngx-qrcode2';
import { NgxPrintModule } from 'ngx-print';

import { TransferMainComponent } from './transfer-main/transfer-main.component';
import { TransferRoutingModule } from './transfer-routing.module';
import { TransferPrintComponent } from './transfer-print/transfer-print.component';

@NgModule({
  imports: [
    CommonModule,
    TransferRoutingModule,
    FormsModule,
    NgxQRCodeModule,
    NgxPrintModule
  ],
  declarations: [
    TransferMainComponent,
    TransferPrintComponent
  ]
})
export class TransferModule { }
