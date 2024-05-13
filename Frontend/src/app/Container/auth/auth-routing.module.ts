import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './auth.component';
import { LoginComponent } from './login/login.component';
import { RegistrationComponent } from './registration/registration.component';
import { NotFound404Component } from '../../Components/not-found-404/not-found-404.component';
import { LoginGuard } from '../../Services/Guards/login-guard.service';

const routes: Routes = [
  {
    path: 'auth',
    component: AuthComponent,
    canActivate:[LoginGuard],
    children: [
      // { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: RegistrationComponent },
      { path: '**', component: NotFound404Component },

    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AuthRoutingModule {}
