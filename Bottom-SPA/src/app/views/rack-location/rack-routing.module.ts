import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { RackMainComponent } from './rack-main/rack-main.component';
import { RackListResolver } from '../../_core/_resolvers/rack-list.resolver';



const routes: Routes = [
    {
        path: '',
        data: {
            title: 'rack'
        },
        children: [
            {
                path: 'main',
                component: RackMainComponent,
                resolve: { racks: RackListResolver },
                data: {
                    title: 'Rack Location'
                }

            },
            
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class RackRoutingModule {
}
