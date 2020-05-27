import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { OutputMainComponent } from './output-main/output-main.component';
import { OutputDetailComponent } from './output-detail/output-detail.component';
import { OutputProcessComponent } from './output-process/output-process.component';
import { OutputPrintQrcodeAgainComponent } from './output-print-qrcode-again/output-print-qrcode-again.component';
import { OutputNavGuard } from '../../_core/_guards/output-nav.guard';
import { OutputDetailPreviewComponent } from './output-detail-preview/output-detail-preview.component';


const routes: Routes = [
    {
        path: '',
        canActivate: [OutputNavGuard],
        data: {
            title: 'Output'
        },
        children: [
            {
                path: 'main',
                component: OutputMainComponent,
                data: {
                    title: 'Scan'
                }

            },
            {
                path: 'detail/:transacNo',
                component: OutputDetailComponent,
                data: {
                    title: 'Output Detail'
                }
            },
            {
                path: 'process',
                component: OutputProcessComponent,
                data: {
                    title: 'Output Process'
                }
            },
            {
                path: 'print-qrcode-again/:qrCodeId/version/:qrCodeVersion',
                component: OutputPrintQrcodeAgainComponent,
                data: {
                    title: 'Output Process'
                }
            },
            {
                path: 'detail-preview/:index',
                component: OutputDetailPreviewComponent,
                data: {
                    title: 'Output Preview Detail'
                }
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class OutputRoutingModule {
}
