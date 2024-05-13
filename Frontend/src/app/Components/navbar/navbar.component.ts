import { Component, Inject, Input, SimpleChanges, inject } from '@angular/core';
import { SideNavLink } from '../../Models/NavLink.model';
import { CommonService } from '../../Services/Common/common.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent {
  commonService = inject(CommonService);
  toggleHamburger() {
    this.commonService.sideBar = !this.commonService.sideBar;
  }
}
