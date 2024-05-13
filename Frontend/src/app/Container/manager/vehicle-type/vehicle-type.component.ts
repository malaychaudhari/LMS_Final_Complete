import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { Vehicle } from '../../../Models/Vehicle.model';
import { VehicleService } from '../../../Services/Manager/vehicle.service';
import { VehicleType } from '../../../Models/VehicleType.model';

@Component({
  selector: 'app-vehicle-type',
  templateUrl: './vehicle-type.component.html',
  styleUrl: './vehicle-type.component.scss',
})
export class VehicleTypeComponent {
  displayedColumns: string[] = ['id', 'type', 'isActive', 'date'];
  vehicleTypes: VehicleType[] = [];
  dataSource: MatTableDataSource<VehicleType>;

  constructor(
    private vehicleService: VehicleService,
    private liveAnnouncer: LiveAnnouncer,
    private toastr: ToastrService
  ) {}

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.getVehicleType();
  }

  getVehicleType(): void {
    this.vehicleService.getVehicleTypes().subscribe({
      next: (response) => {
        console.log(response);

        this.vehicleTypes = response.data as VehicleType[];
        this.dataSource = new MatTableDataSource(this.vehicleTypes);
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
