import { Component } from '@angular/core';
import {
  FormGroup,
  FormBuilder,
  Validators,
  ValidatorFn,
} from '@angular/forms';
import { User } from '../../../Models/User.model';
import { AuthService } from '../../../Services/Common/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss',
})
export class RegistrationComponent {
  registrationForm: FormGroup;
  imageUrl: string =
    'https://www.freshbooks.com/wp-content/uploads/2022/01/what-is-a-warehouse.jpg';

  user: User = {} as User;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.registrationForm = this.fb.group(
      {
        name: [
          '',
          [Validators.required, Validators.pattern(/^[A-Za-z ]{2,}$/)],
        ],
        email: [
          '',
          [
            Validators.required,
            Validators.pattern(
              /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
            ),
          ],
        ],
        phoneNumber: [
          '',
          [Validators.required, Validators.pattern(/^[789]\d{9}$/)],
        ],
        role: ['', Validators.required],
        licenseNumber: [
          null,
          [Validators.pattern(/^[A-Za-z]{2}[0-9]{2}\s?[0-9]{4}\s?[0-9]{7}$/)],
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.pattern(
              /(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{5,}/
            ),
          ],
        ],
        confirmPassword: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            Validators.pattern(
              /(?=.*\d)(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z]).{5,}/
            ),
          ],
        ],
      },
      { validator: this.passwordMatchValidator }
    );
  }

  passwordMatchValidator(frm: FormGroup) {
    return frm.controls['password'].value ===
      frm.controls['confirmPassword'].value
      ? null
      : { mismatch: true };
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      this.mapFormValueToUser(this.registrationForm);
      console.log(this.user);
      this.signUp(this.user);
    }
  }

  mapFormValueToUser(registrationForm: FormGroup): User {
    const formValue = registrationForm.value;
    return (this.user = {
      id: 0,
      name: formValue.name,
      email: formValue.email,
      password: formValue.password,
      phone: formValue.phoneNumber,
      roleId: +formValue.role,
      addressId: null,
      licenseNumber: formValue.licenseNumber,
      wareHouseId: null,
      isAvailable: true,
      isApproved: formValue.role == 4 ? 1 : 0,
      isActive: true,
      createdAt: new Date(),
      updatedAt: null,
    });
  }

  signUp(user: User) {
    this.authService.signUp(user).subscribe({
      next: (res) => {
        this.toastr.success('Success', 'SignUp Successfull');
        this.router.navigate(['/auth/login']);
        this.registrationForm.reset();
      },
      error: (error) => {
        this.toastr.error('Error', error?.error?.error);
      },
    });
    this.registrationForm.reset();
  }
}
