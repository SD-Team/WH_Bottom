import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Import Containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { AuthGuard } from './_core/_guards/auth.guard';

export const routes: Routes = [
         {
           path: "",
          // canActivate: [AuthGuard],
           redirectTo: "dashboard",
           pathMatch: "full"
         },
         {
           path: "404",
           component: P404Component,
           data: {
             title: "Page 404"
           }
         },
         {
           path: "500",
           component: P500Component,
           data: {
             title: "Page 500"
           }
         },
         {
           path: "login",
           component: LoginComponent,
           data: {
             title: "Login Page"
           }
         },
         {
           path: "register",
           component: RegisterComponent,
           data: {
             title: "Register Page"
           }
         },
         {
           path: "",
           component: DefaultLayoutComponent,
           data: {
             title: "Home"
           },
           children: [
             {
               path: "base",
               loadChildren: () =>
                 import("./views/base/base.module").then(m => m.BaseModule)
             },
             {
               path: "buttons",
               loadChildren: () =>
                 import("./views/buttons/buttons.module").then(
                   m => m.ButtonsModule
                 )
             },
             {
               path: "charts",
               loadChildren: () =>
                 import("./views/chartjs/chartjs.module").then(
                   m => m.ChartJSModule
                 )
             },
             {
               path: "dashboard",
              // runGuardsAndResolvers: 'always',
              // canActivate: [AuthGuard],
               loadChildren: () =>
                 import("./views/dashboard/dashboard.module").then(
                   m => m.DashboardModule
                 )
             },
             {
               path: "receipt",
              // runGuardsAndResolvers: 'always',
              // canActivate: [AuthGuard],
               loadChildren: () =>
                 import("./views/receipt/receipt.module").then(
                   m => m.ReceiptModule
                 )
             },
             {
               path: "qr",
               // runGuardsAndResolvers: 'always',
               // canActivate: [AuthGuard],
               loadChildren: () =>
                 import("./views/qr-generate/qr.module").then(
                   m => m.QrModule
                 )
             },
             {
               path: "input",
               // runGuardsAndResolvers: 'always',
               // canActivate: [AuthGuard],
               loadChildren: () =>
                 import("./views/input/input.module").then(
                   m => m.InputModule
                 )
             },
             {
               path: "output",
               // runGuardsAndResolvers: 'always',
               // canActivate: [AuthGuard],
               loadChildren: () =>
                 import("./views/output/output.module").then(
                   m => m.OutputModule
                 )
             },
             {
               path: "io-history",
               // runGuardsAndResolvers: 'always',
               // canActivate: [AuthGuard],
               loadChildren: () =>
                 import("./views/io-history/history.module").then(
                   m => m.HistoryModule
                 )
             },
             {
               path: "rack",
               // runGuardsAndResolvers: 'always',
               // canActivate: [AuthGuard],
               loadChildren: () =>
                 import("./views/rack-location/rack.module").then(
                   m => m.RackModule
                 )
             },
             {
              path: "receiving",
              // runGuardsAndResolvers: 'always',
              // canActivate: [AuthGuard],
              loadChildren: () =>
                import("./views/receiving/receiving.module").then(
                  m => m.ReceivingModule
                )
            },
             {
               path: "icons",
               loadChildren: () =>
                 import("./views/icons/icons.module").then(m => m.IconsModule)
             },
             {
               path: "notifications",
               loadChildren: () =>
                 import("./views/notifications/notifications.module").then(
                   m => m.NotificationsModule
                 )
             },
             {
               path: "theme",
               loadChildren: () =>
                 import("./views/theme/theme.module").then(m => m.ThemeModule)
             },
             {
               path: "widgets",
               loadChildren: () =>
                 import("./views/widgets/widgets.module").then(
                   m => m.WidgetsModule
                 )
             }
           ]
         },
         { path: "**", component: P404Component }
       ];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
