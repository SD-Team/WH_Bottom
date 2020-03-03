import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ReceiptRoutingModule } from './receipt-routing.module';
import { PaginationModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

//Component
import { ReceiptMainComponent } from './receipt-main/receipt-main.component';
import { RecordComponent } from './record/record.component';
import { RecordFormComponent } from './record-form/record-form.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        ReceiptRoutingModule,
        PaginationModule,
        NgSelectModule,
        BsDatepickerModule
    ],
    declarations: [
        ReceiptMainComponent,
        RecordComponent,
        RecordFormComponent
    ]
})

export class ReceiptModule {
}
