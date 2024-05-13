import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DriverRoutingModule } from './driver-routing.module';
import { AssignedOrdersComponent } from './assigned-orders/assigned-orders.component';
import { CompletedOrdersComponent } from './completed-orders/completed-orders.component';
import {MatTableModule} from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import {MatButtonModule} from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    AssignedOrdersComponent,
    CompletedOrdersComponent
  ],
  imports: [
    CommonModule,
    DriverRoutingModule,
    // RouterModule,
    MatTableModule,
    MatSortModule,
    MatButtonModule,
    MatIconModule,
  ],
  exports: [
    AssignedOrdersComponent,
    CompletedOrdersComponent
  ]
})
export class DriverModule { }
