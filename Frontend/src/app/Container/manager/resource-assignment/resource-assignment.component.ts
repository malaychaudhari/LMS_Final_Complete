import { Component, SimpleChanges, ViewChild } from '@angular/core';
import { Order } from '../../../Models/Order.model';
import { User } from '../../../Models/User.model';
import { Vehicle } from '../../../Models/Vehicle.model';
import { OrderService } from '../../../Services/Common/order.service';
import { UserService } from '../../../Services/Common/user.service';
import { VehicleService } from '../../../Services/Manager/vehicle.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ResourceAllocation } from '../../../Models/ResourceAllocation.model';
import { ResourceAllocationService } from '../../../Services/Manager/resource-allocation.service';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatSort, Sort } from '@angular/material/sort';
import { AuthService } from '../../../Services/Common/auth.service';

@Component({
  selector: 'app-resource-assignment',
  templateUrl: './resource-assignment.component.html',
  styleUrl: './resource-assignment.component.scss',
})
export class ResourceAssignmentComponent {
  orders: Order[] = [];
  pendingOrders: Order[] = [];
  drivers: User[] = [];
  availabelDrivers: User[] = [];
  vehicles: Vehicle[] = [];
  availableVehicles: Vehicle[] = [];
  assignedResources: ResourceAllocation[] = [];
  displayedColumns: string[] = [
    'id',
    'userName',
    'orderDate',
    'inventoryId',
    'statusId',
    'selectDriver',
    'selectVehicle',
    'action',
  ];
  dataSource: MatTableDataSource<Order>;
  resourceAllocation: ResourceAllocation = {} as ResourceAllocation;

  ResorceAllocationForm: FormGroup;
  constructor(
    private orderService: OrderService,
    private userService: UserService,
    private vehicleService: VehicleService,
    private resourceAllocationService: ResourceAllocationService,
    private toastr: ToastrService,
    private authService: AuthService,
    private liveAnnouncer: LiveAnnouncer
  ) {}

  // selectedDriver: string = null;
  // selectedVehicle: string = null;

  @ViewChild(MatSort) sort: MatSort;

  ngOnInit() {
    this.getOrders();
    this.getDrivers();
    this.getVehicles();
    this.ResorceAllocationForm = new FormGroup({
      selectedDriver: new FormControl('default', [Validators.required]),
      selectedVehicle: new FormControl('default', [Validators.required]),
    });
  }

  ngOnChanges(changes: SimpleChanges): void {}

  getOrders(): void {
    this.orderService.getOrders().subscribe({
      next: (response) => {
        this.orders = response.data as Order[];
        this.pendingOrders = this.orders.filter(
          (order) => order.statusId === 1
        );
        this.dataSource = new MatTableDataSource(this.pendingOrders);
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getDrivers(): void {
    this.userService.getUsersByRole(3).subscribe({
      next: (response) => {
        this.drivers = response.data as User[];
        this.availabelDrivers = this.drivers.filter(
          (driver) => driver.isAvailable == true
        );
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  getVehicles(): void {
    this.vehicleService.getVehicles().subscribe({
      next: (response) => {
        this.vehicles = response.data as Vehicle[];
        this.availableVehicles = this.vehicles.filter(
          (vehicle) => vehicle.isAvailable === true
        );
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
  onAssignOrderClick(orderId: number) {
    if (this.ResorceAllocationForm.valid) {
      console.log(this.ResorceAllocationForm.value);

      this.resourceAllocation.orderId = orderId;
      this.resourceAllocation.driverId =
        +this.ResorceAllocationForm?.value?.selectedDriver;
      this.resourceAllocation.vehicleId =
        +this.ResorceAllocationForm?.value?.selectedVehicle;
      this.resourceAllocation.managerId = +this.authService.getUserId();
      this.resourceAllocation.assignedDate = new Date();
      this.resourceAllocation.assignmentStatusId = 1;

      this.resourceAssignment(this.resourceAllocation);
    }
  }

  resourceAssignment(resourceAllocationModel: ResourceAllocation) {
    console.log(resourceAllocationModel);

    this.resourceAllocationService
      .resourceAssignment(resourceAllocationModel)
      .subscribe({
        next: (res) => {
          this.toastr.success('Success', 'Resource Allocated successfully ');
          this.ResorceAllocationForm.reset();
          this.getOrders();
          this.getDrivers();
          this.getVehicles();
        },
        error: (error) => {
          this.toastr.error('Error', 'Failed to allocate resource');
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
