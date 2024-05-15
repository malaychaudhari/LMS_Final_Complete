import { Component, Inject, Input, OnInit, SimpleChanges, inject } from '@angular/core';
import { SideNavLink } from '../../Models/NavLink.model';
import { CommonService } from '../../Services/Common/common.service';
import { AuthService } from '../../Services/Common/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss',
})
export class NavbarComponent implements OnInit{
  

  userRole:string=''

  commonService = inject(CommonService);
  authService= inject(AuthService);

  ngOnInit(): void {
    this.userRole= this.authService.getUserRole()
  }
  
  toggleHamburger() {
    this.commonService.sideBar = !this.commonService.sideBar;
  }
}
