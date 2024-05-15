import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerRoutingModule } from './customer-routing.module';
import { CustomerComponent } from './customer.component';
import { HttpClientModule } from '@angular/common/http';
import { NzTableModule } from 'ng-zorro-antd/table';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatIconModule } from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { ProductsComponent } from './products/products.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { HomeComponent } from './home/home.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';
import { FormsModule } from '@angular/forms'; 
import { ProfileComponent } from './profile/profile.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { OrderDetailComponent } from './order-detail/order-detail.component';


@NgModule({
  declarations: [
    // CustomerComponent,
    // HeaderComponent,
    // FooterComponent,
    ProductsComponent,
    ProductDetailsComponent,
    PageNotFoundComponent,
    CheckoutComponent,
    HomeComponent,
    MyOrdersComponent,
    ProfileComponent,
    OrderDetailComponent
  ],
  imports: [
    CommonModule,
    CustomerRoutingModule,
    HttpClientModule,
    MatTableModule,
    MatSortModule,
    MatIconModule,
    MatTooltipModule,
    ReactiveFormsModule,
    FormsModule 
  ],
  exports: [
    // CustomerComponent,
    ProductsComponent,
    ProductDetailsComponent,
    PageNotFoundComponent,
    CheckoutComponent,
  ],
})
export class CustomerModule {}
