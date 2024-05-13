import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiResponse } from '../../Models/ApiResponse.model';
import { Observable } from 'rxjs';
import { InventoryCategory } from '../../Models/InventoryCategory.model';

@Injectable({
  providedIn: 'root'
})
export class InventoryCategoryService {
  apiUrl = 'http://localhost:5181/api/inventory-category';

  constructor(private http: HttpClient) {}
  
  getInventoryCategories():Observable<ApiResponse>{
    return this.http.get<ApiResponse>(this.apiUrl)
  }

  postInventoryCategory(postData:InventoryCategory){
    return this.http.post<ApiResponse>(`${this.apiUrl}`,postData)
  }

  updateInventoryCategory(updateData:InventoryCategory){
    return this.http.put<ApiResponse>(`${this.apiUrl}`, updateData)
  }
  
  deleteInventoryCategory(inventoryId:number){
    return this.http.delete<ApiResponse>(`${this.apiUrl}/${inventoryId}`)
  }
}
