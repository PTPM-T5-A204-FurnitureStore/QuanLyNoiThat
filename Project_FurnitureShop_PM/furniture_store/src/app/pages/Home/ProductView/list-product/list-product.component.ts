import { Component, OnInit } from '@angular/core';
import { APISanPhamServiceService } from 'src/app/services/API_SanPham_Service/api-san-pham-service.service';

@Component({
  selector: 'app-list-product',
  templateUrl: './list-product.component.html',
  styleUrls: ['./list-product.component.css']
})
export class ListProductComponent implements OnInit{

  listProduct:any=[];
  roundedNum!: number;
  loopArray: any=[]; 

  constructor(
    
    private apiSanPhamService: APISanPhamServiceService,
    ) {
    
   }

  ngOnInit() {
   
    this.getListProduct();
  }

  getListProduct()
  {
    console.log("call API");
    this.apiSanPhamService.getData().subscribe((dataproduct) => {
      this.listProduct = dataproduct;
      var num = this.listProduct.length / 5;
      this.roundedNum = Math.round(num);
      this.loopArray = Array(this.roundedNum).fill(0).map((x, i) => i);
    });
  }
}
