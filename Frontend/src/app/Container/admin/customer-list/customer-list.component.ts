import { LiveAnnouncer } from '@angular/cdk/a11y';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { User } from '../../../Models/User.model';
import { UserService } from '../../../Services/Common/user.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.scss',
})
export class CustomerListComponent implements OnInit {
  displayedColumns: string[] = [
    'id',
    'name',
    'email',
    'phone',
    'date',
    'customerStatus',
    'action',
  ];
  dataSource: MatTableDataSource<User>;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('closebutton') closebutton;

  customers: User[] = [] as User[];
  customerIdToDelete: number = 0;
  constructor(
    private userService: UserService,
    private liveAnnouncer: LiveAnnouncer,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getCustomers();
  }

  getCustomers() {
    this.userService.getUsersByRole(4).subscribe({
      next: (res) => {
        console.log(res);

        this.customers = res.data as User[];

        this.customers = this.customers?.filter(
          (customer) => customer.isApproved != 0
        );

        this.dataSource = new MatTableDataSource(this.customers);
        this.dataSource.sort = this.sort;
      },
      error: (error) => {
        console.log(error);

        this.toastr.error('Error', error?.error?.error);
      },
    });
  }

  ConfirmDelete(userId: number) {
    this.customerIdToDelete = userId;
  }
  resetDeleteId() {
    this.customerIdToDelete = 0;
  }

  deleteCustomer() {
    this.userService.deleteUser(this.customerIdToDelete).subscribe({
      next: (res) => {
        this.toastr.success('Success', res.data);
        this.getCustomers();
        this.closebutton.nativeElement.click();
      },
      error: (error) => {
        console.log(error);

        this.toastr.error('Error', 'Error while deleting user');
      },
    });
  }

  unblockCustomer(id: number) {
    this.userService.unblockUser(id).subscribe({
      next: (res) => {
        this.toastr.success('Success', res.data);
        this.getCustomers();
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
