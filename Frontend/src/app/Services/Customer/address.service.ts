import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, of } from "rxjs";
import { ApiResponse } from "../../Models/ApiResponse.model";
import { Address } from "../../Models/Address.model";

@Injectable({ providedIn: 'root' })
export class AddressService {
  apiUrl = 'http://localhost:5181/api/address';

  constructor(private http: HttpClient) {}
  
  getAddresses():Observable<ApiResponse>{
    return this.http.get<ApiResponse>(this.apiUrl)
  }

  getAddressById(id: any):Observable<ApiResponse>{
    const url = `${this.apiUrl}/${id}`;
    return this.http.get<ApiResponse>(url)
  }

  postAddress(address: Address):Observable<ApiResponse>{
    return this.http.post<ApiResponse>(this.apiUrl, address);
  }
}
