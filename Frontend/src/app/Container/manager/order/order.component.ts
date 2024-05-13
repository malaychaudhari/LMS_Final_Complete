import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Inventory } from '../../../Models/Inventory.model';
import { InventoryService } from '../../../Services/Manager/inventory.service';
import { ToastrService } from 'ngx-toastr';
import { Vehicle } from '../../../Models/Vehicle.model';
import { VehicleService } from '../../../Services/Manager/vehicle.service';
import { User } from '../../../Models/User.model';
import { Order } from '../../../Models/Order.model';
import { UserService } from '../../../Services/Common/user.service';
import { OrderService } from '../../../Services/Common/order.service';
@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.scss',
})
export class OrderComponent {
  displayedColumns: string[] = [
    'id',
    'userName',
    'orderDate',
    'totalAmount',
    'inventoryName',
    'quantity',
    'statusId',
  ];
  orders: Order[] = [];
  dataSource: MatTableDataSource<Order>;

  constructor(
    private orderService: OrderService,
    private liveAnnouncer: LiveAnnouncer,
    private toastr: ToastrService
  ) {}

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.getOrders();
  }

  getOrders(): void {
    this.orderService.getOrders().subscribe({
      next: (response) => {
        this.orders = response.data as Order[];
        console.log(JSON.stringify(this.orders));
        
        
        this.dataSource = new MatTableDataSource(this.orders);
        this.dataSource.sort = this.sort;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
  announceSortChange(sortState: Sort): void {
    console.log('sort');

    if (sortState.direction) {
      console.log('sort start');
      this.liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
      console.log('sort start');
    } else {
      this.liveAnnouncer.announce('Sorting cleared');
    }
  }
}
