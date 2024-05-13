import { Component, inject } from '@angular/core';
import { SideNavLink } from '../../Models/NavLink.model';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CommonService } from '../../Services/Common/common.service';

@Component({
  selector: 'app-manager',
  templateUrl: './manager.component.html',
  styleUrl: './manager.component.scss',
})
export class ManagerComponent {
  navigationLinks: SideNavLink[] = [
    {
      path: 'dashboard',
      label: 'Dashboard',
      icon: '../../../assets/Images/Manager/dashboard_icon_2.png',
    },
    {
      path: 'inventory',
      label: 'Inventories',
      icon: '../../../assets/Images/Manager/inventory_1.png',
    },
    {
      path: 'inventory-category',
      label: 'Inventory Categories',
      icon: '../../../assets/Images/Manager/product_category_2.png',
    },
    {
      path: 'vehicle',
      label: 'Vehicles',
      icon: '../../../assets/Images/Manager/vehicle_2.png',
    },
    {
      path: 'vehicle-type',
      label: 'Vehicle Types',
      icon: '../../../assets/Images/Manager/vehicle_type_2.png',
    },
    {
      path: 'available-driver',
      label: 'Drivers',
      icon: '../../../assets/Images/Manager/driver_2.png',
    },
    {
      path: 'order',
      label: 'Orders',
      icon: '../../../assets/Images/Manager/order_1.png',
    },
    {
      path: 'resource-assignment',
      label: 'Resource Allocation',
      icon: '../../../assets/Images/Manager/resource_assignment_2.png',
    },
    {
      path: 'allocated-resource',
      label: 'Allocated Resource',
      icon: '../../../assets/Images/Manager/allocated_resource_icon_1.png',
    },
  ];

  commonService = inject(CommonService);
}
