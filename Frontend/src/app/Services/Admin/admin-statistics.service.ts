import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../Models/ApiResponse.model';

@Injectable({
  providedIn: 'root'
})
export class AdminStatisticsService {

  constructor(private http:HttpClient) { }
  private apiUrl = 'http://localhost:5181/api/statistics/admin';

  getAdminStatistics():Observable<ApiResponse>{
    return this.http.get<ApiResponse>(this.apiUrl)
  }
}
