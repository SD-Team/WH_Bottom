import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrintAgainRoutingModule } from './print-again-routing.module';
import { QrcodePrintComponent } from './qrcode-print/qrcode-print.component';
import { MissingPrintComponent } from './missing-print/missing-print.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule, BsDatepickerModule, AlertModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgxQRCodeModule } from 'ngx-qrcode2';
import { NgxPrintModule } from 'ngx-print';


@NgModule({
  declarations: [
    QrcodePrintComponent,
    MissingPrintComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    PaginationModule,
    NgSelectModule,
    BsDatepickerModule,
    NgxQRCodeModule,
    NgxPrintModule,
    AlertModule.forRoot(),
    PrintAgainRoutingModule
  ]
})
export class PrintAgainModule { }
