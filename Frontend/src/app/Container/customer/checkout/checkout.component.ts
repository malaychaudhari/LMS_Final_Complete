import { Component, OnChanges, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Inventory } from '../../../Models/Inventory.model';
import { Order } from '../../../Models/Order.model';
import { AddOrderService } from '../../../Services/Customer/add-order.service';
import { UserService } from '../../../Services/Common/user.service';
import { AddressService } from '../../../Services/Customer/address.service';
import { User } from '../../../Models/User.model';
import { Address } from '../../../Models/Address.model';
import { AuthService } from '../../../Services/Common/auth.service';
import { ChangeDetectorRef } from '@angular/core';
import { OrderService } from '../../../Services/Common/order.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutComponent implements OnInit {
  orderDetails: Inventory;
  user: User = {} as User;
  customerId: number;
  addressId: number;
  userAddress: Address;
  orderQuantity: number;
  order: Order = {} as Order;

  constructor(private route: ActivatedRoute, private authService: AuthService, private orderService: OrderService, private userService: UserService, private addressService: AddressService, private router: Router, private cdr: ChangeDetectorRef, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.orderDetails = history.state.inventory;
    this.orderQuantity = history.state.quantity;
    this.order.subTotal = this.orderQuantity * this.orderDetails.price;
    console.log(this.orderQuantity);
    
    // console.log(this.orderDetails); // Output the received orderDetails data
    this.customerId = this.authService.getUserId();
    this.getUser();    
  }

  // Get User By id
  getUser() {
    this.userService.getUserById(this.customerId).subscribe({
      next: (res) => {
        // console.log(res);
        this.user = res.data as User;
        this.addressId = +this.user.addressId;

        // fetching the address of the user
        this.getAddress(this.addressId);

        this.cdr.detectChanges(); // Force change detection
      },
      error: (err) => {
        console.error("Error while fetching the user details", err);
      }
    }
    )
  }

  // Get Address of a User 
  getAddress(addressId: number) {
    this.addressService.getAddressById(addressId).subscribe({
      next: (response) => {
        // console.log(response);
        this.userAddress = response.data as Address;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }


  // Place Order
  placeOrder(): void {
    
    // putting all things into order
    this.order.customerId = this.customerId;
    this.order.inventoryId = this.orderDetails.id;
    this.order.quantity = this.orderQuantity;
    // this.order.subTotal = this.orderQuantity * this.orderDetails.price;
    this.order.originId = this.orderDetails.warehouseId;
    this.order.destinationId = this.addressId;
    this.order.orderStatusId = 1;
    
    console.log(this.order);
    this.orderService.postOrder(this.order).subscribe({
      next: (response) => {
        this.toastr.success("Order Placed Successfully");
        // console.log(response);
        this.router.navigate(['customer/my-orders']);
      },
      error: (error) => {
        this.toastr.error("Failed to place order");
        console.log(error);
      }
    })
  }
}
