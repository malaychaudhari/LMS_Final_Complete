import { Component, Input, OnInit, inject } from '@angular/core';
import { SideNavLink } from '../../Models/NavLink.model';
import { CommonService } from '../../Services/Common/common.service';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
import { AuthService } from '../../Services/Common/auth.service';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrl: './side-nav.component.scss',
})
export class SideNavComponent implements OnInit {
  commonService = inject(CommonService);

  userDropdownIcon: boolean = false;
  toggleUserDropdownIcon(): void {
    this.userDropdownIcon = !this.userDropdownIcon;
  }

  userName: string;
  constructor(private authService: AuthService, private router: Router) {}
  @Input()
  navigationLinks: SideNavLink[] = [];

  ngOnInit(): void {
    if (this.authService.isLoggedIn) {
      this.userName = this.authService.getUserName();
    }
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }
}
