import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class APISanPhamServiceService {
  private apiUrl = 'https://localhost:7053/api/SanPham'; 
  constructor(private http: HttpClient) { }

  getData() {
    console.log("call API Product in service")
    return this.http.get(`${this.apiUrl}/GetSanPham`); 
  }
}
