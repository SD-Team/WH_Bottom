import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { OutputMainComponent } from './output-main/output-main.component';
import { OutputDetailComponent } from './output-detail/output-detail.component';
import { OutputProcessComponent } from './output-process/output-process.component';
import { OutputPrintQrcodeAgainComponent } from './output-print-qrcode-again/output-print-qrcode-again.component';


const routes: Routes = [
    {
        path: '',
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
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class OutputRoutingModule {
}
