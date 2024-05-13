import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManagerRoutingModule } from './manager-routing.module';
import { ManagerComponent } from './manager.component';
import { InventoryComponent } from './inventory/inventory.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { SideNavComponent } from '../../Components/side-nav/side-nav.component';
import { InventoryCategoryComponent } from './inventory-category/inventory-category.component';
import { VehicleComponent } from './vehicle/vehicle.component';
import { VehicleTypeComponent } from './vehicle-type/vehicle-type.component';
import { OrderComponent } from './order/order.component';
import { DriverComponent } from './driver/driver.component';
import { ResourceAssignmentComponent } from './resource-assignment/resource-assignment.component';
import { NavbarComponent } from '../../Components/navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { NzTableModule } from 'ng-zorro-antd/table';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { ManageInventoryComponent } from './inventory/manage-inventory/manage-inventory.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ManageInventoryCategoryComponent } from './inventory-category/manage-inventory-category/manage-inventory-category.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { AllocatedResourceComponent } from './allocated-resource/allocated-resource.component';

@NgModule({
  declarations: [
    // ManagerComponent,
    InventoryComponent,
    DashboardComponent,
    InventoryCategoryComponent,
    VehicleComponent,
    VehicleTypeComponent,
    OrderComponent,
    DriverComponent,
    ResourceAssignmentComponent,
    // SideNavComponent,
    // NavbarComponent,
    ManageInventoryComponent,
    ManageInventoryCategoryComponent,
    AllocatedResourceComponent,
  ],
  imports: [
    CommonModule,
    ManagerRoutingModule,
    HttpClientModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
  ],
  exports: [
    InventoryComponent,
    DashboardComponent,
    InventoryCategoryComponent,
    VehicleComponent,
    VehicleTypeComponent,
    OrderComponent,
    DriverComponent,
    ResourceAssignmentComponent,
    // ManagerComponent,
    // SideNavComponent,
    // NavbarComponent,
  ],
})
export class ManagerModule {}
