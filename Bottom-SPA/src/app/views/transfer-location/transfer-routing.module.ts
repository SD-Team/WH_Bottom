import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { TransferMainComponent } from './transfer-main/transfer-main.component';

const routes: Routes = [
    {
        path: '',
        data: {
            title: 'transfer'
        },
        children: [
            {
                path: 'main',
                component: TransferMainComponent,
                data: {
                    title: 'Transfer Main'
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
