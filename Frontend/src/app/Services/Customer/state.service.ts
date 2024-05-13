import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, of } from "rxjs";
import { ApiResponse } from "../../Models/ApiResponse.model";

@Injectable({ providedIn: 'root' })
export class StateService {
  apiUrl = 'http://localhost:5181/api/state';

  constructor(private http: HttpClient) {}
  
  getStates():Observable<ApiResponse>{
    return this.http.get<ApiResponse>(this.apiUrl)
  }
}