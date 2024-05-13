import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Inventory } from '../../../Models/Inventory.model';
import { InventoryService } from '../../../Services/Manager/inventory.service';
import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';
import { ResourceAllocation } from '../../../Models/ResourceAllocation.model';
import { ResourceAllocationService } from '../../../Services/Manager/resource-allocation.service';

@Component({
  selector: 'app-allocated-resource',
  templateUrl: './allocated-resource.component.html',
  styleUrl: './allocated-resource.component.scss',
})
export class AllocatedResourceComponent {
  displayedColumns: string[] = [
    'id',
    'orderId',
    'driverId',
    'vehicleId',
    'assignedDate',
  ];
  dataSource: MatTableDataSource<ResourceAllocation>;
  allocatedResources: ResourceAllocation[] = [];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private resourceAllocationService: ResourceAllocationService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.getAllocatedResource();
  }

  getAllocatedResource(): void {
    this.resourceAllocationService.getAllocatedResources().subscribe({
      next: (response) => {
        this.allocatedResources = response.data as ResourceAllocation[];
        this.dataSource = new MatTableDataSource(this.allocatedResources);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
