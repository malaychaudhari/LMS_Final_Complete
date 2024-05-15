import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
} from '@angular/common/http';
import { finalize, Observable } from 'rxjs';
import { AuthService } from '../Common/auth.service';
import { LoaderService } from '../Common/loader.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService,private loaderService: LoaderService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Check if the user is logged in
    if (this.authService.isLoggedIn()) {
    this.loaderService.showLoader();

      const authToken = this.authService.getToken();

      // add the token to the headers for requests requiring authorization
      if (this.isRequestRequiringAuthorization(request)) {
        const authRequest = request.clone({
          setHeaders: {
            Authorization: `Bearer ${authToken}`,
          },
        });
        return next.handle(authRequest).pipe(
          finalize(() => this.loaderService.hideLoader())
        );;
      }
    }
    console.log('Interceptor');

    // For requests that do not require authorization pass the original request
    return next.handle(request);
  }

  private isRequestRequiringAuthorization(request: HttpRequest<any>): boolean {
    // Example criteria: Exclude requests to specific URLs
    const excludedUrls = ['/pro'];
    return !excludedUrls.some((url) => request.url.includes(url));
  }
}
