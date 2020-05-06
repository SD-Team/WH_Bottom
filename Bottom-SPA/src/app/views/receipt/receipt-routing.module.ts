import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { ReceiptMainComponent } from './receipt-main/receipt-main.component';
import { RecordComponent } from './record/record.component';
import { RecordFormComponent } from './record-form/record-form.component';
import { RecordFormBatchesComponent } from './record-form-batches/record-form-batches.component';
import { RecordDetailComponent } from './record-detail/record-detail.component';
import { RecordEditComponent } from './record-edit/record-edit.component';
import { ReceivingMaterialNavGuard } from '../../_core/_guards/receiving-material-nav.guard';

const routes: Routes = [
    {
        path: '',
        canActivate: [ReceivingMaterialNavGuard],
        data: {
            title: 'Receipt'
        },
        children: [
            {
                path: 'main',
                component: ReceiptMainComponent,
                // resolve: { brands: BrandListResolver },
                data: {
                    title: 'Receipt'
                }
            },
            {
                path: 'record',
                children: [
                    {
                        path: '',
                        component: RecordComponent,
                        data: {
                            title: 'Record'
                        },
                    },
                    {
                        path: 'record-detail',
                        component: RecordDetailComponent,
                        data: {
                            title: 'Detail Record'
                        }
                    },
                    {
                        path: 'add',
                        component: RecordFormComponent,
                        data: {
                            title: 'Add Record'
                        }
                    },
                    {
                        path: 'add-batches',
                        component: RecordFormBatchesComponent,
                        data: {
                            title: 'Add Record'
                        }
                    },
                    {
                        path: 'record-edit',
                        component: RecordEditComponent,
                        data: {
                            title: 'Edit Record'
                        }
                    },
                ]
            },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ReceiptRoutingModule {
}
