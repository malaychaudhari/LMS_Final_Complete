import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../Models/ApiResponse.model';
import { Order } from '../../Models/Order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private http:HttpClient) { }

  private apiUrl = 'http://localhost:5181/api/order';

  getOrders():Observable<ApiResponse>{
    return this.http.get<ApiResponse>(this.apiUrl)
  }

  postOrder(orderData: Order): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(`${this.apiUrl}`, orderData);
  }
}
