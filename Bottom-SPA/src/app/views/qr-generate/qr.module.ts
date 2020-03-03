import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { QrRoutingModule } from './qr-routing.module';


//Component
import { QrMainComponent } from './qr-main/qr-main.component';
import { QrPrintComponent } from './qr-print/qr-print.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        QrRoutingModule,
        PaginationModule,
        NgSelectModule,
        BsDatepickerModule
    ],
    declarations: [
        QrMainComponent,
        QrPrintComponent
    ]
})


export class QrModule {
}
