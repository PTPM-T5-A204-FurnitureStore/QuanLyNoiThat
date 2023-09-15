import { Component, OnInit } from '@angular/core';
import { APILoaiHangServiceService } from 'src/app/services/API_LoaiHang_Service/api-loai-hang-service.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  data: any = [];
  roundedNum: number=0;
  loopArray: any=[]; 
  constructor(private apiService: APILoaiHangServiceService) {
    
   }

  ngOnInit() {
    this.getData();
  }


  getData() {
    console.log("call API");
    this.apiService.getData().subscribe((data) => {
      this.data = data;
      var num = this.data.length / 5;
      this.roundedNum = Math.round(num);
      this.loopArray = Array(this.roundedNum).fill(0).map((x, i) => i);
    });
  }

}
