import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { InputRoutingModule } from './input-routing.module';

//Component
import { InputMainComponent } from './input-main/input-main.component';
import { InputPrintComponent } from './input-print/input-print.component';
import { MissingPrintComponent } from './missing-print/missing-print.component';
import { NgxQRCodeModule } from 'ngx-qrcode2';
import { NgxPrintModule } from 'ngx-print';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        InputRoutingModule,
        PaginationModule,
        NgSelectModule,
        BsDatepickerModule,
        NgxQRCodeModule,
        NgxPrintModule,
    ],
    declarations: [
        InputMainComponent,
        InputPrintComponent,
        MissingPrintComponent
    ]
})


export class InputModule {
}
