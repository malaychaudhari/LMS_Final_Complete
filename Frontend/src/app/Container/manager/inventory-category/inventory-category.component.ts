import { LiveAnnouncer } from '@angular/cdk/a11y';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Inventory } from '../../../Models/Inventory.model';
import { InventoryService } from '../../../Services/Manager/inventory.service';
import { ToastrService } from 'ngx-toastr';
import { InventoryCategoryService } from '../../../Services/Manager/inventory-category.service';
import { InventoryCategory } from '../../../Models/InventoryCategory.model';
import Swal from 'sweetalert2';
import { ManageInventoryComponent } from '../inventory/manage-inventory/manage-inventory.component';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-inventory-category',
  templateUrl: './inventory-category.component.html',
  styleUrl: './inventory-category.component.scss',
})
export class InventoryCategoryComponent {
  displayedColumns: string[] = ['id', 'name', 'createdAt', 'action'];
  inventoryCategories: InventoryCategory[] = [];
  dataSource: MatTableDataSource<InventoryCategory>;
  selectedInventoryCategory: InventoryCategory = null;
  isEditMode: boolean = false;

  constructor(
    private inventoryCategoryService: InventoryCategoryService,
    private liveAnnouncer: LiveAnnouncer,
    private toastr: ToastrService
  ) {}

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('inventoryModel') inventoryModel: ManageInventoryComponent;

  ngOnInit() {
    this.getInventoryCategory();
  }
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }
  getInventoryCategory(): void {
    this.inventoryCategoryService.getInventoryCategories().subscribe({
      next: (response) => {
        console.log(response);
        this.inventoryCategories = response.data as InventoryCategory[];
        this.dataSource = new MatTableDataSource(this.inventoryCategories);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
  onAddInventoryCategoryClick() {
    this.isEditMode = false;
    this.selectedInventoryCategory = null;
  }
  onItemAdded() {
    this.getInventoryCategory();
  }
  onEdit(id: number) {
    this.isEditMode = true;
    this.selectedInventoryCategory = this.inventoryCategories.find(
      (invent) => invent.id === id
    );
  }
  onDelete(id: number) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!',
    }).then((result) => {
      if (result.isConfirmed) {
        this.deleteInventoryCategory(id);
        Swal.fire({
          title: 'Deleted!',
          text: 'Your file has been deleted.',
          icon: 'success',
        });
      }
    });
  }
  deleteInventoryCategory(id: number) {
    this.inventoryCategoryService.deleteInventoryCategory(id).subscribe({
      next: (response) => {
        this.toastr.success(
          'Success',
          'Inventory Category deleted successfully'
        );
        this.getInventoryCategory();
      },
      error: (error) => {
        console.log(error);
        this.toastr.error('Error', 'Error while deleting inventory Category');
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
