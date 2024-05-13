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
import { UserService } from '../../../Services/Common/user.service';

@Component({
  selector: 'app-driver',
  templateUrl: './driver.component.html',
  styleUrl: './driver.component.scss'
})
export class DriverComponent {
  displayedColumns: string[] = ['id', 'name','email', 'licenseNumber', 'wareHouseId', 'isAvailable'];
  drivers: User[] = [];
  dataSource: MatTableDataSource<User>;

  constructor(
    private userService: UserService,
    private liveAnnouncer: LiveAnnouncer,
    private toastr: ToastrService
  ) {}

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.getDrivers();
  }

  getDrivers(): void {
    this.userService.getUsersByRole(3).subscribe({
      next: (response) => {
        this.drivers = response.data as User[];
        this.dataSource = new MatTableDataSource(this.drivers);
        this.dataSource.sort = this.sort;
      },
      error: (error) => {
        console.log(error);
      }
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












