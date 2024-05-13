import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Login } from '../../../Models/Login.mpdel';
import { AuthService } from '../../../Services/Common/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  loginForm: FormGroup;
  rememberMe: boolean = false;
  isLoading:boolean=false;

  loginModal: Login = {} as Login;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      emailId: [
        '',
        [
          Validators.required,
          Validators.pattern(
            /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
          ),
        ],
      ],
      password: ['', [Validators.required, Validators.minLength(6)]],
      rememberMe: [false],
    });

   
    
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const formValue = this.loginForm.value;
      this.loginModal.emailId = formValue.emailId.trim();
      this.loginModal.password = formValue.password.trim();
    }

    this.login(this.loginModal);
  }

  login(loginUser: Login) {
    this.authService.login(loginUser).subscribe({
      next: (res) => {
        this.toastr.success('Success', 'Login Successfull');
        this.loginForm.reset();

        const role = this.authService.getUserRole();

        if (role) {
          switch (role) {
            case 'Admin':
              this.router.navigate(['/admin']);
              break;
            case 'Manager':
              this.router.navigate(['/manager']);
              break;
            case 'Driver':
              this.router.navigate(['/driver']);
              break;
            case 'Customer':
              this.router.navigate(['/customer']);
              break;
            default:
              this.router.navigate(['/']);
          }
        }
      },
      error: (error) => {
        console.log(error);
        this.toastr.error('Error', error?.error?.error);
      },
    });
  }

  // You can implement methods here to save and retrieve the remembered state
  saveRememberedState(): void {
    localStorage.setItem('rememberMe', this.rememberMe ? 'true' : 'false');
  }

  retrieveRememberedState(): void {
    const rememberMeValue = localStorage.getItem('rememberMe');
    if (rememberMeValue) {
      this.rememberMe = rememberMeValue === 'true';
    }
  }
}
