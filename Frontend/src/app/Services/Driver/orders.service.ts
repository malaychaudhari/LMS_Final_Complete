import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../../Models/ApiResponse.model';
import { Observable } from 'rxjs';
import { ResourceAllocation } from '../../Models/ResourceAllocation.model';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  apiUrl = 'http://localhost:5181/api/order/assigned-orders';

  constructor(private http: HttpClient) {}

  getAssignedOrders(id: number): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(this.apiUrl + '/' + id);
  }

  // postInventoryCategory(postData:Orders){
  //   return this.http.post<ApiResponse>(`${this.apiUrl}`,postData)
  // }

  // updateInventoryCategory(updateData:InventoryCategory){
  //   return this.http.put<ApiResponse>(`${this.apiUrl}`, updateData)
  // }

  // deleteInventoryCategory(inventoryId:number){
  //   return this.http.delete<ApiResponse>(`${this.apiUrl}/${inventoryId}`)
  // }
}
