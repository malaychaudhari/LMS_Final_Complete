import { Component, OnInit } from '@angular/core';
import { Inventory } from '../../../Models/Inventory.model';
import { InventoryService } from '../../../Services/Manager/inventory.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent implements OnInit {
  inventories: Inventory[] = [];
  productId: number;

  constructor(
    private inventoryService: InventoryService, private router: Router
  ) {}

  ngOnInit(): void {    
    this.getInventories();
  }

  getInventories(): void {
    this.inventoryService.getInventories().subscribe({
      next: (response) => {
        this.inventories = response.data as Inventory[];
        console.log(response);
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  viewProductDetails(id: any) : void {
    this.router.navigate(['customer/product-details', id]);
  }
}
