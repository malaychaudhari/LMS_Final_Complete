import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { AdminStatisticsService } from '../../../Services/Admin/admin-statistics.service';
import { AdminStatistics } from '../../../Models/AdminStatistics.model';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Order } from '../../../Models/Order.model';
import { OrderService } from '../../../Services/Common/order.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

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
  recentOrders: Order[] = [];

  dataSource: MatTableDataSource<Order>;

  constructor( private orderService: OrderService,
    private liveAnnouncer: LiveAnnouncer,) { }

  adminStatisticsService:AdminStatisticsService= inject(AdminStatisticsService);

  statisticsData:AdminStatistics={} as AdminStatistics;
  @ViewChild(MatSort) sort: MatSort;
  isLoading:boolean=false

  ngOnInit() {
    this.getStatistics();
    this.getOrders();
  }

  getStatistics(){
    this.adminStatisticsService.getAdminStatistics().subscribe({
      next:(res)=>{
        this.statisticsData= res.data as AdminStatistics
      },
      error:(err)=>{
        console.log(err);
        
      }
    })

  }
  getOrders(): void {
    this.orderService.getOrders().subscribe({
      next: (response) => {
        this.isLoading= true;
        this.orders = response.data as Order[];
        console.log(JSON.stringify(this.orders));
        
        this.orders.sort((a, b) => new Date(b.orderDate).getTime() - new Date(a.orderDate).getTime());
        this.recentOrders = this.orders.slice(0, 5);
        this.dataSource = new MatTableDataSource(this.recentOrders);
        this.dataSource.sort = this.sort;
        this.isLoading= false;

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
