import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../../../Services/Common/order.service';
import { Order } from '../../../Models/Order.model';
import { ToastrService } from 'ngx-toastr';
import { User } from '../../../Models/User.model';
import { Address } from '../../../Models/Address.model';
import { UserService } from '../../../Services/Common/user.service';
import { AuthService } from '../../../Services/Common/auth.service';
import { AddressService } from '../../../Services/Customer/address.service';
import { Inventory } from '../../../Models/Inventory.model';
import { InventoryService } from '../../../Services/Manager/inventory.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrl: './order-detail.component.scss'
})
export class OrderDetailComponent {
  orderId: number;
  userId: number;
  order: Order[] = [] as Order[];
  user: User = {} as User;
  orderedProduct: Inventory[] = [] as Inventory[];
  userAddress: Address = {} as Address

  
  constructor(private route: ActivatedRoute,
    private orderService: OrderService,
    private userService: UserService,
    private authService: AuthService,
    private addressService: AddressService,
    private inventoryService: InventoryService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.orderId = this.route.snapshot.params['id'];
    this.userId = this.authService.getUserId();
    this.loadOrderDetails();





  }

  loadOrderDetails() {
    this.orderService.getOrderById(this.orderId).subscribe({
      next: (res) => {
        this.order = res?.data;

        this.order.map((detail)=>
        {
          this.loadOrderedProduct(detail.inventoryId)

        })

        this.loadUserDetails()
      },
      error: (err) => {
        this.toastr.error(err?.error?.error)
      }
    });
  }

  loadUserDetails() {
    this.userService.getUserById(this.userId).subscribe({
      next: (res) => {
        this.user = res?.data;
        this.loadUserAddress(this.user.addressId);
      },
      error: (err) => {
        this.toastr.error(err?.error?.error)
      }
    });
  }

  loadUserAddress(addressId: number) {
    this.addressService.getAddressById(addressId).subscribe({
      next: (res) => {
        this.userAddress = res?.data;
        console.log(this.order);
        
      },
      error: (err) => {
        this.toastr.error(err?.error?.error)
      }
    });
  }

  loadOrderedProduct(inventoryId: number) {
    this.inventoryService.getInventoryDetails(inventoryId).subscribe({
      next: (res) => {
        this.orderedProduct.push(res?.data)

      },
      error: (err) => {
        this.toastr.error(err?.error?.error)
      }
    });
  }
}


