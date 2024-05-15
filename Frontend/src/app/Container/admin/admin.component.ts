import { Component, inject } from '@angular/core';
import { CommonService } from '../../Services/Common/common.service';
import { SideNavLink } from '../../Models/NavLink.model';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss',
})
export class AdminComponent {
  commonService = inject(CommonService);

  navigationLinks: SideNavLink[] = [
    {
      path: 'dashboard',
      label: 'Dashboard',
      icon: '../../../assets/Images/Manager/dashboard_icon_2.png',
    },
    {
      path: 'signup-request',
      label: 'SignUp Requests',
      icon: '../../../assets/Images/Admin/add-user.png',
    },
    {
      path: 'manager',
      label: 'Managers',
      icon: '../../../assets/Images/Admin/manager.png',
    },
    {
      path: 'driver',
      label: 'Drivers',
      icon: '../../../assets/Images/Admin/driver.png',
    },
    {
      path: 'customer',
      label: 'Customers',
      icon: '../../../assets/Images/Admin/people.png',
    },
  ];
}
