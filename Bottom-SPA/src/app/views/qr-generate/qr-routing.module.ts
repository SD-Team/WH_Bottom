import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { QrMainComponent } from './qr-main/qr-main.component';
import { QrPrintComponent } from './qr-print/qr-print.component';

const routes: Routes = [
    {
        path: '',
        data: {
            title: 'QR Generate'
        },
        children: [
            {
                path: 'main',
                component: QrMainComponent,
                //resolve: { brands: BrandListResolver },
                data: {
                    title: 'Search'
                }

            },
            {
                path: 'print',
                component: QrPrintComponent,
                //resolve: { brands: BrandListResolver },
                data: {
                    title: 'QR Print'
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
