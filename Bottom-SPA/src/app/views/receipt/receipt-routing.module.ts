import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { ReceiptMainComponent } from './receipt-main/receipt-main.component';
import { RecordComponent } from './record/record.component';
import { RecordFormComponent } from './record-form/record-form.component';
import { RecordFormBatchesComponent } from './record-form-batches/record-form-batches.component';

const routes: Routes = [
    {
        path: '',
        data: {
            title: 'Receipt'
        },
        children: [
            {
                path: 'main',
                component: ReceiptMainComponent,
                //resolve: { brands: BrandListResolver },
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
                    }
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
