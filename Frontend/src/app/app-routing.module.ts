import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './Container/admin/admin.component';
import { AuthComponent } from './Container/auth/auth.component';
import { NotFound404Component } from './Components/not-found-404/not-found-404.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'auth/login' },
  // { path: 'admin', component: AdminComponent },
  // { path: 'driver', component: DriverComponent },
  // { path: 'customer', component: CustomerComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
