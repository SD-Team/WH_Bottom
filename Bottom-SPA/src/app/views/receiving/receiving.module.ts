import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReceivingRoutingModule } from './receiving-routing.module';
import { ReceivingMainComponent } from './receiving-main/receiving-main.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule, BsDatepickerModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';


@NgModule({
  declarations: [
    ReceivingMainComponent,
  ],
  imports: [
    CommonModule,
    ReceivingRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    PaginationModule,
    NgSelectModule,
    BsDatepickerModule,
  ]
})
export class ReceivingModule { }
