import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../Models/ApiResponse.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  apiUrl = 'http://localhost:5181/api';

  constructor(private http: HttpClient) {}

  getVehicles(): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.apiUrl}/vehicle`);
  }

  getVehicleTypes(): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(`${this.apiUrl}/vehicle-type`);
  }
}
