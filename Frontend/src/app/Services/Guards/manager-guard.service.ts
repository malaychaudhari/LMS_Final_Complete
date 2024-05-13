import { Injectable } from '@angular/core';
import {
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AuthService } from '../Common/auth.service';

@Injectable({
  providedIn: 'root',
})
export class ManagerGuard {
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
      if (userRole === 'Manager') {
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
      return true;
    }
  }
}
