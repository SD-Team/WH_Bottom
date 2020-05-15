import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { InputMainComponent } from './input-main/input-main.component';
import { InputPrintComponent } from './input-print/input-print.component';
import { MissingPrintComponent } from './missing-print/missing-print.component';
import { QrcodeAgainComponent } from './qrcode-again/qrcode-again.component';
import { InputNavGuard } from '../../_core/_guards/input-nav.guard';
import { MissingAgainComponent } from './missing-again/missing-again.component';

const routes: Routes = [
  {
    path: '',
    // canActivate: [InputNavGuard],
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
        path: 'missing-print/:missingNo',
        component: MissingPrintComponent,
        data: {
          title: 'Missing Print'
        }
      },
      {
        path: 'qrcode-again',
        component: QrcodeAgainComponent,
        data: {
          title: 'QrCode Print'
        }
      },
      {
        path: 'missing-again',
        component: MissingAgainComponent,
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

