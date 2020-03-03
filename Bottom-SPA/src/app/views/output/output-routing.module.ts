import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { OutputMainComponent } from './output-main/output-main.component';
import { OutputPrintComponent } from './output-print/output-print.component';


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
                //resolve: { brands: BrandListResolver },
                data: {
                    title: 'Scan'
                }

            },
            {
                path: 'print',
                component: OutputPrintComponent,
                //resolve: { brands: BrandListResolver },
                data: {
                    title: 'Output Print'
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
