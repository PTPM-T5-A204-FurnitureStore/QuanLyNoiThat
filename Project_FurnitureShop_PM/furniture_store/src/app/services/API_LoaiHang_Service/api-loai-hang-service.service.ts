import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; // Import HttpClient thay vì HttpClientModule

@Injectable({
  providedIn: 'root'
})
export class APILoaiHangServiceService {
  private apiUrl = 'https://localhost:7053/api/LoaiHang'; 

  constructor(private http: HttpClient) { // Sửa HttpClientModule thành HttpClient

  }

  getData() {
    console.log("call API Category in service")
    return this.http.get(`${this.apiUrl}/GetLoaiHang`); 
   
  }
}