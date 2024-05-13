import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, of } from "rxjs";
import { ApiResponse } from "../../Models/ApiResponse.model";

@Injectable({ providedIn: 'root' })
export class CountryService {
  apiUrl = 'http://localhost:5181/api/country';

  constructor(private http: HttpClient) {}
  
  getCountries():Observable<ApiResponse>{
    return this.http.get<ApiResponse>(this.apiUrl)
  }
}
