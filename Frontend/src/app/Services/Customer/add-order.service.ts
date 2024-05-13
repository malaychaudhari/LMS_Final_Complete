import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../../Models/Order.model';

@Injectable({
  providedIn: 'root'
})
export class AddOrderService {
  private apiUrl = 'http://example.com/api/orders'; // Replace with your API endpoint

  constructor(private http: HttpClient) {}

  addOrder(orderDetails: Order): Observable<any> {
    return this.http.post<any>(this.apiUrl, orderDetails);
  }
}
