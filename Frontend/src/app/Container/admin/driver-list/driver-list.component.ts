import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { User } from '../../../Models/User.model';
import { UserService } from '../../../Services/Common/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-driver-list',
  templateUrl: './driver-list.component.html',
  styleUrl: './driver-list.component.scss',
})
export class DriverListComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'name',
    'email',
    'phone',
    'licenseNumber',
    'date',
    'approvalStatus',
    'driverStatus',
    'action',
  ];
  dataSource: MatTableDataSource<User>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('closebutton') closebutton;

  drivers: User[] = [] as User[];
  driverIdToDelete: number = 0;
  constructor(
    private userService: UserService,
    private liveAnnouncer: LiveAnnouncer,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getDrivers();
  }

  getDrivers() {
    this.userService.getUsersByRole(3).subscribe({
      next: (res) => {
        this.drivers = res.data as User[];

        this.drivers = this.drivers?.filter((driver) => driver.isApproved != 0);

        this.dataSource = new MatTableDataSource(this.drivers);
        this.dataSource.sort = this.sort;
      },
      error: (error) => {
        console.log(error);

        this.toastr.error('Error', error?.error?.error);
      },
    });
  }

  ConfirmDelete(userId: number) {
    this.driverIdToDelete = userId;
  }
  resetDeleteId() {
    this.driverIdToDelete = 0;
  }

  deleteDriver() {
    this.userService.deleteUser(this.driverIdToDelete).subscribe({
      next: (res) => {
        this.toastr.success('Success', res.data);
        this.getDrivers();
        this.closebutton.nativeElement.click();
      },
      error: (error) => {
        console.log(error);

        this.toastr.error('Error', 'Error while deleting user');
      },
    });
  }

  unblockDriver(id: number) {
    this.userService.unblockUser(id).subscribe({
      next: (res) => {
        this.toastr.success('Success', res.data);
        this.getDrivers();
      },
      error: (error) => {
        console.log(error);

        this.toastr.error('Error', 'Error while unblocking user');
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
