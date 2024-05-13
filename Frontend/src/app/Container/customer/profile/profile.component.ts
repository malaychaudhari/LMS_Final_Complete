import { Component, OnInit } from '@angular/core';
import { User } from '../../../Models/User.model';
import { AuthService } from '../../../Services/Common/auth.service';
import { UserService } from '../../../Services/Common/user.service';
import { ToastrService } from 'ngx-toastr';
import { AddressService } from '../../../Services/Customer/address.service';
import { Address } from '../../../Models/Address.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { City } from '../../../Models/City.model';
import { State } from '../../../Models/State.model';
import { Country } from '../../../Models/Country.model';
import { CityService } from '../../../Services/Customer/city.service';
import { StateService } from '../../../Services/Customer/state.service';
import { CountryService } from '../../../Services/Customer/country.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})

export class ProfileComponent implements OnInit {
  user: User = {} as User;
  userId: number;
  address: Address = {} as Address;
  showAddressFields: boolean = false;
  AddressForm: FormGroup;
  cities: City[] = [] as City[];
  states: State[] = [] as State[];
  countries: Country[] = [] as Country[];
  filteredCities: City[] = [] as City[];

  constructor(private authService: AuthService, private userService: UserService, private toastr:ToastrService, private addressService: AddressService, private cityService: CityService, private stateService: StateService, private countryService: CountryService) {}

  ngOnInit(): void {
    this.userId = this.authService.getUserId();
    this.getUser();
    this.getCities();
    this.getStates();
    this.getCountries();

    this.AddressForm = new FormGroup({
      address: new FormControl('', Validators.required),
      pincode: new FormControl('', [
        Validators.required,
        Validators.pattern(/^\d{6}$/) 
      ]),
      city: new FormControl('', Validators.required),
      state: new FormControl('', Validators.required),
      country: new FormControl('', Validators.required)
    });
  }

  getUser() {
    this.userService.getUserById(this.userId).subscribe({
        next:(res)=>{
            this.user= res.data;
            console.log(this.user);

            if (this.user.addressId != null)
              this.getAddressOfUser();
        },
        error:(error)=>{
            console.log(error);
            this.toastr.error(error?.error);
        }
    });
  }

  getAddressOfUser() {
    this.addressService.getAddressById(this.user.addressId).subscribe({
        next: (res) => {
            this.address = res.data;
            console.log(this.address);
        }, 
        error: (error) => {
            console.log(error);
            // this.toastr.error(error?.error);
        }
    });
  }

  getCities() {
    this.cityService.getCities().subscribe({
      next: (res) => {
        this.cities = res.data;
      }
    });
  }
  
  getStates() {
    this.stateService.getStates().subscribe({
      next: (res) => {
        this.states = res.data;
      }
    });
  }
  
  getCountries() {
    this.countryService.getCountries().subscribe({
      next: (res) => {
        this.countries = res.data;
      }
    });
  }

  onStateChange() {
    const selectedStateId = this.AddressForm.get('state').value;
    console.log(selectedStateId);
    
    if (selectedStateId) {
        this.filteredCities = this.cities.filter(city => city.stateId === +selectedStateId);
        
    } else {
        this.filteredCities = []; // Clear the cities dropdown if no state is selected
    }
}

  viewAddressFields() {
    this.showAddressFields = !this.showAddressFields;
    console.log(this.showAddressFields);

  }

  onAddressSubmit() {
    if (this.AddressForm.valid) {
      this.address.address = this.AddressForm.value.address;
      this.address.pincode = this.AddressForm.value.pincode;
      this.address.cityId = this.AddressForm.value.city;
      this.address.userId = this.userId;

      console.log(this.address);
    
      this.postAddress();
    }
  }

  postAddress() {
    this.addressService.postAddress(this.address).subscribe({
      next: (res) => {
        this.toastr.success('Address added Successfully');
        this.AddressForm.reset();
        this.getUser();
        this.viewAddressFields();
        
        
      },
      error: (err) => {
        console.log(err);
        this.toastr.error('Failed to add Address');
      },
    })
  }
}
