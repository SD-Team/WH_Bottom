import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { OutputRoutingModule } from './output-routing.module';

// Component
import { OutputMainComponent } from './output-main/output-main.component';
import { OutputPrintComponent } from './output-print/output-print.component';
import { OutputDetailComponent } from './output-detail/output-detail.component';
import { NgxQRCodeModule } from 'ngx-qrcode2';
import { NgxPrintModule } from 'ngx-print';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        OutputRoutingModule,
        PaginationModule,
        NgSelectModule,
        BsDatepickerModule,
        NgxQRCodeModule,
        NgxPrintModule,
    ],
    declarations: [
        OutputMainComponent,
        OutputPrintComponent,
        OutputDetailComponent
    ]
})

export class OutputModule {
}
