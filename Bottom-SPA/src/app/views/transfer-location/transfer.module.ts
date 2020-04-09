import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TransferMainComponent } from './transfer-main/transfer-main.component';
import { TransferRoutingModule } from './transfer-routing.module';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    TransferRoutingModule,
    FormsModule
  ],
  declarations: [
    TransferMainComponent
  ]
})
export class TransferModule { }
