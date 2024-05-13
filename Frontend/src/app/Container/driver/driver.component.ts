import { Component, inject } from '@angular/core';
import { SideNavLink } from '../../Models/NavLink.model';
import { CommonService } from '../../Services/Common/common.service';


@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrl: './driver.component.scss'
})
export class DriverComponent {
  commonService = inject(CommonService);
  
  navigationLinks:SideNavLink[] = [
    
    {path:'view-assigned-orders', label: 'Assigned Orders', icon : '../../../assets/Images/Manager/resource_assignment_2.png'},
    {path:'view-completed-orders', label: 'Completed Orders', icon : '../../../assets/Images/Manager/resource_assignment_2.png'}
  ];

}
