import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../Common/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    // Check if the user is logged in
    if (this.authService.isLoggedIn()) {
      const authToken = this.authService.getToken();

      // add the token to the headers for requests requiring authorization
      if (this.isRequestRequiringAuthorization(request)) {
        const authRequest = request.clone({
          setHeaders: {
            Authorization: `Bearer ${authToken}`,
          },
        });
        return next.handle(authRequest);
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
