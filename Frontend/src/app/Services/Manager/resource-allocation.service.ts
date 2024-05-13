import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResourceAllocation } from '../../Models/ResourceAllocation.model';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../Models/ApiResponse.model';

@Injectable({
  providedIn: 'root',
})
export class ResourceAllocationService {
  apiUrl = 'http://localhost:5181/api/resource-mapping';

  constructor(private http: HttpClient) {}

  resourceAssignment(
    resourceAllocationModel: ResourceAllocation
  ): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.apiUrl, resourceAllocationModel);
  }
  getAllocatedResources(): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(this.apiUrl);
  }
}
