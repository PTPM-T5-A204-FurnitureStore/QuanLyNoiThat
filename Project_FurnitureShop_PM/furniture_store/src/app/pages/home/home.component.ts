import { Component, OnInit } from '@angular/core';
import { APILoaiHangServiceService } from 'src/app/services/API_LoaiHang_Service/api-loai-hang-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  data: any = [];
  indicators: any = [];

  constructor(private apiService: APILoaiHangServiceService) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    console.log("call API");
    this.apiService.getData().subscribe((data) => {
      this.data = data;
      this.generateIndicators();
    });
  }

  generateIndicators() {
    const totalCount = this.data.length;
    const itemsPerSlide = 5;
    const slideCount = Math.ceil(totalCount / itemsPerSlide);

    this.indicators = Array.from({ length: slideCount }, (_, i) => i);
  }

  getDataSlice(slideIndex: number) {
    const itemsPerSlide = 5;
    const startIndex = slideIndex * itemsPerSlide;
    return this.data.slice(startIndex, startIndex + itemsPerSlide);
  }
}
