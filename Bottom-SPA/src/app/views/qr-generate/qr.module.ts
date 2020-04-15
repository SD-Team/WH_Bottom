import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { QrRoutingModule } from './qr-routing.module';
import { NgxQRCodeModule } from 'ngx-qrcode2';
// Component
import { QrMainComponent } from './qr-main/qr-main.component';
import { QrPrintComponent } from './qr-print/qr-print.component';
import { QrBodyComponent } from './qr-body/qr-body.component';
import { QrcodePrintComponent } from './qrcode-print/qrcode-print.component';
import { NgxPrintModule } from 'ngx-print';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        QrRoutingModule,
        PaginationModule,
        NgSelectModule,
        BsDatepickerModule,
        NgxQRCodeModule,
        NgxPrintModule
    ],
    declarations: [
        QrMainComponent,
        QrPrintComponent,
        QrBodyComponent,
        QrcodePrintComponent
    ]
})


export class QrModule {
}
