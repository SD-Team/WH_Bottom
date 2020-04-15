import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { OutputMainComponent } from './output-main/output-main.component';
import { OutputPrintComponent } from './output-print/output-print.component';
import { OutputDetailComponent } from './output-detail/output-detail.component';


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
                path: 'print',
                component: OutputPrintComponent,
                data: {
                    title: 'Output Print'
                }
            },
            {
                path: 'detail',
                component: OutputDetailComponent,
                data: {
                    title: 'Output Detail'
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
