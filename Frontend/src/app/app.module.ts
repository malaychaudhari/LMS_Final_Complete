import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AdminComponent } from './Container/admin/admin.component';

import { AuthComponent } from './Container/auth/auth.component';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { NavbarComponent } from './Components/navbar/navbar.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatTableModule } from '@angular/material/table';
import { AdminModule } from './Container/admin/admin.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';
import { AuthModule } from './Container/auth/auth.module';
import { SideNavComponent } from './Components/side-nav/side-nav.component';
import { ManagerComponent } from './Container/manager/manager.component';
import { ManagerModule } from './Container/manager/manager.module';
import { NotFound404Component } from './Components/not-found-404/not-found-404.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { AuthInterceptor } from './Services/Interceptor/auth.interceptor';
import { CustomerComponent } from './Container/customer/customer.component';
import { DriverComponent } from './Container/driver/driver.component';
import { CustomerModule } from './Container/customer/customer.module';
import { DriverModule } from './Container/driver/driver.module';
import { HeaderComponent } from './Container/customer/header/header.component';
import { FooterComponent } from './Container/customer/footer/footer.component';

@NgModule({
  declarations: [
    AppComponent,
    AdminComponent,
    DriverComponent,
    CustomerComponent,
    ManagerComponent,
    AuthComponent,
    NavbarComponent,
    SideNavComponent,
    NotFound404Component,
    HeaderComponent,
    FooterComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    RouterModule,
    CommonModule,
    ManagerModule,
    CustomerModule,
    DriverModule,
    AdminModule,
    AuthModule,
    MatTableModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [
    provideAnimationsAsync(),
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
