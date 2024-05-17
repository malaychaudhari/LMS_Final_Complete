import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ManagerComponent } from './manager.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { InventoryComponent } from './inventory/inventory.component';
import { InventoryCategoryComponent } from './inventory-category/inventory-category.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { VehicleTypeComponent } from './vehicle-type/vehicle-type.component';
import { OrderComponent } from './order/order.component';
import { DriverComponent } from './driver/driver.component';
import { ResourceAssignmentComponent } from './resource-assignment/resource-assignment.component';
import { AllocatedResourceComponent } from './allocated-resource/allocated-resource.component';
import { ManagerGuard } from '../../Services/Guards/manager-guard.service';
import { NotFound404Component } from '../../Components/not-found-404/not-found-404.component';
import { OrderDetailsComponent } from '../../Components/order-details/order-details.component';

const routes: Routes = [
  {
    path: 'manager',
    component: ManagerComponent,
    canActivate: [ManagerGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [ManagerGuard],
      },
      {
        path: 'inventory',
        component: InventoryComponent,
        canActivate: [ManagerGuard],
      },
      {
        path: 'inventory-category',
        component: InventoryCategoryComponent,
        canActivate: [ManagerGuard],
      },
      {
        path: 'vehicle',
        component: VehicleComponent,
        canActivate: [ManagerGuard],
      },
      {
        path: 'vehicle-type',
        component: VehicleTypeComponent,
        canActivate: [ManagerGuard],
      },
      { path: 'order', component: OrderComponent, canActivate: [ManagerGuard] },
      {
        path: 'available-driver',
        component: DriverComponent,
        canActivate: [ManagerGuard],
      },
      {
        path: 'resource-assignment',
        component: ResourceAssignmentComponent,
        canActivate: [ManagerGuard],
      },
      {
        path: 'allocated-resource',
        component: AllocatedResourceComponent,
        canActivate: [ManagerGuard],
      },
      {
        path: 'order-details/:id',
        component: OrderDetailsComponent,
        canActivate: [ManagerGuard],
      },
      {
        path: '**',
        component: NotFound404Component,
        canActivate: [ManagerGuard],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ManagerRoutingModule {}
