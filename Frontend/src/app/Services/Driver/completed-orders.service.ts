import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../../Models/ApiResponse.model';
import { Observable } from 'rxjs';
import { ResourceAllocation } from '../../Models/ResourceAllocation.model';

@Injectable({
    providedIn: 'root'
})
export class CompletedService {
    apiUrl = 'http://localhost:5181/api/order/completed-orders';
  
    constructor(private http: HttpClient) {}
    
    getCompletedOrders(id:number):Observable<ApiResponse>{
      return this.http.get<ApiResponse>(`${this.apiUrl}/${id}`)
    }

  }
  