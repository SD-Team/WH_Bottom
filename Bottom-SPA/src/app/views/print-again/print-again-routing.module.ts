import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { QrGenerateNavGuard } from '../../_core/_guards/qr-generate-nav.guard';
import { QrcodePrintComponent } from './qrcode-print/qrcode-print.component';
import { MissingPrintComponent } from './missing-print/missing-print.component';


const routes: Routes = [
  {
    path: '',
    canActivate: [QrGenerateNavGuard],
    data: {
        title: 'QR Generate'
    },
    children: [
        {
            path: 'qrcode-print',
            component: QrcodePrintComponent,
            data: {
                title: 'QR Print'
            }

        },
        {
            path: 'missing-print',
            component: MissingPrintComponent,
            data: {
                title: 'Missing Print'
            }
        },
    ]
}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PrintAgainRoutingModule { }
