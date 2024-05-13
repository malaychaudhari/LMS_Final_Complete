import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../../Models/ApiResponse.model';
import { Observable } from 'rxjs';
import { OrderStatus } from '../../Models/OrderStatus.model';

@Injectable({
    providedIn: 'root'
})
export class StatusService {
    apiUrl = 'http://localhost:5181/api/order/update-status';
  
    constructor(private http: HttpClient) {}
    
    updateStatus(orderStatus: OrderStatus):Observable<ApiResponse>{
      return this.http.put<ApiResponse>(this.apiUrl, orderStatus)
    }
  }
  