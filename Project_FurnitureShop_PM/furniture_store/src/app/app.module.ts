import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';

import { HomeComponent } from './pages/Home/home.component';

import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbPaginationModule, NgbAlertModule } from '@ng-bootstrap/ng-bootstrap';
import '@angular/localize/init';
import { ParentComponent } from './layout/parent/parent.component';
import { ChildComponent } from './layout/child/child.component';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { AppRoutingModule } from './app-routing.module';
import { ListProductComponent } from './pages/Home/ProductView/list-product/list-product.component';
import { ListCategoryComponent } from './pages/Home/CategoryView/list-category/list-category.component';



@NgModule({
  declarations: [
    AppComponent,
    ParentComponent,
    ChildComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    ListCategoryComponent,
    ListProductComponent,
    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    NgbModule,
    NgbPaginationModule,
    NgbAlertModule,
    AppRoutingModule
   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { } 