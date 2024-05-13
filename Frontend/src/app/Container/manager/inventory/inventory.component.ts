import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Inventory } from '../../../Models/Inventory.model';
import { InventoryService } from '../../../Services/Manager/inventory.service';
import { ManageInventoryComponent } from './manage-inventory/manage-inventory.component';
import Swal from 'sweetalert2';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.scss'],
})
export class InventoryComponent implements AfterViewInit {
  displayedColumns: string[] = [
    'id',
    'name',
    'category',
    'stock',
    'price',
    'action',
  ];
  dataSource: MatTableDataSource<Inventory>;
  inventories: Inventory[] = [];
  selectedInventory: Inventory = null;
  isEditMode: boolean = false;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('inventoryModel') inventoryModel: ManageInventoryComponent;

  constructor(
    private inventoryService: InventoryService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.getInventories();
  }
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  getInventories(): void {
    this.inventoryService.getInventories().subscribe({
      next: (response) => {
        this.inventories = response.data as Inventory[];
        this.dataSource = new MatTableDataSource(this.inventories);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },
      error: (error) => {
        console.log(error);
      },
    });
  }
  onAddInventoryClick() {
    this.isEditMode = false;
    this.selectedInventory = null;
  }
  onItemAdded() {
    this.getInventories();
  }
  onEdit(id: number) {
    this.isEditMode = true;
    this.selectedInventory = this.inventories.find(
      (invent) => invent.id === id
    );
    this.inventoryModel.setValue(this.selectedInventory);
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
        this.deleteInventories(id);
        Swal.fire({
          title: 'Deleted!',
          text: 'Your file has been deleted.',
          icon: 'success',
        });
      }
    });
  }

  deleteInventories(id: number) {
    this.inventoryService.deleteInventory(id).subscribe({
      next: (response) => {
        this.toastr.success('Success', 'Inventory deleted successfully');
        this.getInventories();
      },
      error: (error) => {
        console.log(error);
        this.toastr.error('Error', 'Error while deleting inventory');
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
