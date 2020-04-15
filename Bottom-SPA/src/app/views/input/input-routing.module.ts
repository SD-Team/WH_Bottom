import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { InputMainComponent } from './input-main/input-main.component';
import { InputPrintComponent } from './input-print/input-print.component';
import { MissingPrintComponent } from './missing-print/missing-print.component';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Input'
    },
    children: [
      {
        path: 'main',
        component: InputMainComponent,
        //resolve: { brands: BrandListResolver },
        data: {
          title: 'Scan'
        }

      },
      {
        path: 'print',
        component: InputPrintComponent,
        //resolve: { brands: BrandListResolver },
        data: {
          title: 'Input Print'
        }
      },
      {
        path: 'missing-print',
        component: MissingPrintComponent,
        data: {
          title: 'Missing Print'
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class InputRoutingModule {
}

