import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, of } from "rxjs";
import { ApiResponse } from "../../Models/ApiResponse.model";

@Injectable({ providedIn: 'root' })
export class OrderService {
  apiUrl = 'http://localhost:5181/api/order/all-orders';

  constructor(private http: HttpClient) {}
  
  getOrders():Observable<ApiResponse>{
    return this.http.get<ApiResponse>(this.apiUrl)
  }

  getOrderDetails(id: any):Observable<ApiResponse>{
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<ApiResponse>(url)
  }
}
