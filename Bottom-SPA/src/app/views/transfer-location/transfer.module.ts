import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxQRCodeModule } from 'ngx-qrcode2';
import { NgxPrintModule } from 'ngx-print';
import { BsDatepickerModule, PaginationModule, AlertModule } from 'ngx-bootstrap';

import { TransferMainComponent } from './transfer-main/transfer-main.component';
import { TransferRoutingModule } from './transfer-routing.module';
import { TransferPrintComponent } from './transfer-print/transfer-print.component';
import { TransferHistoryComponent } from './transfer-history/transfer-history.component';
import { TransferDetailComponent } from './transfer-detail/transfer-detail.component';

@NgModule({
  imports: [
    CommonModule,
    TransferRoutingModule,
    FormsModule,
    NgxQRCodeModule,
    NgxPrintModule,
    BsDatepickerModule,
    PaginationModule.forRoot(),
    AlertModule.forRoot()
  ],
  declarations: [
    TransferMainComponent,
    TransferPrintComponent,
    TransferHistoryComponent,
    TransferDetailComponent
  ]
})
export class TransferModule { }
