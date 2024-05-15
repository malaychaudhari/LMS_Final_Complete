import { Component, Injectable } from '@angular/core';
import {
  CanActivate,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../Common/auth.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class AdminGuard {
  constructor(
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {
    const userRole = this.authService.getUserRole();

    if (!this.authService.isTokenExpired()) {
      if (userRole === 'Admin') {
        return true; // Allow access for admin users
      } else {
        // Redirect unauthorized users to login page
        this.router.navigate(['/auth/login']);
        this.toastr.error(
          'You do not have permission to access this page',
          'Access Denied'
        );
        return false;
      }
    } else {
      this.authService.logout();
      this.router.navigate(['/auth/login']);
      this.toastr.error('Please Log In Again', 'Session Expired');
      return false;
    }
  }
}
