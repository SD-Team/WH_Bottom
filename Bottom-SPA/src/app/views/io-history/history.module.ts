import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { PaginationModule } from 'ngx-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { HistoryRoutingModule } from './history-routing.module';

//Component
import { HistoryComponent } from './history/history.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        HistoryRoutingModule,
        PaginationModule,
        NgSelectModule,
        BsDatepickerModule
    ],
    declarations: [
        HistoryComponent
    ]
})

export class HistoryModule {
}
