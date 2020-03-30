import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { RackRoutingModule } from './rack-routing.module';


//Component
import { RackMainComponent } from './rack-main/rack-main.component';
import { RackFormComponent } from './rack-form/rack-form.component';
import { RackPrintComponent } from './rack-print/rack-print.component';
import { NgxQRCodeModule } from 'ngx-qrcode2';



@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        RackRoutingModule,
        PaginationModule,
        NgSelectModule,
        BsDatepickerModule,
        NgxQRCodeModule,
    ],
    declarations: [
        RackMainComponent,
        RackFormComponent,
        RackPrintComponent
    ]
})

export class RackModule {
}
