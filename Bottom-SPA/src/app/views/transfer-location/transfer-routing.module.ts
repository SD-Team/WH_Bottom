import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { TransferMainComponent } from './transfer-main/transfer-main.component';
import { TransferPrintComponent } from './transfer-print/transfer-print.component';

const routes: Routes = [
    {
        path: '',
        data: {
            title: 'Transfer'
        },
        children: [
            {
                path: 'main',
                component: TransferMainComponent,
                data: {
                    title: 'Transfer Main'
                }

            },
            {
                path: 'print',
                component: TransferPrintComponent,
                data: {
                    title: 'Transfer Print'
                }

            },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class TransferRoutingModule {
}
