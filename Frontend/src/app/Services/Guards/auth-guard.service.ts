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
export class AuthGuardService {
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
    if (this.authService.isLoggedIn()) {
      if (this.authService.isTokenExpired) {
        this.authService.logout();
        this.router.navigate(['/auth/login']);
        this.toastr.error('Please Log In Again', 'Session Expired');

        return false;
      } else {
        return true;
      }
    } else {
      this.router.navigate(['/auth/login']);
      this.toastr.error(' Please login to Continue', 'Authentication Required');
      return false;
    }
  }
}
