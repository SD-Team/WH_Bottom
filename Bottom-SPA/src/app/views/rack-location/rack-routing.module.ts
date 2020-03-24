import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { RackMainComponent } from './rack-main/rack-main.component';



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
                //resolve: { brands: BrandListResolver },
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
