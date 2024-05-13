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
  selector: 'app-manage-inventory-category',
  templateUrl: './manage-inventory-category.component.html',
  styleUrl: './manage-inventory-category.component.scss',
})
export class ManageInventoryCategoryComponent {
  InventoryForm: FormGroup;
  inventoryCategory: InventoryCategory = {} as InventoryCategory;
  formSubitted: boolean = false;

  @ViewChild('closebutton') closebutton;
  @Output() itemAdded: EventEmitter<any> = new EventEmitter<any>();
  @Input() selectedInventoryCategory: InventoryCategory;
  @Input() isEditMode: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private toastr: ToastrService
  ) {}

  setValue(value: InventoryCategory) {
    if (value) {
      this.InventoryForm.get('name').setValue(value.name);
      this.InventoryForm.get('createdAt').setValue(value.createdAt);
    }
  }
  inventoryCategoryService: InventoryCategoryService = inject(
    InventoryCategoryService
  );

  ngOnInit(): void {
    this.InventoryForm = new FormGroup({
      name: new FormControl('', [
        Validators.required,
        Validators.pattern(/^(?=.*[a-zA-Z0-9])[a-zA-Z0-9\s]{1,100}$/),
      ]),
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
    this.selectedInventoryCategory = null;
    if (this.InventoryForm.valid) {
      this.closebutton.nativeElement.click();

      if (this.isEditMode) {
        this.onUpdateInventoryClick();
      } else {
        this.inventoryCategory.name = this.InventoryForm.value.name;
        this.postInventoryCategory(this.inventoryCategory);
      }
    }
  }

  onUpdateInventoryClick() {
    console.log("Hello"+this.selectedInventoryCategory);
    this.inventoryCategory.id = this.selectedInventoryCategory.id;
    this.inventoryCategory.name = this.selectedInventoryCategory.name;
    this.inventoryCategory.createdAt = this.selectedInventoryCategory.createdAt;
    this.inventoryCategory.IsActive = true;
    this.inventoryCategory.updatedAt = new Date();
    this.updateInventoryCategory(this.inventoryCategory);
  }
  updateInventoryCategory(inventoryCategory: InventoryCategory) {
    this.inventoryCategoryService
      .updateInventoryCategory(this.inventoryCategory)
      .subscribe({
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
  postInventoryCategory(inventoryCategory: InventoryCategory) {
    this.inventoryCategoryService
      .postInventoryCategory(inventoryCategory)
      .subscribe({
        next: (response) => {
          this.toastr.success('Success', 'Inventory added Successfully');
          this.itemAdded.emit(response);
          this.InventoryForm.reset();
          this.formSubitted = false;
        },
        error: (err) => {
          console.log(err);
          this.toastr.error('Error', err.error.error);
        },
      });
  }
}
