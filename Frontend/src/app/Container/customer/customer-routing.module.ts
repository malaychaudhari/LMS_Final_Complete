import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CustomerComponent } from './customer.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { ProductsComponent } from './products/products.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HomeComponent } from './home/home.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { MyOrdersComponent } from './my-orders/my-orders.component';
import { CustomerGuard } from '../../Services/Guards/customer-guard.service';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  {
    path: 'customer',
    component: CustomerComponent,
    canActivate: [CustomerGuard],

    children: [
      { path: '', redirectTo: 'products', pathMatch: 'full' },
      // { path: '', component: HomeComponent },
      {
        path: 'home',
        component: ProductsComponent,
        canActivate: [CustomerGuard],
      },
      {
        path: 'header',
        component: HeaderComponent,
        canActivate: [CustomerGuard],
      },
      {
        path: 'footer',
        component: FooterComponent,
        canActivate: [CustomerGuard],
      },
      {
        path: 'checkout/:id',
        component: CheckoutComponent,
        canActivate: [CustomerGuard],
      },
      {
        path: 'products',
        component: ProductsComponent,
        canActivate: [CustomerGuard],
      },
      {
        path: 'product-details/:id',
        component: ProductDetailsComponent,
        canActivate: [CustomerGuard],
      },
      {
        path: 'my-orders',
        component: MyOrdersComponent,
        canActivate: [CustomerGuard],
      },
      {path: 'profile', component: ProfileComponent, canActivate: [CustomerGuard]},
      {
        path: '**',
        component: PageNotFoundComponent,
        canActivate: [CustomerGuard],
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomerRoutingModule {}
