import { Component, OnChanges, OnInit } from '@angular/core';
import { Inventory } from '../../../Models/Inventory.model';
import { ActivatedRoute, Router } from '@angular/router';
import { InventoryService } from '../../../Services/Manager/inventory.service';
import Swal from 'sweetalert2';
import { User } from '../../../Models/User.model';
import { UserService } from '../../../Services/Common/user.service';
import { AuthService } from '../../../Services/Common/auth.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss'
})
export class ProductDetailsComponent implements OnInit {
  user:User= {} as User;
  inventory: Inventory;
  quantity: number = 1;
  productId: number;

  constructor(
    private inventoryService: InventoryService, private route: ActivatedRoute, private router: Router,
    private userService:UserService,private authService:AuthService
  ) { }

  ngOnInit(): void {
    this.getInventory();
    this.userService.getUserById(this.authService.getUserId()).subscribe({
      next: (res) => {
        this.user = res.data;
      },
      error: (err) => {
        console.error(err);
      }
    })
  }

  getInventory(): void {
    this.route.params.subscribe(params => {
      this.productId = +params['id']; // Convert id to number
      this.inventoryService.getInventoryDetails(this.productId).subscribe({
        next: (res) => {
          this.inventory = res.data as Inventory;  // Assign the data to the inventory variable     
        },
        error: (error) => {
          console.error('Error fetching inventory details:', error);
        }
      });
    });
  }

  buyNow(id: any): void {
    if (this.user.addressId=== null ) {
      Swal.fire({
        text: " Please add your address before proceeding with the purchase",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Add Address"
      }).then((result) => {
        if (result.isConfirmed) {
        this.router.navigate(['customer/profile']);
        }
      });
    } else {      
      if (this.quantity > 0 && this.quantity <= this.inventory.stock) {
        this.router.navigate(['customer/checkout', id], { state: { inventory: this.inventory, quantity: this.quantity } });
      } else {
        console.error('Invalid quantity!');
      }
    }

  }

  increment() {
    if (this.quantity < this.inventory.stock) {
      this.quantity++;
    }
  }

  decrement() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }
}