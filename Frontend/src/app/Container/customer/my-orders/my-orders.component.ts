import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { OrderService } from '../../../Services/Customer/order.service';
import { Order } from '../../../Models/Order.model';
import { AuthService } from '../../../Services/Common/auth.service';

@Component({
  selector: 'app-my-orders',
  templateUrl: './my-orders.component.html',
  styleUrl: './my-orders.component.scss'
})

export class MyOrdersComponent implements AfterViewInit, OnInit {
  displayedColumns: string[] = ['pName', 'quantity', 'total', 'oDate', 'oStatus'];
  orders: Order[] = [];
  dataSource: MatTableDataSource<Order>;

  constructor(private _liveAnnouncer: LiveAnnouncer, private orderService: OrderService, private authService: AuthService) { }

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit(): void {
    this.getMyOrders();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  getMyOrders(): void {
    this.orderService.getOrderDetails(this.authService.getUserId()).subscribe({
      next: (response) => {
        this.orders = response.data as Order[];
        console.log(this.orders);
        
        this.dataSource = new MatTableDataSource(this.orders);
        this.dataSource.sort = this.sort;
      }, error: (error) => {
        console.log(error);
      }
    })
  }


  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }
}
