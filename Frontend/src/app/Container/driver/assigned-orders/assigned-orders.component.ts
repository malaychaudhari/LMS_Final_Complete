import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { OrdersService } from '../../../Services/Driver/orders.service';
import { ResourceAllocation } from '../../../Models/ResourceAllocation.model';
import { AuthService } from '../../../Services/Common/auth.service';
import { StatusService } from '../../../Services/Driver/status.service';

import { OrderStatus } from '../../../Models/OrderStatus.model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-assigned-orders',
  templateUrl: './assigned-orders.component.html',
  styleUrl: './assigned-orders.component.scss',
})
export class AssignedOrdersComponent implements  OnInit {
  displayedColumns: string[] = [
    'id',
    'cname',
    'cphone',
    'caddress',
    'vehicle',
    'orderStatus',
    'action',
  ];
  resources: ResourceAllocation[] = [];
  dataSource: MatTableDataSource<ResourceAllocation>;
  selectedOrder: ResourceAllocation = null;
  userId:number=this.authService.getUserId();
  orderStatus: OrderStatus = {} as OrderStatus;

  constructor(
    private orderService: OrdersService,
    private _liveAnnouncer: LiveAnnouncer,
    private authService: AuthService,
    private statusService: StatusService,
    private toastr:ToastrService,
    private router:Router
  ) {}

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.getAssignedOrder();
  }

  // ngAfterViewInit() {
  //   this.dataSource.sort = this.sort;
  // }

  getAssignedOrder(): void {
    this.orderService
      .getAssignedOrders(this.userId)
      .subscribe({
        next: (response) => {
          console.log(response);
          this.resources = response.data as ResourceAllocation[];
          this.dataSource = new MatTableDataSource(this.resources);
          this.dataSource.sort = this.sort;
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  updateStatus(orderId:number): void {

    this.orderStatus.orderId=orderId;
    this.orderStatus.orderStatusId =3;  

    this.statusService.updateStatus(this.orderStatus).subscribe({
      next: (response) => {
        console.log(response);
        this.toastr.success('Success', response.data);
        this.router.navigate(['driver/view-completed-orders'])

      },
       error: (err) => {
        console.log(err);
        this.toastr.error('Error', err?.error?.error);
        
       }
    })
  }
}
