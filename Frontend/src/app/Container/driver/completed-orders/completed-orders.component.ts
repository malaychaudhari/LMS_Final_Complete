import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { CompletedService } from '../../../Services/Driver/completed-orders.service';
import { ResourceAllocation } from '../../../Models/ResourceAllocation.model';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../Services/Common/auth.service';

@Component({
  selector: 'app-completed-orders',
  templateUrl: './completed-orders.component.html',
  styleUrl: './completed-orders.component.scss'
})
export class CompletedOrdersComponent implements  OnInit {
  displayedColumns: string[] = ['id', 'cname', 'cphone', 'caddress', 'vehicle', 'date', 'status'];
  resources: ResourceAllocation[] = [];
  dataSource: MatTableDataSource<ResourceAllocation>;
  selectedOrder: ResourceAllocation = null;

  constructor(private compltedOrderService: CompletedService, private _liveAnnouncer: LiveAnnouncer, private authService:AuthService) { }

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.getCompletedOrders();
  }

 
  getCompletedOrders(): void {
    this.compltedOrderService.getCompletedOrders(this.authService.getUserId()).subscribe({
      next: (response) => {
        console.log(response);
        this.resources = response.data as ResourceAllocation[];
        this.dataSource = new MatTableDataSource(this.resources);
        this.dataSource.sort = this.sort;
      },
      error: (error) => {
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
