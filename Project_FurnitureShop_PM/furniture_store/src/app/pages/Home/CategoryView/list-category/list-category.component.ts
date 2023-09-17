import { Component, OnInit } from '@angular/core';
import { APILoaiHangServiceService } from 'src/app/services/API_LoaiHang_Service/api-loai-hang-service.service';

@Component({
  selector: 'app-list-category',
  templateUrl: './list-category.component.html',
  styleUrls: ['./list-category.component.css']
})
export class ListCategoryComponent implements OnInit  {
  listCategory: any = [];
  roundedNum!: number;
  loopArray: any=[]; 

  constructor(
    private apiCategoryService: APILoaiHangServiceService,
    ) {
    
   }


   ngOnInit() {
    this.getListCategory();
  }

  getListCategory() {
    console.log("call API in share");
    this.apiCategoryService.getData().subscribe((data) => {
      this.listCategory = data;
      var num = this.listCategory.length / 5;
      this.roundedNum = Math.round(num);
      this.loopArray = Array(this.roundedNum).fill(0).map((x, i) => i);
    });
  }
 
 
}
