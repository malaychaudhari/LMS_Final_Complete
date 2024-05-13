import { Component, OnInit, ViewChild } from '@angular/core';
import { User } from '../../../Models/User.model';
import { UserService } from '../../../Services/Common/user.service';
import { ToastrService } from 'ngx-toastr';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { WarehouseService } from '../../../Services/Admin/warehouse.service';
import { Warehouse } from '../../../Models/Warehouse.model';
@Component({
  selector: 'app-manager-list',
  templateUrl: './manager-list.component.html',
  styleUrl: './manager-list.component.scss',
})
export class ManagerListComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'name',
    'email',
    'phone',
    'date',
    'approvalStatus',
    'managerStatus',
    'action',
  ];
  dataSource: MatTableDataSource<User>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('closebutton') closebutton;

  managers: User[] = [] as User[];
  managerIdToDelete: number = 0;
  constructor(
    private userService: UserService,
    private warehouseService: WarehouseService,
    private liveAnnouncer: LiveAnnouncer,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getManagers();
  }

  getManagers() {
    this.userService.getUsersByRole(2).subscribe({
      next: (res) => {
        console.log(res);

        this.managers = res.data as User[];

        this.managers = this.managers?.filter(
          (manager) => manager.isApproved != 0
        );

        this.dataSource = new MatTableDataSource(this.managers);
        this.dataSource.sort = this.sort;
      },
      error: (error) => {
        console.log(error);

        this.toastr.error('Error', error?.error?.error);
      },
    });
  }

  ConfirmDelete(userId: number) {
    this.managerIdToDelete = userId;
  }
  resetDeleteId() {
    this.managerIdToDelete = 0;
  }

  deleteManager() {
    this.userService.deleteUser(this.managerIdToDelete).subscribe({
      next: (res) => {
        this.toastr.success('Success', res.data);
        this.getManagers();
        this.closebutton.nativeElement.click();
      },
      error: (error) => {
        console.log(error);

        this.toastr.error('Error', 'Error while deleting user');
      },
    });
  }

  unblockManager(id: number) {
    this.userService.unblockUser(id).subscribe({
      next: (res) => {
        this.toastr.success('Success', res.data);
        this.getManagers();
      },
      error: (error) => {
        console.log(error);

        this.toastr.error('Error', 'Error while unblocking user');
      },
    });
  }

  getWarehouseById(warehouseId: number) {
    console.log(warehouseId);

    this.warehouseService.getWarehouseById(warehouseId).subscribe({
      next: (res) => {
        console.log(res);

        return res.data.name as Warehouse;
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
