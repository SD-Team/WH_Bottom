import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { QrMainComponent } from './qr-main/qr-main.component';
import { QrPrintComponent } from './qr-print/qr-print.component';
import { QrBodyComponent } from './qr-body/qr-body.component';
import { QrcodePrintComponent } from './qrcode-print/qrcode-print.component';
import { QrGenerateNavGuard } from '../../_core/_guards/qr-generate-nav.guard';

const routes: Routes = [
    {
        path: '',
        canActivate: [QrGenerateNavGuard],
        data: {
            title: 'QR Generate'
        },
        children: [
            {
                path: 'main',
                component: QrMainComponent,
                // resolve: { brands: BrandListResolver },
                data: {
                    title: 'Search'
                }

            },
            {
                path: 'print',
                component: QrPrintComponent,
                // resolve: { brands: BrandListResolver },
                data: {
                    title: 'QR Print'
                }
            },
            {
                path: 'body',
                component: QrBodyComponent,
                // resolve: { brands: BrandListResolver },
                data: {
                    title: 'QR Body'
                }
            },
            {
                path: 'qrcode-print/:qrCodeId/version/:qrCodeVersion',
                component: QrcodePrintComponent,
                data: {
                    title: 'QRCode Print'
                }
            },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class QrRoutingModule {
}
