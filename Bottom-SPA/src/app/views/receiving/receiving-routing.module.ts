import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReceivingMainComponent } from './receiving-main/receiving-main.component';


const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Receiving Material '
    },
    children:
    [
      {
        path: 'main',
        component: ReceivingMainComponent,
        // resolve: { brands: BrandListResolver },
        data: {
            title: 'Search'
        }

      },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReceivingRoutingModule { }
