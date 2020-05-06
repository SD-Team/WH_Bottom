import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { OutputMainComponent } from './output-main/output-main.component';
import { OutputPrintComponent } from './output-print/output-print.component';
import { OutputDetailComponent } from './output-detail/output-detail.component';
import { OutputProcessComponent } from './output-process/output-process.component';
import { OutputPrintQrcodeAgainComponent } from './output-print-qrcode-again/output-print-qrcode-again.component';
import { OutputNavGuard } from '../../_core/_guards/output-nav.guard';


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
                path: 'print',
                component: OutputPrintComponent,
                data: {
                    title: 'Output Print'
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
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class OutputRoutingModule {
}
