import { HttpClient, HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../../Models/User.model';
import { Observable, tap } from 'rxjs';
import { ApiResponse } from '../../Models/ApiResponse.model';
import { Login } from '../../Models/Login.mpdel';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'http://localhost:5181/api/auth';
  decodedToken: any;
  constructor(private http: HttpClient) {}

  signUp(user: User): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.apiUrl}/signup`, user);
  }

  login(login: Login): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.apiUrl}/login`, login).pipe(
      tap((response) => {
        this.storeToken(response.data);
        console.log(response);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('token');
  }

  private storeToken(token: string): void {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  decodeToken() {
    const token = this.getToken();
    if (token) {
      const decodedToken = jwtDecode(token);
      this.decodedToken = decodedToken;
      return decodedToken;
    }
    return null;
  }

  getUserId() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.Id : null;
  }

  getUserName() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.unique_name : null;
  }

  getUserRole() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.role : null;
  }

  getEmailId() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.email : null;
  }

  getExpiryTime() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.exp : null;
  }

  isTokenExpired(): boolean {
    const expiryTime: number = this.getExpiryTime();
    if (expiryTime) {
      return 1000 * expiryTime - new Date().getTime() < 5000;
    } else {
      return false;
    }
  }
}
