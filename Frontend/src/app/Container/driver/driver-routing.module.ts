import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DriverComponent } from './driver.component';
import { AssignedOrdersComponent } from './assigned-orders/assigned-orders.component';
import { CompletedOrdersComponent } from './completed-orders/completed-orders.component';
import { DriverGuard } from '../../Services/Guards/driver-guard.service';

const routes: Routes = [
  {
    path: 'driver',
    component: DriverComponent,
    canActivate: [DriverGuard],
    children: [
      { path: '', redirectTo: 'view-assigned-orders', pathMatch: 'full' },
      {
        path: 'view-assigned-orders',
        component: AssignedOrdersComponent,
        canActivate: [DriverGuard],
      },
      {
        path: 'view-completed-orders',
        component: CompletedOrdersComponent,
        canActivate: [DriverGuard],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DriverRoutingModule {}
