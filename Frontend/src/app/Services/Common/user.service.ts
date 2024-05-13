import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../Models/ApiResponse.model';
import { SignupRequest } from '../../Models/SignupRequest.mode';
import { AssignWarehouse } from '../../Models/AssignWarehouse.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = 'http://localhost:5181/api/user';

  constructor(private http: HttpClient) {}

  getPendingUsersByRole(id: number): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.apiUrl}/pendingusers/role/${id}`);
  }
  getUsersByRole(id: number): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.apiUrl}/role/${id}`);
  }

  getUserById(id: number): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.apiUrl}/${id}`);
  }

  signUpRequest(signupRequest: SignupRequest): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(
      `${this.apiUrl}/signup-request`,
      signupRequest
    );
  }

  assignWarehouse(assignWarehouse: AssignWarehouse): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(
      `${this.apiUrl}/assign-warehouse`,
      assignWarehouse
    );
  }

  deleteUser(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(`${this.apiUrl}/deactivate/${id}`);
  }

  unblockUser(id: number): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(`${this.apiUrl}/activate/${id}`, {});
  }
}
