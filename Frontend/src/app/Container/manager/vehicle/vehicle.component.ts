import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Inventory } from '../../../Models/Inventory.model';
import { InventoryService } from '../../../Services/Manager/inventory.service';
import { ToastrService } from 'ngx-toastr';
import { Vehicle } from '../../../Models/Vehicle.model';
import { VehicleService } from '../../../Services/Manager/vehicle.service';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrl: './vehicle.component.scss',
})
export class VehicleComponent {
  displayedColumns: string[] = [
    'id',
    'vehicleNumber',
    'vehicleType',
    'wareHouseId',
    'isAvailable',
  ];
  vehicles: Vehicle[] = [];
  dataSource: MatTableDataSource<Vehicle>;

  constructor(
    private vehicleService: VehicleService,
    private liveAnnouncer: LiveAnnouncer,
    private toastr: ToastrService
  ) {}

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.getVehicles();
  }

  getVehicles(): void {
    this.vehicleService.getVehicles().subscribe({
      next: (response) => {
        console.log(response);

        this.vehicles = response.data as Vehicle[];
        this.dataSource = new MatTableDataSource(this.vehicles);
        this.dataSource.sort = this.sort;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
  announceSortChange(sortState: Sort): void {
    if (sortState.direction) {
      this.liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this.liveAnnouncer.announce('Sorting cleared');
    }
  }
}
