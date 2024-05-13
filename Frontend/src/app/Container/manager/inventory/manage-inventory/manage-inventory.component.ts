import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  SimpleChanges,
  ViewChild,
  inject,
} from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { InventoryCategoryService } from '../../../../Services/Manager/inventory-category.service';
import { InventoryCategory } from '../../../../Models/InventoryCategory.model';
import { InventoryService } from '../../../../Services/Manager/inventory.service';
import { Inventory } from '../../../../Models/Inventory.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-manage-inventory',
  templateUrl: './manage-inventory.component.html',
  styleUrl: './manage-inventory.component.scss',
})
export class ManageInventoryComponent implements OnInit {
  InventoryForm: FormGroup;

  inventoryCategories: InventoryCategory[] = [];
  file: File = null;

  inventory: Inventory = {} as Inventory;
  formSubitted: boolean = false;
  @ViewChild('closebutton') closebutton;

  @Output() itemAdded: EventEmitter<any> = new EventEmitter<any>();

  @Input() selectedInventory: Inventory;
  @Input() isEditMode: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private toastr: ToastrService
  ) { }

  setValue(value: Inventory) {
    if (value) {
      this.InventoryForm.get('name').setValue(value.name);
      this.InventoryForm.get('category').setValue(value.categoryId);
      this.InventoryForm.get('description').setValue(value.description);
      this.InventoryForm.get('quantity').setValue(value.stock);
      this.InventoryForm.get('price').setValue(value.price);
    }
  }
  inventoryCategoryService: InventoryCategoryService = inject(
    InventoryCategoryService
  );
  inventoryService: InventoryService = inject(InventoryService);

  ngOnInit(): void {
    this.getInventoryCategories();
    this.InventoryForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.pattern(/^(?=.*[a-zA-Z0-9])[a-zA-Z0-9\s]{1,100}$/),
      ]),
      category: new FormControl('default', [Validators.required]),
      description: new FormControl(null, [
        Validators.required,
        Validators.pattern(/^.{5,500}$/),
      ]),
      quantity: new FormControl(null, [
        Validators.required,
        Validators.pattern(/^(?:[1-9]|\d{2,3}|[1-4]\d{3}|5000)$/),
      ]),
      price: new FormControl('', [
        Validators.required,
        Validators.pattern(/^\d+(\.\d{1,2})?$/),
      ]),
      image: new FormControl('', [Validators.required]),
    });
  }
  onEditModeChange() {
    this.isEditMode = true;
  }
  onChange() {
    this.formSubitted = false;
  }
  onFormSubmit() {
    this.formSubitted = true;
    // this.selectedInventory = null;
    if (this.InventoryForm.valid) {
      this.closebutton.nativeElement.click();

      if (this.isEditMode) {
        this.onUpdateInventoryClick();
      } else {
        this.inventory.name = this.InventoryForm.value.name;
        this.inventory.categoryId = this.InventoryForm.value.category;
        this.inventory.description = this.InventoryForm.value.description;
        this.uploadImage();
        this.inventory.stock = this.InventoryForm.value.quantity;
        this.inventory.price = this.InventoryForm.value.price;
        this.inventory.warehouseId = 1;
      }
    }
  }

  uploadImage() {
    this.inventoryService
      .fileUpload(this.file)
      .then((res) => {
        this.inventory.image = res;
        this.postInventory(this.inventory);
      })
      .catch((err) => { });
  }

  onFileSelected(event: any) {
    this.file = event.target.files[0];
  }

  getInventoryCategories() {
    this.inventoryCategoryService.getInventoryCategories().subscribe({
      next: (response) => {
        this.inventoryCategories = response.data as InventoryCategory[];
      },
    });
  }
  onUpdateInventoryClick() {
    console.log(this.selectedInventory);

    this.inventory.id = this.selectedInventory.id;
    this.inventory.name = this.InventoryForm.value.name;
    this.inventory.categoryId = this.InventoryForm.value.category;
    this.inventory.description = this.InventoryForm.value.description;
    this.inventory.image = this.selectedInventory.image;
    this.inventory.stock = this.InventoryForm.value.quantity;
    this.inventory.price = this.InventoryForm.value.price;
    this.inventory.IsActive = true;
    this.inventory.createdAt = this.selectedInventory.createdAt;
    this.inventory.updatedAt = new Date();

    this.inventory.warehouseId = 1;

    this.updateInventory(this.inventory);
  }
  updateInventory(inventory: Inventory) {
    console.log(inventory);

    this.inventoryService.updateInventory(inventory).subscribe({
      next: (response) => {
        this.toastr.success('Success', 'Inventory Updated Successfully');
        this.itemAdded.emit(response);
        this.InventoryForm.reset();
        this.formSubitted = false;
      },
      error: (err) => {
        console.log(err);
        this.toastr.error('Error', 'Failed to Update Inventory');
      },
    });
  }

  postInventory(inventory: Inventory) {
    this.inventoryService.postInventory(inventory).subscribe({
      next: (response) => {
        this.toastr.success('Success', 'Inventory added Successfully');
        this.itemAdded.emit(response);
        this.InventoryForm.reset();
        this.formSubitted = false;
      },
      error: (err) => {
        console.log(err);
        this.toastr.error('Error', 'Failed to add Inventory');
      },
    });
  }
}
