import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { HistoryComponent } from './history/history.component';

const routes: Routes = [
    {
        path: '',
        component: HistoryComponent,
        data: {
            title: 'IN/OUT History'
        },
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class HistoryRoutingModule {
}
