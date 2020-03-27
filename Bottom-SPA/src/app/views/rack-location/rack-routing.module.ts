import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { RackMainComponent } from './rack-main/rack-main.component';
import { RackListResolver } from '../../_core/_resolvers/rack-list.resolver';
import { RackFormComponent } from './rack-form/rack-form.component';



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
            {
                path: 'form',
                component: RackFormComponent,
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
