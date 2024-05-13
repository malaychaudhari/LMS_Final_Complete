import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, of } from "rxjs";
import { ApiResponse } from "../../Models/ApiResponse.model";

@Injectable({ providedIn: 'root' })
export class CityService {
  apiUrl = 'http://localhost:5181/api/city';

  constructor(private http: HttpClient) {}
  
  getCities():Observable<ApiResponse>{
    return this.http.get<ApiResponse>(this.apiUrl)
  }
}
