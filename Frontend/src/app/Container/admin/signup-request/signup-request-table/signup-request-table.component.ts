import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, Input, OnChanges, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { UserService } from '../../../../Services/Common/user.service.js';
import { Observable } from 'rxjs';
import { ApiResponse } from '../../../../Models/ApiResponse.model.js';
import { SignupRequest } from '../../../../Models/SignupRequest.mode.js';
import { ToastrService } from 'ngx-toastr';
import { AssignWarehouse } from '../../../../Models/AssignWarehouse.model.js';
import { User } from '../../../../Models/User.model.js';

@Component({
  selector: 'app-signup-request-table',
  templateUrl: './signup-request-table.component.html',
  styleUrl: './signup-request-table.component.scss',
})
export class SignupRequestTableComponent implements OnChanges {
  displayedColumns: string[] = [
    'id',
    'name',
    'email',
    'phone',
    'date',
    'licenseNumber',
    'approvalStatus',
    'action',
  ];
  dataSource: MatTableDataSource<User>;

  @Input() selectedTab: string = '';
  users: User[] = [];
  signupReqModel: SignupRequest = {} as SignupRequest;
  assignWarehouseModel: AssignWarehouse = {} as AssignWarehouse;

  @ViewChild(MatSort) sort: MatSort;

  constructor(
    private userService: UserService,
    private liveAnnouncer: LiveAnnouncer,
    private toastr: ToastrService
  ) {}

  ngOnChanges(): void {
    if (this.selectedTab) {
      this.selectedTab == 'Driver'
        ? this.displayedColumns.splice(5, 0, 'licenseNumber')
        : this.displayedColumns.splice(5, 1);
      this.fetchDataBasedOnTab();
    }
  }

  //fetch user based on role
  fetchDataBasedOnTab() {
    this.fetchPendingUsers().subscribe({
      next: (res) => {
        this.users = res.data as User[];
        this.dataSource = new MatTableDataSource(this.users);
        this.dataSource.sort = this.sort;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  // calling user service's getPendingUsersByRole() method
  fetchPendingUsers(): Observable<ApiResponse> {
    if (this.selectedTab === 'Manager') {
      return this.userService.getPendingUsersByRole(2);
    } else if (this.selectedTab === 'Driver') {
      return this.userService.getPendingUsersByRole(3);
    }
    return new Observable<ApiResponse>();
  }

  //---------------------------------------------------------------
  // approve user's signup request
  approveUser(userId: number) {
    this.signupReqModel.userId = userId;
    this.signupReqModel.status = 1;
    console.log(this.signupReqModel);

    this.signUpRequest(this.signupReqModel);
  }

  // reject user's signup request
  rejectUser(userId: number) {
    this.signupReqModel.userId = userId;
    this.signupReqModel.status = -1;

    this.signUpRequest(this.signupReqModel);
  }

  // calling user service's signUpRequest() method
  signUpRequest(signupRequest: SignupRequest) {
    console.log(signupRequest);

    this.userService.signUpRequest(signupRequest).subscribe({
      next: (res) => {
        if (signupRequest.status === 1) {
          this.toastr.success(
            'Success',
            `${this.selectedTab}'s Status approved`
          );
          if (this.selectedTab == 'Manager') {
            this.assignManagerToWarehouse(signupRequest.userId);
          }
        }

        if (signupRequest.status === -1) {
          this.toastr.success(
            'Success',
            `${this.selectedTab}'s Status rejected`
          );
        }
        this.fetchDataBasedOnTab();
      },
      error: (error) => {
        this.toastr.error('Error', 'Failed to Update status');
      },
    });
  }

  assignManagerToWarehouse(managerId: number) {
    this.assignWarehouseModel.managerId = managerId;
    this.assignWarehouseModel.warehouseId = 1;
    this.userService.assignWarehouse(this.assignWarehouseModel).subscribe({
      next: (res) => {
        console.log(res.data);
        this.toastr.success('Success', res.data);
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
